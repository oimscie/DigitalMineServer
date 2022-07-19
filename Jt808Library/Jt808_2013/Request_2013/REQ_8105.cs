/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 15:23:37
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 91e0e292-bf8f-4af9-8432-7a0a47a2c6fc
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
    /// <summary>
    /// 消息指令0x8105(终端控制)
    /// </summary>
    public class REQ_8105_2013
    {
        public REQ_8105_2013()
        {
        }

        public byte[] Encode(PB8105 info)
        {
            int len = (info.CmdParameters == null ? 1 : (info.CmdParameters.Length + 1));

            byte[] retBytes = new byte[len];
            retBytes[0] = info.Cmd;

            info.CmdParameters.CopyTo(retBytes, 1);
            return retBytes;
        }
    }
}
