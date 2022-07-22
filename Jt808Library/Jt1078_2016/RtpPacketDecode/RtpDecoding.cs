using JtLibrary;
using JtLibrary.PacketBody;
using System;
using static JtLibrary.Structures.EquipVersion;

namespace JtLibrary.Jt1078_2016.RtpPacketDecode
{
    public class RtpDecoding
    {
        /// <summary>
        /// Rtp音视频消息体解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public Video Decode(byte[] msgBody, string type_1078)
        {
            Video item = new Video();
            int indexOffset = 0;
            item.state = msgBody.ToUInt32(indexOffset += 4);
            item.V_P_X_CC = msgBody[indexOffset];
            item.M_PT = msgBody[indexOffset += 1];
            item.num = msgBody.ToUInt16(indexOffset += 1);
            //兼容粤标10位
            if (type_1078 == Version_1078.Ver_1078_2019)
            {
                item.SIM = msgBody.Copy(indexOffset += 2, 10);
                item.ID = msgBody[indexOffset += 10];
            }
            else
            {
                byte[] temp = new byte[10];
                Buffer.BlockCopy(msgBody, indexOffset += 2, temp, 4, 6);
                item.SIM = temp;
                item.ID = msgBody[indexOffset += 6];
            }
            item.type = msgBody[indexOffset += 1];
            item.Time = msgBody.Copy(indexOffset += 1, 8);
            if (BitConvert.ByteToBit(item.type).Substring(0, 4) == "0011")
            {
                //音频
                item.length = msgBody.ToUInt16(indexOffset += 8);

                item.data = msgBody.Copy(indexOffset + 2, item.length);
            }
            else
            {
                //视频
                item.Last_I_F = msgBody.ToUInt16(indexOffset += 8);
                item.Last_F = msgBody.ToUInt16(indexOffset += 2);
                item.length = msgBody.ToUInt16(indexOffset += 2);
                item.data = msgBody.Copy(indexOffset + 2, item.length);
            }
            return item;
        }
        /// <summary>
        /// 判断1078版本，粤标SIM码10位
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public string Check1078Versioin(byte[] buffer)
        {
            if (buffer[8] == 0)
            {
                return Version_1078.Ver_1078_2019;
            }
            else
            {
                return Version_1078.Ver_1078_2016;
            }
        }
    }
}
