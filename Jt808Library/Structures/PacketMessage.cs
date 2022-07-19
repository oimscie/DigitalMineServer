
using System;
using System.Runtime.InteropServices;

namespace JtLibrary.Structures
{
    /// <summary>
    /// 消息包封装项
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketTag
    {
        /// <summary>
        /// 消息包总数
        /// </summary>
        public UInt16 ptTotal;
        /// <summary>
        ///包序号
        /// </summary>
        public UInt16 ptSerialnumber;
    }

    /// <summary>
    /// 消息体属性，兼容2019
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketAttribute
    {
        /// <summary>
        ///分包标志,1分包,0不分包
        /// </summary>
        public byte paSubFlag;
        /// <summary>
        /// 加密标志,
        /// </summary>
        public byte paEncryptFlag;
        /// <summary>
        /// 消息体长度
        /// </summary>
        public UInt16 paMessageBodyLength;
        /// <summary>
        /// 版本标识,19版加入，固定为1，依此判断终端版本
        /// </summary>
        public byte IdentifiersVersion;
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public byte[] Encoding()
        {
            UInt16 value = (UInt16)((paEncryptFlag << 10) | (paSubFlag << 13) | (IdentifiersVersion<< 14) | paMessageBodyLength);
            return new byte[2] {
              (byte)(value>>8),
              (byte)value
            };
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="pAttribute"></param>
        public void Decoding(UInt16 pAttribute)
        {
            paMessageBodyLength = (UInt16)(pAttribute & 0x03FF);
            paEncryptFlag = (byte)((pAttribute >> 10) & 0x01);
            paSubFlag = (byte)((pAttribute >> 13) & 0x01);
            IdentifiersVersion = (byte)((pAttribute >> 14) & 0x01);
        }
    }


    /// <summary>
    /// 数据包消息头结构信息，兼容2019
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketHead
    {
        /// <summary> 
        /// 消息头ID
        /// </summary>
        public UInt16 phMessageId;
        /// <summary>
        ///  消息流水号
        /// </summary>
        public UInt16 phSerialnumber;
        /// <summary>
        /// BCD码的Sim卡号,此处默认使用19版本的10字节，13版前四位默认为0
        /// </summary>
        public byte[] hSimNumber = new byte[10];
        /// <summary>
        /// 消息包封装项结构
        /// </summary>
        public PacketTag phPackeHeadTag;
        /// <summary>
        /// 消息体属性
        /// </summary>
        public PacketAttribute phPacketHeadAttribute;
        /// <summary>
        /// 协议版本号，13版默认0，19版开始关键修订递增，初始为1
        /// </summary>
        public byte protocolVersion;
    }

    /// <summary>
    /// 数据包信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PacketMessage
    {
        /// <summary>
        /// 标识位,0x7e
        /// </summary>
        public UInt16 pmFlag = 0x7e;
        /// <summary>
        /// 校验码
        /// </summary>
        public byte pmCheckcode;
        /// <summary>
        /// 消息头
        /// </summary>
        public PacketHead pmPacketHead;
        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] pmMessageBody;
    }
}
