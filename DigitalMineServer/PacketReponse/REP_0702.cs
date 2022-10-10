using DigitalMineServer.Mysql;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.Jt808_2013.Reponse_2013;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Jt808_2019.Reponse_2019;
using JtLibrary.Jt808_2019.Request_2019;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    class REP_0702
    {
        private readonly MySqlHelper mySql;
        public REP_0702()
        {
            mySql = new MySqlHelper();
        }

        public void R0702(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            string sim = Extension.BCDToString(msg.pmPacketHead.hSimNumber);
            switch (Resource.equipVersion[Extension.BCDToString(msg.pmPacketHead.hSimNumber)].Item1)
            {
                case Version_808.Ver_808_2013:
                    byte[] buffer_2013 = Packet_0702_2013(msg, pConvert);
                    Session.Send(buffer_2013, 0, buffer_2013.Length);
                    InsertDriver(sim, new REP_0702_2013().Decode(msg.pmMessageBody), Resource.VehicleList[sim].Item3);
                    break;
                case Version_808.Ver_808_2019:
                    byte[] buffer_2019 = Packet_0702_2019(msg, pConvert);
                    Session.Send(buffer_2019, 0, buffer_2019.Length);
                    InsertDriver(sim, new REP_0702_2019().Decode(msg.pmMessageBody), Resource.VehicleList[sim].Item3);
                    break;
            }
        }
        /// <summary>
        /// 2013消息打包
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pConvert"></param>
        /// <returns></returns>
        private byte[] Packet_0702_2013(PacketMessage msg, IPacketProvider pConvert) {
            byte[] body_0702 = new REQ_8001_2013().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2013(new PacketFrom()
            {
                msgBody = body_0702,
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
        /// 2013消息打包
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pConvert"></param>
        /// <returns></returns>
        private byte[] Packet_0702_2019(PacketMessage msg, IPacketProvider pConvert) {
            byte[] body_0702 = new REQ_8001_2019().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2019(new PacketFrom()
            {
                msgBody = body_0702,
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
        /// 司机签登存入数据库
        /// </summary>
        /// <param name="sim"></param>
        /// <param name="bodyinfo"></param>
        /// <param name="company"></param>
        private void InsertDriver(string sim, PB0702 bodyinfo, string company) {
            if (Resource.VehicleList.ContainsKey(sim))
            {
                if (bodyinfo.Status == 0x01)
                {
                    mySql.UpdOrInsOrdel("UPDATE `list_vehicle` SET `VEHICLE_DRIVER` = '" + bodyinfo.DriverName + "' where  COMPANY='" + company + "' and  VEHICLE_SIM='" + sim + "' ");
                }
                else
                {
                    mySql.UpdOrInsOrdel("UPDATE `list_vehicle` SET `VEHICLE_DRIVER` = '退签' where  COMPANY='" + company + "' and  VEHICLE_SIM='" + sim + "' ");
                }
            }
        }
    }
}
