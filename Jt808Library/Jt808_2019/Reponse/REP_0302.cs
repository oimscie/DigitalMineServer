
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Reponse_2019
{
    /// <summary>
    /// 消息指令0x0203(提问应答)
    /// </summary>
    public class REP_0302_2019
    {
        public REP_0302_2019()
        {
        }
        public PB0302 Decode(byte[] msgBody)
        {
            return new PB0302()
            {
                SerialNumber = msgBody.ToUInt16(0),
                AnswerId = msgBody[2]
            };
        }
    }
}
