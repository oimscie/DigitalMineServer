using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 消息指令0x0303(信息点播/取消)
    /// </summary>
    public class REP_0303_2013
    {
        public REP_0303_2013()
        {
        }
        public PB0303 Decode(byte[] msgBody)
        {
            return new PB0303()
            {
                MessageType = msgBody[0],
                OperationIdentity = msgBody[1]
            };
        }
    }
}
