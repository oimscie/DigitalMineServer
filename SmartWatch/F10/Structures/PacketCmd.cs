using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Structures
{
    /// <summary>
    /// F10原始包体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct F10Packet
    {
        /// <summary>
        /// F10固定格式包头
        /// </summary>
        public FixBody FixBody;

        /// <summary>
        /// 内容
        /// </summary>
        public string content;
    }

    /// <summary>
    /// F10固定格式（[包头*设备ID*内容长度*内容]的前三项）
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FixBody
    {
        /// <summary>
        /// 包头
        /// </summary>
        public string head;

        /// <summary>
        /// 设备id
        /// </summary>
        public string id;

        /// <summary>
        /// 内容长度，无需设置，封包时自动计算
        /// </summary>
        public string length;
    }

    /// <summary>
    /// F10消息类型
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct F10Cmd
    {
        #region 消息类型

        /// <summary>
        /// 心跳
        /// </summary>
        public const String LK = "LK";

        /// <summary>
        /// 位置数据上报
        /// </summary>
        public const String UD_LTE = "UD_LTE";

        /// <summary>
        /// 报警数据上报(平台无需回复)
        /// </summary>
        public const String AL_LTE = "AL_LTE";

        /// <summary>
        /// 盲区补偿数据上报(平台无需回复)
        /// </summary>
        public const String UD2_LTE = "UD2_LTE";

        /// <summary>
        /// 上传间隔设置
        /// </summary>
        public const String UPLOAD = "UPLOAD";

        /// <summary>
        /// 获取天气
        /// </summary>
        public const String WT = "WT";

        /// <summary>
        /// IP 端口设置
        /// </summary>
        public const String IP = "IP";

        /// <summary>
        /// SOS 号码设置
        /// </summary>
        public const String SOS = "SOS";

        /// <summary>
        /// 监听
        /// </summary>
        public const String MONITOR = "MONITOR";

        /// <summary>
        /// SOS 短信报警开关
        /// </summary>
        public const String SOSSMS = "SOSSMS";

        /// <summary>
        /// 低电短信报警开关
        /// </summary>
        public const String LOWBAT = "LOWBAT";

        /// <summary>
        /// 短信报警接收号码
        /// </summary>
        public const String CENTER = "CENTER";

        /// <summary>
        /// 取下手表短信报警开关
        /// </summary>
        public const String REMOVESMS = "REMOVESMS";

        /// <summary>
        ///取下手环报警开关
        /// </summary>
        public const String REMOVE = "REMOVE";

        /// <summary>
        ///远程拍照平台下发
        /// </summary>
        public const String rcapture = "rcapture";

        /// <summary>
        ///远程拍照终端上传
        /// </summary>
        public const String img = "img";

        /// <summary>
        /// 体温间隔测量下发协议
        /// </summary>
        public const String bodytemp = "bodytemp";

        /// <summary>
        /// 心率协议（同时测量心率、血压、血氧）
        /// </summary>
        public const String hrtstart = "hrtstart";

        /// <summary>
        /// 心率血压数据同时上传
        /// </summary>
        public const String bphrt = "bphrt";

        /// <summary>
        /// 血氧上报
        /// </summary>
        public const String oxygen = "oxygen";

        /// <summary>
        /// 体温上报
        /// </summary>
        public const String btemp2 = "btemp2";

        #endregion 消息类型
    }
}