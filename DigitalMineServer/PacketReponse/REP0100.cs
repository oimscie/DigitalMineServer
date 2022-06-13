using DigitalMineServer.implement;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.PacketBody;
using JtLibrary.PacketBody.Request;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.PacketReponse
{
    class REP0100
    {
        public  void R0100(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            byte[] body_0100 = new REQ_8100().Encode(new PB8100()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                Result = 0,
                AuthenticationCode = "111111"
            });
            byte[] buffer = pConvert.Encode(new PacketFrom()
            {
                msgBody = body_0100,
                msgId = JT808Cmd.REQ_8100,
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
