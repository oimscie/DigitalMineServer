/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:46:04
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: a4931a75-ac71-4dd8-9921-5a80adb46feb
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Request
{
   public class REQ_8800
    {
        /// <summary>
        /// 消息指令0x8800数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8800 info)
        {
            byte count = (byte)(info.pIdList.Count);
            int len = 4 + (info.pIdList == null ? 0 : (count << 1));

            byte[] buffer = new byte[len];
            info.MediaId.ToBytes().CopyTo(buffer, 0);

            if (len > 4)
            {
                buffer[4] = count;
                int c = 5;
                for (int i = 0; i < count; ++i)
                {
                    buffer[c] = (byte)(info.pIdList[i] >> 8);
                    buffer[c + 1] = (byte)(info.pIdList[i]);
                    ++c;
                }
            }
            return buffer;
        }
    }
}
