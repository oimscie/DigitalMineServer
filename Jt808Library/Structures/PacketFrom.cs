using System;

namespace JtLibrary.Structures
{
    /// <summary>
    /// 数据打包信息结构
    /// </summary>
    public class PacketFrom
    {
        /// <summary>
        /// 消息包标志位0x7e（头标志和尾标识）共用
        /// </summary>
        public byte msgFlag { get; } = 0x7e;
        /// <summary>
        /// 消息ID
        /// </summary>
        public UInt16 msgId;
        /// <summary>
        /// 消息流水号,默认从0开始
        /// </summary>
        public UInt16 msgSerialnumber;
        /// <summary>
        /// 包总数,默认为1，只有分包才用
        /// </summary>
        public UInt16 pTotal = 1;
        /// <summary>
        /// 包序号，默认从1开始，只有分包才用
        /// </summary>
        public UInt16 pSerialnumber = 1;
        /// <summary>
        /// 加密标志,默认不加密,0不加密,否则其他加密方式
        /// </summary>
        public byte pEncryptFlag = 0;
        /// <summary>
        /// 默认不分包,0不分包，1分包
        /// </summary>
        public byte pSubFlag = 0;
        /// <summary>
        /// 此处适用2019版本十位BCD编码的SIM卡号,默认分配10个字节空间，13版本前四位默认0
        /// </summary>
        public byte[] simNumber = new byte[10];
        /// <summary>
        /// 版本标识
        /// </summary>
        public byte IdentifiersVersion;
        /// <summary>
        /// 协议版本号
        /// </summary>
        public byte protocolVersion;
        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] msgBody = null;
        /// <summary>
        /// 2013版本封包
        /// </summary>
        /// <returns></returns>
        internal byte[] Encoding_2013()
        {
            UInt16 k = 12;
            //计算包的长度
            ushort blen = (ushort)(msgBody == null ? 0 : msgBody.Length);

            //数据包总长度
            int pLen = blen + (pSubFlag == 0 ? 12 : 16);

            //分配数据长度
            byte[] buffer = new byte[pLen];

            //消息头ID
            buffer[0] = (byte)(msgId >> 8);   //存高位
            buffer[1] = (byte)msgId;          //存低位

            //消息属性
            byte[] arr = (new PacketAttribute_2013()
            {
                paEncryptFlag = pEncryptFlag,
                paSubFlag = pSubFlag,
                paMessageBodyLength = blen
            }).Encoding();

            buffer[2] = arr[0];
            buffer[3] = arr[1];
            //终端手机号,13版本前四位默认0
            Buffer.BlockCopy(simNumber, 4, buffer, 4, 6);
            //流水号
            buffer[10] = (byte)(msgSerialnumber >> 8);
            buffer[11] = (byte)msgSerialnumber;

            //判断是否分包
            if (pSubFlag != 0)
            {
                //包总数
                buffer[12] = (byte)(pTotal >> 8);
                buffer[13] = (byte)pTotal;
                //包序号
                buffer[14] = (byte)(pSerialnumber >> 8);
                buffer[15] = (byte)pSerialnumber;
                k = 16;
            }
            if (blen > 0)
            {
                msgBody.CopyTo(buffer, k);
            }

            return Escape(buffer);
        }
        /// <summary>
        /// 2019版本封包
        /// </summary>
        /// <returns></returns>
        internal byte[] Encoding_2019()
        {
            UInt16 k = 17;
            //计算包的长度
            ushort blen = (ushort)(msgBody == null ? 0 : msgBody.Length);

            //数据包总长度
            int pLen = blen + (pSubFlag == 0 ? 17 : 21);

            //分配数据长度
            byte[] buffer = new byte[pLen];

            //消息头ID
            buffer[0] = (byte)(msgId >> 8);   //存高位
            buffer[1] = (byte)msgId;          //存低位

            //消息属性
            byte[] arr = new PacketAttribute_2019()
            {
                paEncryptFlag = pEncryptFlag,
                paSubFlag = pSubFlag,
                paMessageBodyLength = blen,
                IdentifiersVersion = IdentifiersVersion
            }.Encoding();

            buffer[2] = arr[0];
            buffer[3] = arr[1];
            //协议版本号
            buffer[4] = protocolVersion;
            //手机号
            simNumber.CopyTo(buffer, 5);//10 byte
            //流水号
            buffer[15] = (byte)(msgSerialnumber >> 8);
            buffer[16] = (byte)msgSerialnumber;
            //判断是否分包
            if (pSubFlag != 0)
            {
                //包总数
                buffer[17] = (byte)(pTotal >> 8);
                buffer[18] = (byte)pTotal;
                //包序号
                buffer[19] = (byte)(pSerialnumber >> 8);
                buffer[20] = (byte)pSerialnumber;
                k = 21;
            }
            if (blen > 0)
            {
                msgBody.CopyTo(buffer, k);
            }
            return Escape(buffer);
        }
        /// <summary>
        /// 数据转义
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        internal unsafe byte[] Escape(byte[] buffer)
        {
            int i = 0, index = 1, len = buffer.Length;
            int rlen = len + 3 + (len >> 4);
            byte checkcode = buffer[0];

            fixed (byte* dst = new byte[rlen], src = buffer)
            {
                dst[0] = msgFlag;

                while (i < len)
                {
                    switch (*(src + i))
                    {
                        case 0x7e:
                            {
                                *(dst + index) = 0x7d;
                                *(dst + index + 1) = 0x02;
                                index += 2;
                            }
                            break;
                        case 0x7d:
                            {
                                *(dst + index) = 0x7d;
                                *(dst + index + 1) = 0x01;
                                index += 2;
                            }
                            break;
                        default:
                            *(dst + index) = *(src + i);
                            ++index;
                            break;
                    }

                    if (i >= 1)
                    {
                        checkcode ^= *(src + i);
                    }

                    ++i;
                }

                switch (checkcode)
                {
                    case 0x7e:
                        {
                            *(dst + index) = 0x7d;
                            *(dst + index + 1) = 0x02;
                            index += 2;
                        }
                        break;
                    case 0x7d:
                        {
                            *(dst + index) = 0x7d;
                            *(dst + index + 1) = 0x01;
                            index += 2;
                        }
                        break;
                    default:
                        {
                            *(dst + index) = checkcode;
                            ++index;
                        }
                        break;
                }

                *(dst + index) = msgFlag;
                ++index;

                byte[] nbuffer = new byte[index];
                fixed (byte* b = nbuffer)
                {
                    Nactive.Api.memcpy(b, dst, index);
                }
                return nbuffer;
            }
        }
    }
}
