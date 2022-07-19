/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:48:35
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: d0b1e85a-b1bc-4c16-b616-06e839cb8c67
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
   public class REQ_8803_2013
    {
        /// <summary>
        /// 存储多媒体数据上传命令
        /// </summary>
        /// <param name="info">多媒体数据上传结构</param>
        /// <returns></returns>
        public byte[] Encode(PB8803 info)
        {
            //计算数组长度
            byte[] data = new byte[16];

            data[0] = info.mType;
            data[1] = info.channelId;
            data[2] = info.eventCode;
            //添加时间
            info.stime.TimeFormatToBCD().CopyTo(data, 3);
            info.etime.TimeFormatToBCD().CopyTo(data, 9);
            //删除或保留
            data[15] = info.delFlag;
            return data;
        }
    }
}
