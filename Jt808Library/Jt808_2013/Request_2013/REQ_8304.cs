/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:09:10
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: d4f4edc0-8a48-4355-a58e-befc6887f021
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
    public class REQ_8304_2013
    {
        private Encoding encoding = null;
        public REQ_8304_2013()
        {
            encoding= Encoding.GetEncoding("GBK");
        }
        /// <summary>
        ///  信息服务数据体打包
        /// </summary>
        /// <param name="info">,Value:消息类型，StringValue:消息内容</param>
        /// <returns></returns>
        public byte[] Encode(ByteString info)
        {
            byte[] txt = encoding.GetBytes(info.StringValue);
            byte[] data = new byte[txt.Length + 3];
            //信息类型
            data[0] = info.Value;

            //信息长度
            UInt16 mlen = (UInt16)txt.Length;
            data[1] = (byte)(mlen >> 8);
            data[2] = (byte)mlen;

            //添加信息内容
            txt.CopyTo(data, 3);
            return data;
        }
    }
}
