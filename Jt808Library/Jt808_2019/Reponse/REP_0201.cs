/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:32:19
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 3ec3eaf3-2be3-4e83-aa91-75d73f04a1fb
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
    /// 位置信息查询应答
    /// </summary>
    public class REP_0201_2019
    {
        public REP_0201_2019()
        {
        }
        public PB0201 Decode(byte[] msgBody)
        {
            PB0201 item = new PB0201()
            {
                SerialNumber = msgBody.ToUInt16(0)
            };

            REP_0200_2019 body0200 = new REP_0200_2019();
            item.PositionInformation = body0200.Decode(msgBody.Copy(2, msgBody.Length - 2));

            return item;
        }
    }
}
