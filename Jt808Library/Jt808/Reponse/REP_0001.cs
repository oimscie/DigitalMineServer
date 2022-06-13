/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:25:27
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 59a00fa1-eedb-4d9f-862c-dcb08bd2a59f
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Reponse
{
    /// <summary>
    /// 消息指令0x0001(终端通用应答)
    /// </summary>
    public class REP_0001
    {
        public REP_0001()
        {
        }

        public PB0001 Decode(byte[] msgBody)
        {
            return new PB0001()
            {
                serialNumber = msgBody.ToUInt16(0),
                messageId = msgBody.ToUInt16(2),
                result = msgBody[4]
            };
        }
    }
}
