
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 0x0200消息指令解析(位置信息汇报)
    /// </summary>
    public class REP_0200_2013
    {
        public REP_0200_2013()
        {
        }
        /// <summary>
        /// 终端位置信息汇报消息体解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0200 Decode(byte[] msgBody)
        {
            PB0200 item = new PB0200();
            int indexOffset = 0;
            byte id, len;

            item.AlarmIndication = msgBody.ToUInt32(indexOffset);

            item.StatusIndication = msgBody.ToUInt32(indexOffset += 4);

            item.Latitude = msgBody.ToUInt32(indexOffset += 4);

            item.Longitude = msgBody.ToUInt32(indexOffset += 4);

            item.Altitude = msgBody.ToUInt16(indexOffset += 4);

            item.Speed = msgBody.ToUInt16(indexOffset += 2);

            item.Direction = msgBody.ToUInt16(indexOffset += 2);

            item.LocationTime = msgBody.Copy(indexOffset += 2, 6);

            indexOffset += 6;

            //解析附加信息体
            int blen = msgBody.Length - 1;
            if (blen > indexOffset)
            {
                item.AttachItems = new List<ByteBytes>(blen >> 1);
                while (blen > indexOffset)
                {
                    //附加信息ID
                    id = msgBody[indexOffset];
                    if (id != 0xEB)
                    {
                        //附加信息长度
                        len = msgBody[indexOffset += 1];
                        if (len == 0) continue;
                        item.AttachItems.Add(new ByteBytes()
                        {
                            Value = id,
                            BytesValue = msgBody.Copy(indexOffset += 1, len)
                        });
                        indexOffset += len;
                    }
                    else
                    {
                        byte[] temp = msgBody.Copy(indexOffset += 2, msgBody.Length - indexOffset);
                        indexOffset = 0;
                        int blens = (temp.Length - 1);
                        while (blens > indexOffset)
                        {
                            len = temp[indexOffset += 1];
                            if (len == 0) continue;
                            item.AttachItems.Add(new ByteBytes()
                            {
                                Value = temp[indexOffset += 2],
                                BytesValue = temp.Copy(indexOffset += 1, len - 2)
                            });
                            indexOffset += len - 2;
                        }
                        blen = 0;
                    }
                }
            }
            return item;
        }
    }
}
