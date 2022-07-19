/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:27:58
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 4b30a7c2-4d04-4f42-a660-5c7b8fd403b6
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 终端鉴权接收解析
    /// </summary>
    public class REP_0102_2013
    {
        private Encoding encoding = null;
        public REP_0102_2013()
        {
            encoding = Encoding.GetEncoding("GBK");
        }
        /// <summary>
        /// 终端鉴权数据体解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0102 Decode(byte[] msgBody)
        {
            return new PB0102()
            {
                AuthenticationCode = encoding.GetString(msgBody)
            };
        }
    }
}
