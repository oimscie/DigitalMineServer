/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:47:15
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: 7121d2c1-431a-45cf-a3c8-b2083667dc67
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
   public class REQ_8802_2019
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
