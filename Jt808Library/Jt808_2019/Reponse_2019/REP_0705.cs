
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Reponse_2019
{
    /// <summary>
    /// 消息指令0x0705(CAN 总线数据上传)
    /// </summary>
    public class REP_0705_2019
    {
        public REP_0705_2019()
        {
        }
        /// <summary>
        /// CAN 总线数据上传
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0705 Decode(byte[] msgBody)
        {
            UInt16 count = msgBody.ToUInt16(0);
            PB0705 item = new PB0705()
            {
                CANBusDataReceptionTime = msgBody.Copy(2, 5)
            };
            UInt16 indexOffset = 7;
            item.CANItems = new List<CANItem>(count);

            for (int i = 0; i < count; ++i)
            {
                CANItem canItem = new CANItem();
                canItem.CANData = msgBody.Copy(indexOffset, 4);
                indexOffset += 4;
                canItem.CANId = msgBody.Copy(indexOffset, 8);
                indexOffset += 8;

                item.CANItems.Add(canItem);
            }

            return item;
        }
    }
}
