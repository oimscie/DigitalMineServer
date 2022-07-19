using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt1078_2016.Request
{
   public class REQ_9102_2016
    {
        /// <summary>
        /// 0x9102消息体数据打包
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] Encode(PB9102 info)
        {
            List<byte> list = new List<byte>
            {
                  //逻辑通道
                info.id,
                //i控制指令
                info.order,
                 //数据类型
                info.type,
                 //码流类型
                info.datatypes
            };
            return list.ToArray();
        }
    }
}
