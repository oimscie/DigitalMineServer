
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
   public class REQ_8500_2019
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
