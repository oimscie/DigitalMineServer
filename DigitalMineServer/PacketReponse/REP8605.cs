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
   public class REP8605
    {
        public  byte[] R8605(string sim)
        {
            byte[] body_8605 = new REQ_8605().Encode(new List<UInt32>() { });
            byte[] buffer = PacketProvider.CreateProvider().Encode(new PacketFrom()
            {
                msgBody = body_8605,
                msgId = JT808Cmd.REQ_8605,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(sim),
            });
            return buffer;
        }
    }
}
