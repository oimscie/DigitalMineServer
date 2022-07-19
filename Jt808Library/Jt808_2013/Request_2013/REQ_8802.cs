
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
   public class REQ_8802_2013
    {
        /// <summary>
        /// 存储多媒体数据检索数据体打包 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] Encode(PB8802 info)
        {
            byte[] data = new byte[15];
            data[0] = info.mType;
            data[1] = info.channelId;
            data[2] = info.eventCode;
            info.stime.TimeFormatToBCD().CopyTo(data, 3);
            info.etime.TimeFormatToBCD().CopyTo(data, 9);
            return data;
        }
    }
}
