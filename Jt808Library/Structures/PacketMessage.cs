/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 11:17:25
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 13d73536-65f2-4954-9557-70a17ebdcb4f
---------------------------------------------------------------*/
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
    /// 消息体属性
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
        /// 编码
        /// </summary>
        /// <returns></returns>
        public byte[] Encoding()
        {
            UInt16 value = (UInt16)((paEncryptFlag << 10) | (paSubFlag << 13) | paMessageBodyLength);
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
        }
    }

    /// <summary>
    /// 数据包消息头结构信息
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
        /// BCD码的Sim卡号,默认分配6个字节空间
        /// </summary>
        public byte[] hSimNumber = new byte[6];
        /// <summary>
        /// 消息包封装项结构
        /// </summary>
        public PacketTag phPackeHeadTag;
        /// <summary>
        /// 消息体属性
        /// </summary>
        public PacketAttribute phPacketHeadAttribute;
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
