
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
   public class REQ_8500_2013
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
