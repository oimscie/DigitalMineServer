
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
   public class REQ_8900_2013
    {

        /// <summary>
        /// 数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8900 info)
        {
            byte[] data = new byte[info.content.Length + 1];
            data[0] = info.mType;
            info.content.CopyTo(data, 1);
            return data;
        }
    }
}
