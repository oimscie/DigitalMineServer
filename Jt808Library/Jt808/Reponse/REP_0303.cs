/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:37:32
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 421caf78-146c-4227-9a65-31ff8b9c8ace
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Reponse
{
    /// <summary>
    /// 消息指令0x0303(信息点播/取消)
    /// </summary>
    public class REP_0303
    {
        public REP_0303()
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
