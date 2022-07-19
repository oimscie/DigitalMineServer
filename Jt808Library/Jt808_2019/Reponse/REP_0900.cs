/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:46:40
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: f35b2bee-2faf-4420-8a0b-9910deb6b9b6
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
    /// 消息指令0x0900(数据上行透传)
    /// </summary>
    public class REP_0900_2019
    {
        public REP_0900_2019()
        {
        }
        public PB0900 Decode(byte[] msgBody)
        {
            return new PB0900()
            {
                PassthroughType = msgBody[0],
                PassThroughContent = msgBody.Copy(1, msgBody.Length - 1)
            };
        }
    }
}
