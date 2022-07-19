/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:47:51
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 05ed4b99-4610-44a1-91bd-f5d1b8ae371f
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Reponse_2019
{
    /// <summary>
    /// 消息指令0x0A00(终端RSA公钥{e,n})
    /// </summary>
    public class REP_0A00_2019
    {
        public REP_0A00_2019()
        {
        }
        /// <summary>
        /// 终端公钥数据体解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0A00 Decode(byte[] msgBody)
        {
            return new PB0A00()
            {
                RSA_e = msgBody.ToUInt32(0),
                RSA_n = msgBody.Copy(4, msgBody.Length - 4)
            };
        }
    }
}
