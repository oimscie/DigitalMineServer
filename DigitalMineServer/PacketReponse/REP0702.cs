using DigitalMineServer.Mysql;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.PacketBody;
using JtLibrary.PacketBody.Reponse;
using JtLibrary.PacketBody.Request;
using JtLibrary.Structures;

namespace DigitalMineServer.PacketReponse
{
    class REP0702
    {
        MySqlHelper MysqlHelper = new MySqlHelper();
        public void R0702(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            byte[] body_0702 = new REQ_8001().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode(new PacketFrom()
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
            Session.Send(buffer, 0, buffer.Length);
            PB0702 bodyinfo_0702 = new REP_0702().Decode(msg.pmMessageBody);
            string sim= Extension.BCDToString(msg.pmPacketHead.hSimNumber);
            if (Resource.VehicleList.ContainsKey(sim))
            {
                if (bodyinfo_0702.Status == 0x01)
                {
                    MysqlHelper.UpdOrInsOrdel("UPDATE `list_vehicle` SET `VEHICLE_DRIVER` = '" + bodyinfo_0702.DriverName + "' where  COMPANY='" + Resource.VehicleList[sim].Item3 + "' and   VEHICLE_SIM='" + sim + "' ");
                }
                else {
                    MysqlHelper.UpdOrInsOrdel("UPDATE `list_vehicle` SET `VEHICLE_DRIVER` = '已退签' where  COMPANY='" + Resource.VehicleList[sim].Item3 + "' and   VEHICLE_SIM='" + sim + "' ");
                }
            }        
        }
    }
}
