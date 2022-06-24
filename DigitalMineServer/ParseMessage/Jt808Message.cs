using DigitalMineServer.implement;
using DigitalMineServer.Mysql;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.Util;
using JtLibrary;
using JtLibrary.PacketBody;
using JtLibrary.PacketBody.Reponse;
using JtLibrary.Structures;
using JtLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DigitalMineServer
{
    public class Jt808Message
    {
        private MySqlHelper mysql;
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
                    implement.Util.ModifyLable(JtServerForm.JtForm.Message, Resource.OriginalDataQueues.Count.ToString());
                }
                catch
                {
                    implement.Util.AppendText(JtServerForm.JtForm.infoBox, "原始数据队列取出错误");
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
                if (msg != null && msg.pmPacketHead.phMessageId.ToString() != null)
                {
                    switch (msg.pmPacketHead.phMessageId)
                    {
                        case JT808Cmd.RSP_0102:
                            new REP0102().R0102(msg, pConvert, Session);
                            break;
                        case JT808Cmd.RSP_0100:
                            new REP0100().R0100(msg, pConvert, Session);
                            break;
                        case JT808Cmd.RSP_0200:
                            new REP0200().R0200(msg, pConvert, Session);
                            break;
                        case JT808Cmd.RSP_0002:
                            new REP0002().R0002(msg, pConvert, Session);
                            break;
                        case JT808Cmd.RSP_0702:
                            new REP0702().R0702(msg, pConvert, Session);
                            break;
                        case JT808Cmd.RSP_0704:
                            new REP0704().R0704(msg, pConvert, Session);
                            break;
                        case JT1078Cmd.REQ_1003:
                            new REPDefault().Default(msg, pConvert, Session);
                            break;
                        case JT1078Cmd.REQ_1205:
                            new REPDefault().Default(msg, pConvert, Session);
                            break;
                        default:
                            new REPDefault().Default(msg, pConvert, Session);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("RTP数据处理错误----", e);
            }
        }
        /// <summary>
        /// 数据库存储
        /// </summary>
        public void InsertDB()
        {
            /// vehicleInfo
            /// item1：车辆编号外键
            /// item2：车辆类型
            /// item3：所属公司
            /// item4：超速阈值
            /// item5：车辆编号
            /// item6：司机
            while (true)
            {
                try
                {
                    if (Resource.InsertQueues.Count <1)
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
                        implement.Util.AppendText(JtServerForm.JtForm.infoBox, Sim + "--时间异常--" + time.ToString());
                        continue;
                    }
                    //检查车辆信息List中是否存在此车辆，不存在就返回
                    if (!Resource.VehicleList.ContainsKey(Sim))
                    {
                        implement.Util.AppendText(JtServerForm.JtForm.infoBox, Sim + "--未知车辆--" + time.ToString());
                        continue;
                    }
                    //经纬度转换2000坐标
                    List<double> xy = CTransform.WGS84ToXY(Convert.ToDouble(bodyinfo.Latitude) / 1000000, Convert.ToDouble(bodyinfo.Longitude) / 1000000, 3);
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
                             " 0, '"+ ACC + "', 0, '" + vehicleInfo.Item3 + "', '" + time + "', " +
                              "NULL, NULL, NULL, NULL);";
                    }
                    else
                    {
                        Dictionary<string, string> dic = mysql.SingleSelect("select POSI_NUM from vehicle_state where FID='" + vehicleInfo.Item1 + "' ", "POSI_NUM");
                        dic.TryGetValue("POSI_NUM", out string num);
                        sql = "UPDATE `vehicle_state` SET " +
                            " `POSI_STATE` = '" + IsStop + "', `POSI_X` = '" + xy[0] + "', `POSI_Y` = '" + xy[1] + "', `POSI_SPEED` ='" + bodyinfo.Speed * 0.1 + "', `ACC` = '" + ACC + "', `POSI_NUM` = '" + (int.Parse(num) + 1) + "',`ADD_TIME`='" + time + "' WHERE FID='" + vehicleInfo.Item1 + "' ";
                    }
                    mysql.UpdOrInsOrdel(sql);
                    //定位信息插入临时表
                    sql = "INSERT INTO `temp_posi`( `VEHICLE_ID`, `VEHICLE_TYPE`, `POSI_X`, `POSI_Y`, `POSI_SPEED`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ( '" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item2 + "'," + xy[0] + ", " + xy[1] + ", '" + bodyinfo.Speed * 0.1 + "', '" + vehicleInfo.Item3 + "', '" + time + "', NULL, NULL, NULL, NULL);";
                    mysql.UpdOrInsOrdel(sql);
                    //定位信息插入永久表
                    sql = "INSERT INTO `posi_vehicle`( `VEHICLE_ID`, `VEHICLE_TYPE`, `POSI_X`, `POSI_Y`, `POSI_SPEED`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ( '" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item2 + "'," + xy[0] + ", " + xy[1] + ", '" + bodyinfo.Speed * 0.1 + "', '" + vehicleInfo.Item3 + "', '" + time + "', NULL, NULL, NULL, NULL);";
                    mysql.UpdOrInsOrdel(sql);
                    //检查超速
                    if (bodyinfo.Speed*0.1 > int.Parse(vehicleInfo.Item4))
                    {
                        if (mysql.GetCount("select COUNT(ID) as Count from rec_unu_speed where VEHICLE_ID='" + vehicleInfo.Item5 + "'" +
                            "and Company='" + vehicleInfo.Item3 + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 5 MINUTE)") == 0)
                        {
                            sql = "INSERT INTO `rec_unu_speed`" +
                                "(`VEHICLE_ID`, `DRIVER`, `VEHICLE_TYPE`, `POSI_SPEED`, `POSI_X`, `POSI_Y`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`" +
                                ") " +
                                "VALUES ( " +
                                "'" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item6 + "', '" + vehicleInfo.Item2 + "','" + bodyinfo.Speed * 0.1 + "', '" + xy[0] + "', '" + xy[1] + "', '" + vehicleInfo.Item3 + "', '" + time + "', NULL, NULL, NULL, NULL" +
                                ");";
                            mysql.UpdOrInsOrdel(sql);
                        }
                    }
                    //附加消息体
                    for (int i = 0; i < bodyinfo.AttachItems.Count; i++)
                    {
                        if (bodyinfo.AttachItems[i].Value == 0x23 || bodyinfo.AttachItems[i].Value.ToString("X2") == "2")
                        {
                            string ico = Encoding.ASCII.GetString(bodyinfo.AttachItems[i].BytesValue);
                            sql = "INSERT INTO `fuel_orig`( `VEHICLE_ID`, `DRIVE_NAME`, `ORIG_FUEL`, `REC_STATE`, `COMPANY`, `ADD_TIME`) VALUES ('" + vehicleInfo.Item5 + "', '" + vehicleInfo.Item6 + "', '" + Encoding.ASCII.GetString(bodyinfo.AttachItems[i].BytesValue) + "', 'NO', '" + vehicleInfo.Item3 + "', '" + time + "')";
                            mysql.UpdOrInsOrdel(sql);
                        }
                    }
                    if (Resource.isVehicleUpdate) {
                        continue;
                    }
                    //判断禁入围栏
                    if (Resource.fenceFanbidInInfo.ContainsKey(Sim))
                    {
                        ValueTuple<string, string, string, string, string, List<Point>> temp = Resource.fenceFanbidInInfo[Sim];
                        if (Polygon.IsInPolygon(new Point(xy[0],xy[1]), temp.Item6))
                        {
                            sql = "select COUNT(ID) as Count from rec_unu_info where COMPANY='" + temp.Item2 + "' and VEHICLE_ID='" + temp.Item4 + "' and WARNTYPE='" + WarnType.Forbid_In + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 5 MINUTE)";
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
                        if (!Polygon.IsInPolygon(new Point(xy[0],xy[1]), temp.Item6))
                        {
                            sql = "select COUNT(ID) as Count from rec_unu_info where COMPANY='" + temp.Item2 + "' and VEHICLE_ID='" + temp.Item4 + "' and WARNTYPE='" + WarnType.Forbid_Out + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 5 MINUTE)";
                            if (mysql.GetCount(sql) == 0)
                            {
                                sql = "INSERT INTO `product`.`rec_unu_info`( `VEHICLE_ID`, `VEHICLE_TYPE`, `WARNTYPE`, `INFO`, `DRIVER`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + temp.Item4 + "','" + temp.Item3 + "', '" + WarnType.Forbid_Out + "', '围栏名称：" + temp.Item1 + "', '" + temp.Item5 + "', '" + temp.Item2 + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL)";
                                mysql.UpdOrInsOrdel(sql);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog("数据存储----", e);
                }
            }
        }
        private void OutMessage(object obj)
        {
            byte[] buffer = (byte[])obj;
            string str = "";
            foreach (var i in buffer)
            {
                str += i.ToString("X2") + " ";
            }
            implement.Util.AppendText(JtServerForm.JtForm.infoBox, str);
            PacketMessage msg =PacketProvider.CreateProvider().Decode(buffer,0, buffer.Length);
            if (msg.pmPacketHead.phMessageId== JT808Cmd.RSP_0200) {
                PB0200 bodyinfo = new REP_0200().Decode(msg.pmMessageBody);
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "经度"+bodyinfo.Longitude+"纬度"+ bodyinfo.Latitude);
            }
        }

        private void CheckWarn(uint AlarmIndication) {
            //检查报警标识
            byte[] warn = BitConvert.UInt32ToBit(AlarmIndication);
            if (warn[0] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "紧急报警");
            }
            if (warn[2] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "疲劳驾驶报警");
            }
            if (warn[3] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "危险驾驶报警");
            }
            if (warn[4] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "GNSS模块故障报警");
            }
            if (warn[5] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "GNSS天线未接报警");
            }
            if (warn[6] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "GNSS天线短路报警");
            }
            if (warn[7] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "终端主电源欠压报警");
            }
            if (warn[8] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "终端主电源掉电报警");
            }
            if (warn[9] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "终端LCD故障报警");
            }
            if (warn[10] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "TTS模块故障报警");
            }
            if (warn[11] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "摄像头故障报警");
            }
            if (warn[12] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "IC卡故障报警");
            }
            if (warn[13] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "超速报警");
            }
            if (warn[14] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "疲劳驾驶报警");
            }
            if (warn[15] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "违规行驶报警");
            }
            if (warn[16] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "胎压报警");
            }
            if (warn[17] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "右转盲区异常报警");
            }
            if (warn[18] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "当天累计驾驶超时报警");
            }
            if (warn[19] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "超时停车报警");
            }
            if (warn[20] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "进出区域报警");
            }
            if (warn[21] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "进出路线报警");
            }
            if (warn[22] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "路段行驶时间报警");
            }
            if (warn[23] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "路线偏离报警");
            }
            if (warn[24] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆VVS报警");
            }
            if (warn[25] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆油量异常报警");
            }
            if (warn[26] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆被盗报警");
            }
            if (warn[27] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆点火报警");
            }
            if (warn[28] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "车辆非法位移报警");
            }
            if (warn[29] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "碰撞侧翻报警");
            }
            if (warn[30] == 1)
            {
                implement.Util.AppendText(JtServerForm.JtForm.infoBox, "侧翻报警");
            }
        }
    }
}
