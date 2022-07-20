using JtLibrary.PacketBody;
using System.Collections.Generic;

namespace JtLibrary.Jt1078_2016.Request_2016
{
    public class REQ_9202_2016
    {
        /// <summary>
        /// 0x9202消息体数据打包
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] Encode(PB9202 info)
        {
            List<byte> list = new List<byte>
            {
                //逻辑通道
                info.id,
                //回放控制
                info.type,
                //快进快退倍数
                info.order,
            };
            list.AddRange(info.time);
            return list.ToArray();
        }
    }
}
