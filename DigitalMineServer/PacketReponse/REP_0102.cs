using DigitalMineServer.Mysql;
using DigitalMineServer.Redis;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.Util;
using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Jt808_2019.Request_2019;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using JtLibrary.Utils;
using System;
using System.Collections.Generic;
using static DigitalMineServer.Structures.Comprehensive;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    internal class REP_0102
    {
        private readonly RedisHelper redis;
        private readonly MySqlHelper mysql;

        public REP_0102()
        {
            mysql = new MySqlHelper();
            redis = new RedisHelper();
        }

        public void R0102(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            string sim = Extension.BCDToString(msg.pmPacketHead.hSimNumber);
            //判断808版本
            string Version808 = VersionCheck.Get808Version(msg.pmPacketHead.phPacketHeadAttribute.IdentifiersVersion);
            if (Version808 == Version_808.Ver_808_null)
            {
                return;
            }
            //查找服务器存储的1078版本与主动安全版本
            string sql = "select EQUIP_1078_TYPE as equip_1078,EQUIP_AcSafe_TYPE as AcSafe from list_vehicle where VEHICLE_SIM='" + sim + "'";
            List<Dictionary<string, string>> list = mysql.MultipleSelect(sql, new List<string>() { "equip_1078", "AcSafe" });
            if (list == null)
            {
                //判断是否是人员
                sql = "select EQUIP_1078_TYPE as equip_1078 from list_person where PERSON_SIM='" + sim + "'";
                list = mysql.MultipleSelect(sql, new List<string>() { "equip_1078" });
                if (list == null)
                {
                    return;
                }
                else
                {
                    //人员不存在主动安全模块，置空
                    list[0]["AcSafe"] = "";
                }
            }
            string Version_1078 = VersionCheck.Get1078Version(list[0]["equip_1078"]);
            string Version_AcSafe = VersionCheck.GetAcSafeVersion(list[0]["AcSafe"]);
            ValueTuple<string, string, string, int> val = new ValueTuple<string, string, string, int>
            {
                Item1 = Version808,
                Item2 = Version_1078,
                Item3 = Version_AcSafe,
                Item4 = msg.pmPacketHead.protocolVersion
            };
            //存入字典
            redis.Set(sim + Redis_key_ext.equipVersion, Utils.Util.ObjectSerializ(val), -1);
            //回复终端
            switch (msg.pmPacketHead.phPacketHeadAttribute.IdentifiersVersion)
            {
                case 0:
                    byte[] buffer_2013 = Packet_0102_2013(msg, pConvert);
                    Session.Send(buffer_2013, 0, buffer_2013.Length);
                    break;

                case 1:
                    byte[] buffer_2019 = Packet_0102_2019(msg, pConvert);
                    Session.Send(buffer_2019, 0, buffer_2019.Length);
                    break;
            }
            //连接头设置终端SIM
            Session.Sim = sim;
        }

        /// <summary>
        /// 2013版打包消息
        /// </summary>
        private byte[] Packet_0102_2013(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0102 = new REQ_8001_2013().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2013(new PacketFrom()
            {
                msgBody = body_0102,
                msgId = JT808Cmd.REQ_8001,
                msgSerialnumber = msg.pmPacketHead.phSerialnumber,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber,
            });
            return buffer;
        }

        /// <summary>
        /// 2019版打包消息
        /// </summary>
        private byte[] Packet_0102_2019(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0102 = new REQ_8001_2019().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2019(new PacketFrom()
            {
                msgBody = body_0102,
                msgId = JT808Cmd.REQ_8001,
                msgSerialnumber = msg.pmPacketHead.phSerialnumber,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber,
            });
            return buffer;
        }
    }
}