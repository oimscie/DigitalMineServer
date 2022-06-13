/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:12:44
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: 1009595b-7748-412b-9061-5c65b037d022
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Request
{
   public class REQ_8500
    {
        /// <summary>
        /// 车辆控制数据体打包
        /// </summary>
        /// <param name="flag">0表示车门解锁,1表示车门加锁</param>
        /// <returns></returns>
        public byte[] Encode(byte flag)
        {
            return new byte[]
            {
                (byte)(flag&0x01)
            };
        }
    }
}
