using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.PacketBody;
using JtLibrary.PacketBody.Request;
using JtLibrary.Structures;

namespace DigitalMineServer.PacketReponse
{
    class REP0002
    {
        public void R0002(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            byte[] body_0002 = new REQ_8001().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode(new PacketFrom()
            {
                msgBody = body_0002,
                msgId = JT808Cmd.REQ_8001,
                msgSerialnumber = msg.pmPacketHead.phSerialnumber,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber,
            });
            Session.Send(buffer,0, buffer.Length);
        }
    }
}
