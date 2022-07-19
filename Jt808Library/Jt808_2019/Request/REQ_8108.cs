/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 15:26:27
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: a60d0a1b-55ea-4a7b-9560-f0ea764c4b75
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
    /// <summary>
    /// 下发终端升级数据包
    /// </summary>
    public class REQ_8108_2019
    {
        private Encoding encoding = Encoding.GetEncoding("GBK");

        public REQ_8108_2019()
        {
        }

        public byte[] Encode(PB8108 info)
        {
            byte[] v = encoding.GetBytes(info.version);
            List<byte> buffer = new List<byte>(info.manufacturersNo.Length + v.Length + info.updateData.Length + 1);

            //添加升级类型
            buffer.Add(info.updateType);
            //添加制造商ID
            buffer.AddRange(info.manufacturersNo);
            //添加版本号长度
            buffer.Add((byte)v.Length);
            //添加版本号
            buffer.AddRange(v);
            //添加升级数据长度
            UInt32 len = (UInt32)info.updateData.Length;
            buffer.Add((byte)(len >> 24));
            buffer.Add((byte)(len >> 16));
            buffer.Add((byte)(len >> 8));
            buffer.Add((byte)len);

            //添加升级数据
            buffer.AddRange(info.updateData);
            return buffer.ToArray();
        }
    }
}
