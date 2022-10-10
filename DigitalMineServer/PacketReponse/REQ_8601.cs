using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;

namespace DigitalMineServer.PacketReponse
{
    /// <summary>
    /// 弃用
    /// </summary>
    public class REQ_8601
    {
        public byte[] R8601(string sim)
        {
            byte[] body_8601 = new REQ_8601_2013().Encode(new List<UInt32>() { });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
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
