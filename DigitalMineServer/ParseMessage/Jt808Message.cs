using ActionSafe.AcSafe_Su.Decode;
using ActionSafe.AcSafe_Su.REP_0X65;
using ActionSafe.AcSafe_Su.Reponse_Su_2013;
using DigitalMineServer.Mysql;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.Util;
using DigitalMineServer.Util.Transform;
using Google.Protobuf.WellKnownTypes;
using JtLibrary;
using JtLibrary.Jt808_2013.Reponse_2013;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using JtLibrary.Utils;
using MySqlX.XDevAPI;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace DigitalMineServer
{
    public class Jt808Message
    {
        private readonly MySqlHelper mysql;

        public Jt808Message()
        {
            mysql = new MySqlHelper();
        }

        /// <summary>
        /// 解析RTP原始数据
        /// </summary>
        public void ParseMessages()
        {
            while (true)
            {
                try
                {
                    if (Resource.OriginalDataQueues.Count <= 0)
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                    Resource.OriginalDataQueues.TryDequeue(out ValueTuple<byte[], Jt808Session> value);
                    ThreadPool.QueueUserWorkItem(OriDadaParse, value);
                    Utils.Util.ModifyLable(JtServerForm.JtForm.Message, Resource.OriginalDataQueues.Count.ToString());
                }
                catch
                {
                    Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "原始数据队列取出错误");
                }
            }
        }

        /// <summary>
        /// 处理RTP数据
        /// </summary>
        /// <param name="obj"></param>
        private void OriDadaParse(object obj)
        {
            try
            {
                IPacketProvider pConvert = PacketProvider.CreateProvider();
                ValueTuple<byte[], Jt808Session> value = (ValueTuple<byte[], Jt808Session>)obj;
                byte[] Rebyte = value.Item1;
                Jt808Session Session = value.Item2;
                PacketMessage msg = pConvert.Decode(Rebyte, 0, Rebyte.Length);
                if (Extension.BCDToString(msg.pmPacketHead.hSimNumber) == JtServerForm.JtForm.input.Text)
                {
                    ThreadPool.QueueUserWorkItem(OutMessage, Rebyte);
                }
                if (msg == null)
                {
                    return;
                }
                switch (msg.pmPacketHead.phMessageId)
                {
                    case JT808Cmd.RSP_0102:
                        new REP_0102().R0102(msg, pConvert, Session);
                        break;

                    case JT808Cmd.RSP_0100:
                        new REP_0100().R0100(msg, pConvert, Session);
                        break;

                    case JT808Cmd.RSP_0200:
                        new REP_0200().R0200(msg, pConvert, Session);
                        break;

                    case JT808Cmd.RSP_0002:
                        new REP_0002().R0002(msg, pConvert, Session);
                        break;

                    case JT808Cmd.RSP_0702:
                        new REP_0702().R0702(msg, pConvert, Session);
                        break;

                    case JT808Cmd.RSP_0704:
                        new REP_0704().R0704(msg, pConvert, Session);
                        break;

                    case JT1078Cmd.RSP_1003:
                        new REQ_8001().Default(msg, pConvert, Session);
                        break;

                    case JT1078Cmd.RSP_1205:
                        new REQ_8001().Default(msg, pConvert, Session);
                        PB1205 PB1205 = new REP_1205().Decode(msg.pmMessageBody);
                        DealWith1205(PB1205);
                        break;

                    default:
                        new REQ_8001().Default(msg, pConvert, Session);
                        break;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("RTP数据处理错误----", e);
            }
        }

        /// <summary>
        /// 0200数据体循环处理
        /// </summary>
        public void DealWith0200()
        {
            while (true)
            {
                try
                {
                    if (Resource.InsertQueues.Count < 1)
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                    Resource.InsertQueues.TryDequeue(out ValueTuple<string, PB0200> value);
                    PB0200 bodyinfo = value.Item2;
                    string Sim = value.Item1;
                    DateTime time = bodyinfo.LocationTime.BCDToTimeFormat();
                    //检查终端数据的时间是否在合理范围，终端可能上传错误时间
                    if ((time - DateTime.Now).TotalMinutes > 10)
                    {
                        Utils.Util.AppendText(JtServerForm.JtForm.infoBox, Sim + "--时间异常--" + time.ToString());
                        continue;
                    }
                    //检查车辆信息List中是否存在此车辆，不存在就返回
                    if (!Resource.VehicleList.ContainsKey(Sim))
                    {
                        Utils.Util.AppendText(JtServerForm.JtForm.infoBox, Sim + "--未知车辆--" + time.ToString());
                        continue;
                    }
                    //经纬度转换2000坐标
                    List<double> xy = WGS84ToCS2000.WGS84ToXY(Convert.ToDouble(bodyinfo.Latitude) / 1000000, Convert.ToDouble(bodyinfo.Longitude) / 1000000, 3);
                    //根据sim获取车辆信息
                    Resource.VehicleList.TryGetValue(Sim, out ValueTuple<string, string, string, string, string, string> vehicleInfo);
                    string sql = null;
                    byte[] state = BitConvert.UInt32ToBit(bodyinfo.StatusIndication);
                    string ACC = state[0] == 0 ? ACC = "关" : ACC = "开";
                    string IsStop = state[1] == 0 ? IsStop = "未定位" : IsStop = "已定位";
                    //检查车辆在状态表中是否存在
                    if (mysql.GetCount("select count(ID) as Count from vehicle_state where FID='" + vehicleInfo.Item1 + "'") == 0)
                    {
                        sql = "INSERT INTO `vehicle_state`" +
                             "(`FID`, `POSI_STATE`, `POSI_X`, `POSI_Y`, `POSI_SPEED`," +
                             " `REAl_FUEL`, `ACC`, `POSI_NUM`, `COMPANY`, `ADD_TIME`, " +
                             "`TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) " +
                             "VALUES" +
                             " ('" + vehicleInfo.Item1 + "', '" + IsStop + "', '" + xy[0] + "', '" + xy[1] + "','" + bodyinfo.Speed * 0.1 + "'," +
                             " 0, '" + ACC + "', 0, '" + vehicleInfo.Item3 + "', '" + time + "', " +
                              "NULL, NULL, NULL, NULL)";
                    }
                    else
                    {
                        Dictionary<string, string> dic = mysql.SingleSelect("select POSI_NUM from vehicle_state where FID='" + vehicleInfo.Item1 + "' ", "POSI_NUM");
                        //获取定位更新次数
                        dic.TryGetValue("POSI_NUM", out string num);
                        sql = "UPDATE `vehicle_state` SET " +
                            " `POSI_STATE` = '" + IsStop + "', `POSI_X` = '" + xy[0] + "', `POSI_Y` = '" + xy[1] + "', `POSI_SPEED` ='" + bodyinfo.Speed * 0.1 + "', `ACC` = '" + ACC + "', `POSI_NUM` = '" + (int.Parse(num) + 1) + "',`ADD_TIME`='" + time + "' WHERE FID='" + vehicleInfo.Item1 + "' ";
                    }
                    mysql.UpdOrInsOrdel(sql);
                    //定位信息插入临时表
                    sql = "INSERT INTO `temp_posi`( `VEHICLE_ID`, `VEHICLE_TYPE`, `POSI_X`, `POSI_Y`, `POSI_SPEED`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ( '" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item2 + "'," + xy[0] + ", " + xy[1] + ", '" + bodyinfo.Speed * 0.1 + "', '" + vehicleInfo.Item3 + "', '" + time + "', NULL, NULL, NULL, NULL)";
                    mysql.UpdOrInsOrdel(sql);
                    //定位信息插入永久表
                    sql = "INSERT INTO `posi_vehicle`( `VEHICLE_ID`, `VEHICLE_TYPE`, `POSI_X`, `POSI_Y`, `POSI_SPEED`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ( '" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item2 + "'," + xy[0] + ", " + xy[1] + ", '" + bodyinfo.Speed * 0.1 + "', '" + vehicleInfo.Item3 + "', '" + time + "', NULL, NULL, NULL, NULL)";
                    mysql.UpdOrInsOrdel(sql);
                    //检查超速
                    CheckSpeed(Sim, time, vehicleInfo, bodyinfo, xy);
                    //处理附加消息体
                    ManageAttachItems(Sim, bodyinfo, vehicleInfo, time);
                    if (Resource.isVehicleUpdate)
                    {
                        continue;
                    }
                    //围栏检查
                    CheckFence(Sim, xy);
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog("数据存储----", e);
                }
            }
        }

        /// <summary>
        /// 1205信息处理
        /// </summary>
        /// <param name="PB1205"></param>
        private void DealWith1205(PB1205 PB1205)
        {
            if (PB1205.count == 0)
            {
                if (!Resource.msgSerialnumberDic.ContainsKey(PB1205.serialNumber))
                {
                    return;
                }
                //获取客户端录像请求连接头返回结果
                ClientHistoryVideoServer Server = JtServerForm.bootstrap.GetServerByName("ClientHistoryVideoServer") as ClientHistoryVideoServer;
                var sessions = Server.GetSessions(s => s.Sim == Resource.msgSerialnumberDic[PB1205.serialNumber]);
                if (sessions.Count() > 0)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes("未找到资源");
                    foreach (var item in sessions)
                    {
                        item.Send(buffer.Concat(new byte[] { 11, 22, 33, 44 }).ToArray(), 0, buffer.Length + 4);
                    }
                }
                Resource.msgSerialnumberDic.TryRemove(PB1205.serialNumber, out _);
            }
        }

        /// <summary>
        /// 超速检查
        /// </summary>
        /// <param name="Sim">SIM号</param>
        /// <param name="time">时间</param>
        /// <param name="vehicleInfo">车辆信息</param>
        /// <param name="bodyinfo">0200数据体</param>
        /// <param name="xy">2000坐标</param>
        private void CheckSpeed(string Sim, DateTime time, ValueTuple<string, string, string, string, string, string> vehicleInfo, PB0200 bodyinfo, List<double> xy)
        {
            if (bodyinfo.Speed * 0.1 > int.Parse(vehicleInfo.Item4))
            {
                //检查1min内是否已上报超度记录，有则跳过
                if (mysql.GetCount("select COUNT(ID) as Count from rec_unu_speed where VEHICLE_ID='" + vehicleInfo.Item5 + "'" +
                    "and Company='" + vehicleInfo.Item3 + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 1 MINUTE)") == 0)
                {
                    //给车辆发送超速警告，语音播报
                    SendMessage(Sim, new REQ_8300().R8300(Sim, "你已超速，限速" + vehicleInfo.Item4));
                    string sql = "INSERT INTO `rec_unu_speed`" +
                         "(`VEHICLE_ID`, `DRIVER`, `VEHICLE_TYPE`, `POSI_SPEED`, `POSI_X`, `POSI_Y`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`" +
                         ") " +
                         "VALUES ( " +
                         "'" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item6 + "', '" + vehicleInfo.Item2 + "','" + bodyinfo.Speed * 0.1 + "', '" + xy[0] + "', '" + xy[1] + "', '" + vehicleInfo.Item3 + "', '" + time + "', NULL, NULL, NULL, NULL" +
                         ")";
                    mysql.UpdOrInsOrdel(sql);
                }
            }
        }

        /// <summary>
        /// 判断围栏
        /// </summary>
        /// <param name="Sim">SIM号</param>
        /// <param name="xy">2000坐标</param>
        private void CheckFence(string Sim, List<double> xy)
        {
            //判断禁入围栏
            if (Resource.fenceFanbidInInfo.ContainsKey(Sim))
            {
                ValueTuple<string, string, string, string, string, List<Point>> temp = Resource.fenceFanbidInInfo[Sim];
                if (Polygon.IsInPolygon(new Point(xy[0], xy[1]), temp.Item6))
                {
                    string sql = "select COUNT(ID) as Count from rec_unu_info where COMPANY='" + temp.Item2 + "' and VEHICLE_ID='" + temp.Item4 + "' and WARNTYPE='" + WarnType.Forbid_In + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 2 MINUTE)";
                    if (mysql.GetCount(sql) == 0)
                    {
                        sql = "INSERT INTO `product`.`rec_unu_info`( `VEHICLE_ID`, `VEHICLE_TYPE`, `WARNTYPE`, `INFO`, `DRIVER`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + temp.Item4 + "','" + temp.Item3 + "', '" + WarnType.Forbid_In + "', '围栏名称：" + temp.Item1 + "', '" + temp.Item5 + "', '" + temp.Item2 + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL)";
                        mysql.UpdOrInsOrdel(sql);
                    }
                }
            }
            //判断禁出围栏
            if (Resource.fenceFanbidOutInfo.ContainsKey(Sim))
            {
                ValueTuple<string, string, string, string, string, List<Point>> temp = Resource.fenceFanbidOutInfo[Sim];
                if (!Polygon.IsInPolygon(new Point(xy[0], xy[1]), temp.Item6))
                {
                    string sql = "select COUNT(ID) as Count from rec_unu_info where COMPANY='" + temp.Item2 + "' and VEHICLE_ID='" + temp.Item4 + "' and WARNTYPE='" + WarnType.Forbid_Out + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 2 MINUTE)";
                    if (mysql.GetCount(sql) == 0)
                    {
                        sql = "INSERT INTO `product`.`rec_unu_info`( `VEHICLE_ID`, `VEHICLE_TYPE`, `WARNTYPE`, `INFO`, `DRIVER`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + temp.Item4 + "','" + temp.Item3 + "', '" + WarnType.Forbid_Out + "', '围栏名称：" + temp.Item1 + "', '" + temp.Item5 + "', '" + temp.Item2 + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL)";
                        mysql.UpdOrInsOrdel(sql);
                    }
                }
            }
        }

        /// <summary>
        /// 给指定终端下发消息(无论是否在线)
        /// </summary>
        /// <param name="sim">SIM号</param>
        /// <param name="info"></param>
        private void SendMessage(string sim, byte[] info)
        {
            //获取终端连接下发指令
            Jt808Server Jt808 = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = Jt808.GetSessions(s => s.Sim == sim);
            if (sessions.Count() == 1)
            {
                sessions.First().Send(info, 0, info.Length);
            }
        }

        /// <summary>
        /// 给指定终端下发消息（返回发送结果）
        /// </summary>
        /// <param name="sim">SIM号</param>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool AsyncSendMessage(string sim, byte[] info)
        {
            //获取终端连接下发指令
            Jt808Server Jt808 = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = Jt808.GetSessions(s => s.Sim == sim);
            if (sessions.Count() == 1)
            {
                return sessions.First().TrySend(info, 0, info.Length);
            }
            return false;
        }

        /// <summary>
        /// 输出指定SIM车辆
        /// </summary>
        /// <param name="obj"></param>
        private void OutMessage(object obj)
        {
            byte[] buffer = (byte[])obj;
            string str = "";
            foreach (var i in buffer)
            {
                str += i.ToString("X2") + " ";
            }
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, str);
            PacketMessage msg = PacketProvider.CreateProvider().Decode(buffer, 0, buffer.Length);
            if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0200)
            {
                PB0200 bodyinfo = new REP_0200_2013().Decode(msg.pmMessageBody);
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "经度---" + bodyinfo.Longitude + "纬度---" + bodyinfo.Latitude);
            }
        }

        private void CheckWarn(uint AlarmIndication)
        {
            //检查报警标识
            byte[] warn = BitConvert.UInt32ToBit(AlarmIndication);
            if (warn[0] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "紧急报警");
            }
            if (warn[2] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "疲劳驾驶报警");
            }
            if (warn[3] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "危险驾驶报警");
            }
            if (warn[4] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "GNSS模块故障报警");
            }
            if (warn[5] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "GNSS天线未接报警");
            }
            if (warn[6] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "GNSS天线短路报警");
            }
            if (warn[7] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "终端主电源欠压报警");
            }
            if (warn[8] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "终端主电源掉电报警");
            }
            if (warn[9] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "终端LCD故障报警");
            }
            if (warn[10] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "TTS模块故障报警");
            }
            if (warn[11] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "摄像头故障报警");
            }
            if (warn[12] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "IC卡故障报警");
            }
            if (warn[13] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "超速报警");
            }
            if (warn[14] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "疲劳驾驶报警");
            }
            if (warn[15] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "违规行驶报警");
            }
            if (warn[16] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "胎压报警");
            }
            if (warn[17] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "右转盲区异常报警");
            }
            if (warn[18] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "当天累计驾驶超时报警");
            }
            if (warn[19] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "超时停车报警");
            }
            if (warn[20] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "进出区域报警");
            }
            if (warn[21] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "进出路线报警");
            }
            if (warn[22] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "路段行驶时间报警");
            }
            if (warn[23] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "路线偏离报警");
            }
            if (warn[24] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆VVS报警");
            }
            if (warn[25] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆油量异常报警");
            }
            if (warn[26] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆被盗报警");
            }
            if (warn[27] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆点火报警");
            }
            if (warn[28] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆非法位移报警");
            }
            if (warn[29] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "碰撞侧翻报警");
            }
            if (warn[30] == 1)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "侧翻报警");
            }
        }

        /// <summary>
        /// 处理附加信息
        /// </summary>
        /// <param name="bodyinfo"></param>
        /// <param name="sim"></param>
        /// <param name="vehicleInfo"></param>
        /// <param name="time"></param>
        private void ManageAttachItems(string sim, PB0200 bodyinfo, ValueTuple<string, string, string, string, string, string> vehicleInfo, DateTime time)
        {
            //附加消息体
            for (int i = 0; i < bodyinfo.AttachItems.Count; i++)
            {
                switch (bodyinfo.AttachItems[i].Value)
                {
                    case 0xEB:
                        //获取油量
                        AttachItems0XEb(vehicleInfo, time, bodyinfo.AttachItems[i].BytesValue);
                        break;

                    case 0x02:
                        AttachItems0X02(vehicleInfo, time, bodyinfo.AttachItems[i].BytesValue);
                        break;

                    case 0x64:
                        AttachItems0X64(sim, vehicleInfo, bodyinfo.AttachItems[i].BytesValue);
                        break;

                    case 0x65:
                        AttachItems0X65(sim, vehicleInfo, bodyinfo.AttachItems[i].BytesValue);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 博实结附加信息油量处理
        /// </summary>
        /// <param name="vehicleInfo">车辆信息</param>
        /// <param name="time">时间</param>
        /// <param name="BytesValue">数据体</param>
        private void AttachItems0XEb(ValueTuple<string, string, string, string, string, string> vehicleInfo, DateTime time, byte[] BytesValue)
        {
            Dictionary<int, byte[]> dic = new DecodeBSJ().decode(BytesValue);
            if (dic.ContainsKey(0x23))
            {
                string sql = "INSERT INTO `fuel_orig`( `VEHICLE_ID`, `DRIVE_NAME`, `ORIG_FUEL`, `REC_STATE`, `COMPANY`, `ADD_TIME`) VALUES ('" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item6 + "', '" + Encoding.ASCII.GetString(dic[0x23]) + "', 'NO', '" + vehicleInfo.Item3 + "', '" + time + "')";
                mysql.UpdOrInsOrdel(sql);
            }
        }

        /// <summary>
        /// 通用附加信息油量处理
        /// </summary>
        /// <param name="vehicleInfo">车辆信息</param>
        /// <param name="time">时间</param>
        /// <param name="BytesValue">数据体</param>
        private void AttachItems0X02(ValueTuple<string, string, string, string, string, string> vehicleInfo, DateTime time, byte[] BytesValue)
        {
            string sql = "INSERT INTO `fuel_orig`( `VEHICLE_ID`, `DRIVE_NAME`, `ORIG_FUEL`, `REC_STATE`, `COMPANY`, `ADD_TIME`) VALUES ('" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item6 + "', '" + (BytesValue.ToUInt16(0) / 10) + "', 'NO', '" + vehicleInfo.Item3 + "', '" + time + "')";
            mysql.UpdOrInsOrdel(sql);
        }

        /// <summary>
        /// 主动安全0x64信息
        /// </summary>
        /// <param name="sim"></param>
        /// <param name="vehicleInfo">车辆信息</param>
        /// <param name="BytesValue">数据体</param>
        private void AttachItems0X64(string sim, ValueTuple<string, string, string, string, string, string> vehicleInfo, byte[] BytesValue)
        {
            PB0X64 DriveHelp = new REP_0X64().Decode(BytesValue);
            if (DriveHelp.WarnState == 0x01 || true)
            {
                //平台分配的唯一主动安全报警标识号，来自精确到微秒的时间
                byte[] WarnId = Encoding.UTF8.GetBytes(Utils.Util.getTime());
                //经纬度转换2000坐标
                List<double> xy = WGS84ToCS2000.WGS84ToXY(Convert.ToDouble(DriveHelp.latitude) / 1000000, Convert.ToDouble(DriveHelp.longitude) / 1000000, 3);
                string info = "纬度：" + Convert.ToDouble(DriveHelp.latitude) / 1000000 + "，经度：" + Convert.ToDouble(DriveHelp.longitude) / 1000000;
                string sql = "INSERT INTO `rec_unu_acsafe`(`VEHICLE_ID`, `DRIVER`, `VEHICLE_TYPE`, `POSI_SPEED`,`WARN_TYPE`, `EVENT_TYPE`, `LEVEL`, `POSI_X`, `POSI_Y`, `WARN_INFO`, `WARN_NUMBER`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item6 + "', '" + vehicleInfo.Item2 + "', '" + DriveHelp.VehicleSpeed + "', 'warnDriveHelp','" + new Decode().GetDriveHelpWarnType(DriveHelp.WarnType) + "', '" + new Decode().GetWarnLevel(DriveHelp.WarnLevel) + "', '" + xy[0] + "','" + xy[1] + "', '" + info + "', '" + Utils.Util.GetMd5(Utils.Util.GetStringHex(WarnId)) + "', '" + vehicleInfo.Item3 + "', '" + Extension.BCDToTimeFormat(DriveHelp.Time) + "', NULL, NULL, NULL, NULL)";
                mysql.UpdOrInsOrdel(sql);
                //给车辆发送超速警告，语音播报
                SendMessage(sim, new REQ_8300().R8300(sim, "请注意，检测到" + new Decode().GetDriveHelpWarnType(DriveHelp.WarnType)));
                //检查报警附件
                WarnNumber WarnNumber = new Decode().DecodeWarnNumber(DriveHelp.WarnNumber);
                if (WarnNumber.FileCount > 0)
                {
                    bool result = AsyncSendMessage(sim, new REQ_9208().Packet_9208_Su_2013(sim, DriveHelp.WarnNumber, WarnId));
                    if (result)
                    {
                        Resource.WarnIdDic.TryAdd(sim, new ValueTuple<byte[], DateTime>
                        {
                            Item1 = WarnId,
                            Item2 = DateTime.Now
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 主动安全0x65信息
        /// </summary>
        /// <param name="sim"></param>
        /// <param name="vehicleInfo">车辆信息</param>
        /// <param name="BytesValue">数据体</param>
        private void AttachItems0X65(string sim, ValueTuple<string, string, string, string, string, string> vehicleInfo, byte[] BytesValue)
        {
            PB0X65 DriverState = new REP_0X65().Decode(BytesValue);
            if (DriverState.WarnState == 0x01 || true)
            {
                //平台分配的唯一主动安全报警标识号，来自精确到微秒的时间
                byte[] WarnId = Encoding.UTF8.GetBytes(Utils.Util.getTime());
                List<double> xy = WGS84ToCS2000.WGS84ToXY(Convert.ToDouble(DriverState.latitude) / 1000000, Convert.ToDouble(DriverState.longitude) / 1000000, 3);
                string info = "纬度：" + Convert.ToDouble(DriverState.latitude) / 1000000 + "，经度：" + Convert.ToDouble(DriverState.longitude) / 1000000;
                string sql = "INSERT INTO `rec_unu_acsafe`( `VEHICLE_ID`, `DRIVER`, `VEHICLE_TYPE`, `POSI_SPEED`,`WARN_TYPE`,`EVENT_TYPE`, `LEVEL`, `POSI_X`, `POSI_Y`, `WARN_INFO`, `WARN_NUMBER`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item6 + "', '" + vehicleInfo.Item2 + "','" + DriverState.VehicleSpeed + "', 'warnDriverState','" + new Decode().GetDriverStateWarnType(DriverState.WarnType) + "', '" + new Decode().GetWarnLevel(DriverState.WarnLevel) + "', '" + xy[0] + "', '" + xy[1] + "', '" + info + "', '" + Utils.Util.GetMd5(Utils.Util.GetStringHex(WarnId)) + "', '" + vehicleInfo.Item3 + "', '" + Extension.BCDToTimeFormat(DriverState.Time) + "', NULL, NULL, NULL, NULL)";
                mysql.UpdOrInsOrdel(sql);
                SendMessage(sim, new REQ_8300().R8300(sim, "请注意，检测到" + new Decode().GetDriveHelpWarnType(DriverState.WarnType)));
                //检查报警附件
                WarnNumber WarnNumber = new Decode().DecodeWarnNumber(DriverState.WarnNumber);
                if (WarnNumber.FileCount > 0)
                {
                    bool result = AsyncSendMessage(sim, new REQ_9208().Packet_9208_Su_2013(sim, DriverState.WarnNumber, WarnId));
                    if (result)
                    {
                        Resource.WarnIdDic.TryAdd(sim, new ValueTuple<byte[], DateTime>
                        {
                            Item1 = WarnId,
                            Item2 = DateTime.Now
                        });
                    }
                }
            }
        }
    }
}