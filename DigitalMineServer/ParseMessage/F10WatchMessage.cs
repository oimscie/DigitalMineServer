using DigitalMineServer.Utils;
using DigitalMineServer.OrderMessage;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.SuperSocket.SocketSession;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartWatch.F10.PacketBody;
using SmartWatch.F10.Structures;
using DigitalMineServer.Static;
using System.Threading;
using System;
using DigitalMineServer.Util;
using SmartWatch.F10.Reponse;
using Google.Protobuf.WellKnownTypes;
using DigitalMineServer.Mysql;
using DigitalMineServer.Util.Transform;
using static ServiceStack.Script.Lisp;
using DigitalMineServer.Redis;
using System.Windows.Forms;
using MySqlX.XDevAPI;

namespace DigitalMineServer.ParseMessage
{
    public class F10WatchMessage
    {
        private readonly PacketFrom PacketFrom;

        private readonly MySqlHelper Mysql;

        private readonly RedisHelper PersonRedis;

        public F10WatchMessage()
        {
            PacketFrom = new PacketFrom();
            Mysql = new MySqlHelper();
            PersonRedis = new RedisHelper();
        }

        public void ParseMessage()
        {
            while (Resource.IsActive)
            {
                try
                {
                    if (Resource.OriginalWatchDataQueues.Count <= 0)
                    {
                        Thread.Sleep(500);
                        continue;
                    }
                    Resource.OriginalWatchDataQueues.TryDequeue(out ValueTuple<byte[], F10WatchSession> value);
                    F10Packet F10Packet = PacketFrom.F10UnPack(value.Item1);
                    if (F10Packet.content == null)
                    {
                        continue;
                    }
                    if (value.Item2.Id == null)
                    {
                        value.Item2.Id = F10Packet.FixBody.id;
                    }
                    ParseContent(PacketFrom.F10GetHeadName(F10Packet.content), F10Packet, value);
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog("F10智能手表数据错误", e);
                }
            }
        }

