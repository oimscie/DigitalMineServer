using System;
using System.Runtime.InteropServices;

namespace JtLibrary.PacketBody
{
    #region JT1078协议 Receive操作结构信息

    /// <summary>
    /// 实时音视频传输请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB9101
    {
        /// <summary>
        /// 服务器ip地址长度
        /// </summary>
        public byte length;
        /// <summary>
        /// 服务器ip
        /// </summary>
        public string ip;
        /// <summary>
        /// 服务器端口号TCP
        /// </summary>
        public UInt16 port;
        /// <summary>
        /// 服务器端口号UDP
        /// </summary>
        public UInt16 ports;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte id;
        /// <summary>
        /// 数据类型
        /// </summary>
        public byte datatype;
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte datatypes;
    }


    /// <summary>
    /// 视频回放传输请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB9201
    {
        /// <summary>
        /// 服务器ip地址长度
        /// </summary>
        public byte length;
        /// <summary>
        /// 服务器ip
        /// </summary>
        public string ip;
        /// <summary>
        /// 服务器端口号TCP
        /// </summary>
        public UInt16 port;
        /// <summary>
        /// 服务器端口号UDP
        /// </summary>
        public UInt16 ports;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte id;
        /// <summary>
        /// 音视频类型
        /// </summary>
        public byte datatype;
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte datatypes;
        /// <summary>
        /// 回放方式
        /// </summary>
        public byte ReviewType;
        /// <summary>
        /// 存储类型
        /// </summary>
        public byte memoryType;
        /// <summary>
        /// 快进或快退倍数
        /// </summary>
        public byte FastOrSlow;
        /// <summary>
        /// 开始时间
        /// </summary>
        public byte[] StartTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public byte[] OverTime;
    }


    /// <summary>
    /// 实时音视频传输控制
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB9102
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte id;
        /// <summary>
        /// 控制指令
        /// </summary>
        public byte order;
        /// <summary>
        /// 操作类型
        /// </summary>
        public byte type;
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte datatypes;
    }
    /// <summary>
    /// 历史音视频传输控制
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB9202
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte id;
        /// <summary>
        ///回放控制
        /// </summary>
        public byte type;
        /// <summary>
        /// 快进快退倍数
        /// </summary>
        public byte order;
        /// <summary>
        /// 拖动回放位置
        /// </summary>
        public byte[] time;
    }
    /// <summary>
    /// 音视频
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Video
    {
        /// <summary>
        /// 帧头标识
        /// </summary>
        public UInt32 state;
        /// <summary>
        /// V_P_X_CC；2bits++bits+1bits+4bits
        /// </summary>
        public byte V_P_X_CC;
        /// <summary>
        /// M_PT；1bits+7bits
        /// </summary>
        public byte M_PT;
        /// <summary>
        /// 包序号
        /// </summary>
        public uint num;
        /// <summary>
        /// SIM卡号
        /// </summary>
        public byte[] SIM;
        /// <summary>
        /// 逻辑通道
        /// </summary>
        public byte ID;
        /// <summary>
        ///数据类型，分包处理标记；4bits+4bits
        /// </summary>
        public byte type;
        /// <summary>
        /// 时间戳；8位
        /// </summary>
        public byte[] Time;
        /// <summary>
        /// 与上一关键帧时间间隔，数据类型为0100时没有该字段
        /// </summary>
        public UInt16 Last_I_F;
        /// <summary>
        /// 与上一关时间间隔，数据类型为0100时没有该字段
        /// </summary>
        public UInt16 Last_F;
        /// <summary>
        /// 数据体长度
        /// </summary>
        public UInt16 length;
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] data;
    }
    #endregion
}
