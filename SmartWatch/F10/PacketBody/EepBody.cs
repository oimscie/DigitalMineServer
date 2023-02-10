using SmartWatch.F10.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.PacketBody
{
    #region F10协议（终端上传）该协议中所有数据都按照[包头*设备ID*内容长度*内容]格式,其中包头标识固定为两个字节,内容长度固定为四个字节的 ASSII 码, 高位在前地位在后, 例如 000A表示长度为 10，其中 包头*设备ID*内容长度为固定格式

    /// <summary>
    /// 心跳[3G*XXXXXXXXXX*LEN*LK,步数,翻滚次数,电量百分数]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepLk_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;

        /// <summary>
        /// 步数
        /// </summary>
        public string step;

        /// <summary>
        /// 翻滚次数
        /// </summary>
        public string roll;

        /// <summary>
        /// 电量
        /// </summary>
        public string battery;
    }

    /// <summary>
    /// 位置数据上报[3G*XXXXXXXXXX*LEN*UD_LTE,位置数据]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepUd_Lte_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;

        /// <summary>
        /// 位置数据
        /// </summary>
        public Position_St position;
    }

    /// <summary>
    /// 位置数据
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Position_St
    {
        /// <summary>
        /// 时间-045524，(时分秒)04 点 55 分 24 秒（格式时间，北京时间 需+8）
        /// </summary>
        public DateTime time;

        /// <summary>
        /// 是否定位，A:定位 V:未定位
        /// </summary>
        public string active;

        /// <summary>
        /// 纬度，31.871384
        /// </summary>
        public string lat;

        /// <summary>
        /// 纬度标识N 表示北纬,S 表示南纬
        /// </summary>
        public string latType;

        /// <summary>
        /// 经度，117.32
        /// </summary>
        public string lon;

        /// <summary>
        /// 经度标识
        /// </summary>
        public string lonType;

        /// <summary>
        /// 速度
        /// </summary>
        public string speed;

        /// <summary>
        /// 电量
        /// </summary>
        public string battery;

        /// <summary>
        /// 步数
        /// </summary>
        public string step;

        /// <summary>
        /// 终端状态
        /// </summary>
        public string state;

        /// <summary>
        /// 定位精度
        /// </summary>
        public string accuracy;
    }

    /// <summary>
    /// 盲区补偿数据上报[3G*XXXXXXXXXX*LEN*UD2_LTE,位置数据]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepUd2_Lte_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;

        /// <summary>
        /// 位置数据
        /// </summary>
        public Position_St position;
    }

    /// <summary>
    /// 报警数据上报[3G*XXXXXXXXXX*LEN*AL_LTE,位置数据]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepAl_Lte_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;

        /// <summary>
        /// 位置数据
        /// </summary>
        public string position;
    }

    /// <summary>
    /// 上传间隔设置[3G*XXXXXXXXXX*LEN*UPLOAD]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepUpload_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// SOS号码设置[3G*9617624925*0003*SOS]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepSos_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// SOS短信报警开关[3G*9617624925*0006*SOSSMS]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepSosSms_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// 短信报警接收号码[3G*9617624925*0006*CENTER]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepCenter_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// 低电短信报警开关[3G*XXXXXXXXXX*LEN*LOWBAT]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepLowBat_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// 取下手环报警开关[3G*XXXXXXXXXX*LEN*REMOVE]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepRemove_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// 取下手表报警开关[3G*XXXXXXXXXX*LEN*REMOVESMS]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepRemoveSms_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// 远程拍照[3G*XXXXXXXXXX*len*img,5,y,z]，参数 Y 表示 ：时间（年月日时分秒： 221112144812） 参数 Z 为照片内容。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepImg_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;

        /// <summary>
        /// 待定
        /// </summary>
        public string unknown;

        /// <summary>
        /// 时间
        /// </summary>
        public string time;

        /// <summary>
        /// 照片内容
        /// </summary>
        public string container;
    }

    /// <summary>
    /// 心率协议（同时测量心率、血压、血氧）[3G*9617624925*0008*hrtstart]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepHrtStart_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// 心率血压数据同时上传（同时测量心率、血压、血氧）[3G*XXXXXXXXXX*len*bphrt,参数 1：高压,参数 2：低压,参数 3：心率,参数 4,参数 5,参数 6,参数 7]，数
    /// 值为 0 代表无效
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepBphrt_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;

        /// <summary>
        /// 参数 1：高压：数值为 0 代表无效
        /// </summary>
        public string high;

        /// <summary>
        /// 参数 2：低压：数值为 0 代表无效。
        /// </summary>
        public string low;

        /// <summary>
        /// 参数 3：心率：数值为 0 代表无效
        /// </summary>
        public string HeartRate;
    }

    /// <summary>
    /// 血氧上报协议[3G*XXXXXXXXXX*LEN*oxygen,type,oxy]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepOxygen_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;

        /// <summary>
        ///测量类型
        /// </summary>
        public string type;

        /// <summary>
        /// 血氧值(double 类型)
        /// </summary>
        public string oxy;
    }

    /// <summary>
    /// 体温间隔测量下发协议[3G*XXXXXXXXXX*LEN*bodytemp]
    /// arg2 ： 2 :间隔时间，单位小时，取值： 1-12（夜间模式不上报）
    /// arg1 ： 0 :间隔测量关闭 1 :间隔测量开启
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepBodyTemp_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    /// <summary>
    /// 体温异常提醒设置下发协议[3G*XXXXXXXXXX*LEN*BTWARNSET]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RepBtWarnSet_St
    {
        /// <summary>
        ///  固定格式
        /// </summary>
        public FixBody fixBody;

        /// <summary>
        /// 信息id
        /// </summary>
        public string messageId;
    }

    #endregion F10协议（终端上传）该协议中所有数据都按照[包头*设备ID*内容长度*内容]格式,其中包头标识固定为两个字节,内容长度固定为四个字节的 ASSII 码, 高位在前地位在后, 例如 000A表示长度为 10，其中 包头*设备ID*内容长度为固定格式
}