        public void ParseContent(string HeadName, F10Packet F10Packet, ValueTuple<byte[], F10WatchSession> value)
        {
            switch (HeadName)
            {
                case F10Cmd.LK:
                    CommonRsp(HeadName, F10Packet, value.Item2);
                    Utils.Util.AppendText(JtServerForm.JtForm.infoBox, F10Packet.content);
                    break;

                case F10Cmd.UD_LTE:
                    //定位数据处理
                    PositionData(F10Packet);
                    break;

                case F10Cmd.UD2_LTE:
                    //盲区定位数据处理
                    PositionData(F10Packet);
                    break;

                case F10Cmd.AL_LTE:
                    PositionData(F10Packet);
                    break;

                case F10Cmd.UPLOAD:
                    break;

                case F10Cmd.WT:
                    break;

                case F10Cmd.IP:
                    break;

                case F10Cmd.SOS:
                    break;

                case F10Cmd.MONITOR:
                    break;

                case F10Cmd.SOSSMS:
                    break;

                case F10Cmd.LOWBAT:
                    break;

                case F10Cmd.CENTER:
                    break;

                case F10Cmd.REMOVESMS:
                    break;

                case F10Cmd.REMOVE:
                    break;

                case F10Cmd.rcapture:
                    break;

                case F10Cmd.img:
                    break;

                case F10Cmd.bodytemp:
                    break;

                case F10Cmd.hrtstart:
                    break;

                case F10Cmd.bphrt:
                    //检查人员信息List中是否存在此人员，不存在就返回
                    RepBphrt_St RepBphrt_St = new RepBphrt().Decode(F10Packet.content);
                    ValueTuple<string, string, string, string> PersonInfo2 = PersonRedis.GetPersonList(F10Packet.FixBody.id);
                    if (PersonInfo2.Item1 is null)
                    {
                        Utils.Util.AppendText(JtServerForm.JtForm.infoBox, F10Packet.FixBody.id + "--未知人员手表设备--" + DateTime.Now);
                        return;
                    }
                    string bphrtSql = "select count(id) as Count from person_state where person_sim='" + F10Packet.FixBody.id + "'";
                    if (Mysql.GetCount(bphrtSql) == 0)
                    {
                        // string HEARTRATE = RepBphrt_St.high + "/" + RepBphrt_St.low;
                        bphrtSql = "INSERT INTO `person_state`(`FID`, `POSI_STATE`, `POSI_X`, `POSI_Y`, `ACC`, `BATTERY`, `STEP`, `STATE`, `HEARTRATE`, `BLPRES`, `POSI_NUM`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + PersonInfo2.Item1 + "', '未定位', '未定位', '未定位', '不支持', '暂无', '暂无', '终端状态', '" + RepBphrt_St.HeartRate + "', '" + RepBphrt_St.high + "/" + RepBphrt_St.low + "', 0, '" + PersonInfo2.Item3 + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL)";
                    }
                    else
                    {
                        bphrtSql = "update  `person_state` set `HEARTRATE`='" + RepBphrt_St.HeartRate + "',`BLPRES`='" + RepBphrt_St.high + "/" + RepBphrt_St.low + "' ,ADD_TIME='" + DateTime.Now + "' where `FID`='" + PersonInfo2.Item1 + "'";
                    }
                    Mysql.UpdOrInsOrdel(bphrtSql);
                    break;

                case F10Cmd.oxygen:
                    //检查人员信息List中是否存在此人员，不存在就返回
                    RepOxygen_St RepOxygen_St = new RepOxygen().Decode(F10Packet.content);
                    ValueTuple<string, string, string, string> PersonInfo3 = PersonRedis.GetPersonList(F10Packet.FixBody.id);
                    if (PersonInfo3.Item1 is null)
                    {
                        Utils.Util.AppendText(JtServerForm.JtForm.infoBox, F10Packet.FixBody.id + "--未知人员手表设备--" + DateTime.Now);
                        return;
                    }
                    string OxygenSql = "select count(id) as Count from person_state where person_sim='" + F10Packet.FixBody.id + "'";
                    if (Mysql.GetCount(OxygenSql) == 0)
                    {
                        // string HEARTRATE = RepBphrt_St.high + "/" + RepBphrt_St.low;
                        bphrtSql = "INSERT INTO `person_state`(`FID`, `POSI_STATE`, `POSI_X`, `POSI_Y`, `ACC`, `BATTERY`, `STEP`, `STATE`, `HEARTRATE`, `BLPRES`, `POSI_NUM`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + PersonInfo3.Item1 + "', '未定位', '未定位', '未定位', '不支持', '暂无', '暂无', '终端状态', '暂无', '" + RepOxygen_St.oxy + "', 0, '" + PersonInfo3.Item3 + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL)";
                    }
                    else
                    {
                        bphrtSql = "update  `person_state` set `BLPRES`='" + RepOxygen_St.oxy + "',ADD_TIME='" + DateTime.Now + "' where `FID`='" + PersonInfo3.Item1 + "'";
                    }
                    Mysql.UpdOrInsOrdel(bphrtSql);
                    RepOxygen(HeadName, F10Packet, value.Item2);
                    break;

                default:
                    Utils.Util.AppendText(JtServerForm.JtForm.infoBox, F10Packet.content);
                    break;
            }
        }

