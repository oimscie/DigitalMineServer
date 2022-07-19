using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.PacketBody;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.PacketReponse
{
   public class REP8601
    {
        public  byte[] R8601(string sim)
        {
            byte[] body_8601 = new REQ_8601_2013().Encode(new List<UInt32>() { });
            byte[] buffer = PacketProvider.CreateProvider().Encode(new PacketFrom()
            {
                msgBody = body_8601,
                msgId = JT808Cmd.REQ_8601,
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
