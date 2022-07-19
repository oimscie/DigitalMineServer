using DigitalMineServer.implement;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.PacketBody;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Structures;
using System;
using JtLibrary.Jt808_2013.Reponse_2013;

namespace DigitalMineServer.PacketReponse
{
    class REP0200
    {
        private readonly REP_0200_2013 PB0200X = new REP_0200_2013();
        private PB0200 bodyinfo;
        public void R0200(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            byte[] body_0200 = new REQ_8001_2013().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode(new PacketFrom()
            {
                msgBody = body_0200,
                msgId = JT808Cmd.REQ_8001,
                msgSerialnumber = msg.pmPacketHead.phSerialnumber,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber,
            });
            Session.Send(buffer, 0, buffer.Length);
            bodyinfo = PB0200X.Decode(msg.pmMessageBody);
            Resource.InsertQueues.Enqueue(new ValueTuple<string, PB0200>
            {
                Item1 = Extension.BCDToString(msg.pmPacketHead.hSimNumber),
                Item2 = bodyinfo
            });
        }
    }
}
