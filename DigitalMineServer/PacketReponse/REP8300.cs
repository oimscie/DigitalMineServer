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
    class REP8300
    {
        public  byte[] R8300(string[] data)
        {
            byte[] body_8300 = new REQ_8300().Encode(new PB8300()
            {
                EmFlag = 1,
                displayScreen = 1,
                tts = 1,
                adScreen = 1,
                msgType = 0,
                msgContent = data[3],
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode(new PacketFrom()
            {
                msgBody = body_8300,
                msgId = JT808Cmd.REQ_8300,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(data[1]),
            });
            return buffer;
        }
    }
}
