/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:51:30
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: a68971e5-9e25-4926-b0ce-e552b20c690e
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
   public class REQ_8805_2019
    {
        /// <summary>
        /// 单条存储多媒体数据检索上传命令数据打包
        /// </summary>
        /// <param name="mediaId">多媒体ID,必需大于0</param>
        /// <param name="deleteFlag">0：保留；1：删除</param>
        /// <returns></returns>
        public byte[] Encode(PB8805 info)
        {
            byte[] buffer = new byte[5];
            info.mediaId.ToBytes().CopyTo(buffer, 0);
            buffer[4] = info.deleteFlag;
            return buffer;
        }
    }
}
