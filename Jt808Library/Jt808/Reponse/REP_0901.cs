/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:47:21
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: e09af8b7-e24a-472a-91d0-674f3966efa4
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Reponse
{
    /// <summary>
    /// 消息指令0x0901(数据压缩上报)
    /// </summary>
    public class REP_0901
    {
        public REP_0901()
        {
        }
        /// <summary>
        /// (GZip压缩)压缩的数据
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0901 Decode(byte[] msgBody)
        {
            return new PB0901()
            {
                CompressData = msgBody.Copy(4, msgBody.Length - 4)
            };
        }
    }
}
