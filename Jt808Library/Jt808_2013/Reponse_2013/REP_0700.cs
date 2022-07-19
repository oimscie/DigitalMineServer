
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 消息指令0x0700(行驶记录数据上传)
    /// </summary>
    public class REP_0700_2013
    {
        public REP_0700_2013()
        {
        }
        public PB0700 Decode(byte[] msgBody)
        {
            return new PB0700()
            {
                SerialNumber = msgBody.ToUInt16(0),
                Command = msgBody[2],
                DataContent = msgBody.Copy(3, msgBody.Length - 3)
            };
        }
    }
}
