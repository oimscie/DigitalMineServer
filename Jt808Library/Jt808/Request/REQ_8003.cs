/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 15:13:41
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 0aefca0b-d8ab-4b7c-bf98-1603f3e07788
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Request
{
    /// <summary>
    /// 消息指令0x8003 补传分包请求
    /// </summary>
    public class REQ_8003
    {
        public REQ_8003()
        {
        }

        /// <summary>
        /// 补传包请求数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8003 info)
        {
            byte[] buffer = new byte[((byte)info.IDList.Count << 1) + 3];

            buffer[0] = (byte)(info.Serialnumber >> 8);
            buffer[1] = (byte)info.Serialnumber;

            buffer[2] = (byte)info.IDList.Count;

            int c = 3;
            for (int i = 0; i < buffer[2]; ++i)
            {
                buffer[i + c] = (byte)(info.IDList[i] >> 8);
                buffer[i + (c += 1)] = (byte)info.IDList[i];
            }
            return buffer;
        }
    }
}
