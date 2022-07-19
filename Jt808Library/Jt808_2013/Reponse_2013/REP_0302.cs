/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:35:31
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 6ef456f2-ef20-4c54-8756-402a84eccd99
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
    /// 消息指令0x0203(提问应答)
    /// </summary>
    public class REP_0302_2013
    {
        public REP_0302_2013()
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
