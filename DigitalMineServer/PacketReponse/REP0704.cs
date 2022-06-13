using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.PacketBody;
using JtLibrary.PacketBody.Reponse;
using JtLibrary.PacketBody.Request;
using JtLibrary.Structures;
using System;

namespace DigitalMineServer.PacketReponse
{
    class REP0704
    {
        public void R0704(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            //补传
            byte[] body_0704 = new REQ_8001().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode(new PacketFrom()
            {
                msgBody = body_0704,
                msgId = JT808Cmd.REQ_8001,
                msgSerialnumber = msg.pmPacketHead.phSerialnumber,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber,
            });
            Session.Send(buffer, 0, buffer.Length);
            try
            {
                PB0704 bodyinfo_0704 = new REP_0704().Decode(msg.pmMessageBody);
                for (int i = 0; i < bodyinfo_0704.PositionInformationItems.Count; i++)
                {

                    Resource.InsertQueues.Enqueue(new ValueTuple<string, PB0200>
                    {
                        Item1 = Extension.BCDToString(msg.pmPacketHead.hSimNumber),
                        Item2 = bodyinfo_0704.PositionInformationItems[i]
                    });
                }
            }
            catch
            {

            }
        }
    }
}
