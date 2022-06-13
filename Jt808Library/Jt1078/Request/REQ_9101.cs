using JtLibrary.PacketBody;
using System.Collections.Generic;
using System.Text;

namespace JtLibrary.Jt1078.Request
{
   public class REQ_9101
    {
        private readonly Encoding encoding = Encoding.GetEncoding("GBK");
        /// <summary>
        /// 0x9101消息体数据打包
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] Encode(PB9101 info)
        {
            List<byte> list = new List<byte>
            {
                //ip长度
                info.length
            };
            //ip
            list.AddRange(encoding.GetBytes(info.ip));
            //tcp端口号
            list.AddRange(info.port.ToBytes());
            //udp端口号
            list.AddRange(info.ports.ToBytes());
            //逻辑通道号
            list.Add(info.id);
            //数据类型
            list.Add(info.datatype);
            //码流类型
            list.Add(info.datatypes);
            return list.ToArray();
        }
    }
}