        /// <summary>
        /// 定位数据处理
        /// </summary>
        /// <param name="F10Packet"></param>
        public void PositionData(F10Packet F10Packet)
        {
            //经纬度转换2000坐标
            RepUd_Lte_St RepUd_Lte_St = new RepUd_Lte().Decode(F10Packet.content);
            //检查人员信息List中是否存在此人员，不存在就返回
            ValueTuple<string, string, string, string> PersonInfo = PersonRedis.GetPersonList(F10Packet.FixBody.id);
            if (PersonInfo.Item1 is null)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, F10Packet.FixBody.id + "--未知人员手表设备--" + DateTime.Now);
                return;
            }
            List<double> xy = WGS84ToCS2000.WGS84ToXY(Convert.ToDouble(RepUd_Lte_St.position.lat), Convert.ToDouble(RepUd_Lte_St.position.lon), 3);
            string Ud_LteSql = "select count(id) as Count from person_state where person_sim='" + F10Packet.FixBody.id + "'";
            if (Mysql.GetCount(Ud_LteSql) == 0)
            {
                Ud_LteSql = "INSERT INTO `person_state`(`FID`, `POSI_STATE`, `POSI_X`, `POSI_Y`, `ACC`, `BATTERY`, `STEP`, `STATE`, `HEARTRATE`, `BLPRES`, `POSI_NUM`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + PersonInfo.Item1 + "', '" + RepUd_Lte_St.position.active + "', '" + xy[0] + "', '" + xy[1] + "', '不支持', '" + RepUd_Lte_St.position.battery + "', '" + RepUd_Lte_St.position.step + "', '终端状态', '暂无', '暂无', 0, '" + PersonInfo.Item3 + "', '" + RepUd_Lte_St.position.time + "', NULL, NULL, NULL, NULL)";
            }
            else
            {
                Dictionary<string, string> dic = Mysql.SingleSelect("select POSI_NUM from vehicle_state where FID='" + PersonInfo.Item1 + "' ", "POSI_NUM");
                //获取定位更新次数
                dic.TryGetValue("POSI_NUM", out string num);
                Ud_LteSql = "update  `person_state` set `POSI_STATE`='" + RepUd_Lte_St.position.active + "',`POSI_X`='" + xy[0] + "', `POSI_Y`='" + xy[0] + "',`BATTERY`='" + RepUd_Lte_St.position.battery + "', `STEP`='" + RepUd_Lte_St.position.step + "', `STATE`='终端状态',`POSI_NUM`='" + (int.Parse(num) + 1) + "',ADD_TIME='" + RepUd_Lte_St.position.time + "' where `FID`='" + PersonInfo.Item1 + "'";
            }
            Mysql.UpdOrInsOrdel(Ud_LteSql);
        }

        /// <summary>
        /// 平台通用回复，不含设置参数，如[3G*XXXXXXXXXX*LEN*指令类型]
        /// </summary>
        /// <param name="HeadName">指令类型</param>
        /// <param name="F10Packet">已解码的终端发送的数据包</param>
        /// <param name="Session">终端连接session</param>
        private void CommonRsp(string HeadName, F10Packet F10Packet, F10WatchSession Session)
        {
            byte[] buffer2 = PacketFrom.F10Pack(
              new F10Packet
              {
                  FixBody = new FixBody
                  {
                      head = F10Packet.FixBody.head,
                      id = F10Packet.FixBody.id
                  },
                  content = new ReqLk().Encode(
                                    new ReqLk_St
                                    {
                                        messageId = HeadName
                                    }
                           )
              });
            Session.Send(buffer2, 0, buffer2.Length);
        }

        /// <summary>
        /// 血氧上报回复
        /// </summary>
        /// <param name="HeadName"></param>
        /// <param name="F10Packet"></param>
        /// <param name="Session"></param>
        private void RepOxygen(string HeadName, F10Packet F10Packet, F10WatchSession Session)
        {
            byte[] buffer2 = PacketFrom.F10Pack(
                 new F10Packet
                 {
                     FixBody = new FixBody
                     {
                         head = F10Packet.FixBody.head,
                         id = F10Packet.FixBody.id
                     },
                     content = new ReqOxygen().Encode(
                                       new ReqOxygen_St
                                       {
                                           messageId = HeadName,
                                           state = "0"
                                       }
                              )
                 });
            Session.Send(buffer2, 0, buffer2.Length);
        }
    }
}