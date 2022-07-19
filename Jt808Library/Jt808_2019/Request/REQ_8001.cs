/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 15:06:28
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: cb363326-712e-42b2-88a3-1d21158d5b82
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
    /// <summary>
    /// 消息指令0x8001(平台通用应答)
    /// </summary>
    public class REQ_8001_2019
    {
        public REQ_8001_2019()
        { }

        /// <summary>
        /// 0x8001通用应答数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8001 info)
        {
            return new byte[5]
            {
                (byte)(info.Serialnumber >> 8),
                (byte)info.Serialnumber,
                (byte)(info.MessageId >> 8),
                (byte)info.MessageId,
                info.Result 
            };
        }
    }
}
