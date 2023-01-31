using SmartWatch.F10.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.PacketBody
{
    #region F10协议（平台下发及回复）该协议中所有数据都按照[包头*设备ID*内容长度*内容]格式,其中包头标识固定为两个字节,内容长度固定为四个字节的 ASSII 码, 高位在前地位在后, 例如 000A表示长度为 10

    /// <summary>
    /// 心跳[3G*XXXXXXXXXX*LEN*LK]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqLk
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
    /// 上传间隔设置[3G*9617624925*000A*UPLOAD,600]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqUpload
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
        /// 时间S
        /// </summary>
        public string time;
    }

    /// <summary>
    /// IP设置[3g*XXXXXXXXXX*LEN*IP,IP或域名,端口]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqIp
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
        /// IP或域名
        /// </summary>
        public string ip;

        /// <summary>
        /// 端口
        /// </summary>
        public string port;
    }

    /// <summary>
    /// SOS号码设置[3G*9617624925*0003*SOS,电话号码,电话号码,电话号码]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqSos
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
        /// 电话号码
        /// </summary>
        public string telephone1;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string telephone2;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string telephone3;
    }

    /// <summary>
    /// SOS短信报警开关[3G*9617624925*0006*SOSSMS,0 或1]0:关闭,1:  打开
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqSosSms
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
        /// 开关0或1
        /// </summary>
        public string order;
    }

    /// <summary>
    /// 短信报警接收号码[3G*9617624925*0006*CENTER,电话号码]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqCenter
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
        /// 电话号码
        /// </summary>
        public string telephone;
    }

    /// <summary>
    /// 低电短信报警开关[3G*XXXXXXXXXX*LEN*LOWBAT,0 或1]0:关闭,1:  打开
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqLowBat
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
        /// 开关0或1
        /// </summary>
        public string order;
    }

    /// <summary>
    /// 取下手环报警开关[3G*XXXXXXXXXX*LEN*REMOVE,0 或1]0:关闭,1:  打开
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqRemove
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
        /// 开关0或1
        /// </summary>
        public string order;
    }

    /// <summary>
    /// 取下手表报警开关[3G*XXXXXXXXXX*LEN*REMOVESMS,0 或1]0:关闭,1:  打开
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqRemoveSms
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
        /// 开关0或1
        /// </summary>
        public string order;
    }

    /// <summary>
    /// 远程拍照[3G*XXXXXXXXXX*len*rcapture]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqRcapture
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
    /// 心率协议（同时测量心率、血压、血氧）[3G*XXXXXXXXXX*len*hrtstart,X]
    /// 说明： x 为上传间隔时间，单位秒,连续上传时最小时间不小于 300秒，最大不超过 65535。
    /// x 为 1 则代表终端心率单次上传，上传完后自动关闭。 x 为 0 则代表终端心率上传关闭。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqHrtStart
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
        /// 上传间隔时间，1代表终端心率单次上传，上传完后自动关闭。 0则代表终端心率上传关闭。连续上传时最小时间不小于 300秒，最大不超过 65535。
        /// </summary>
        public string order;
    }

    /// <summary>
    /// 体温间隔测量下发协议[3G*XXXXXXXXXX*LEN*bodytemp,arg1， arg2]
    /// arg2 ： 2 :间隔时间，单位小时，取值： 1-12（夜间模式不上报）
    /// arg1 ： 0 :间隔测量关闭 1 :间隔测量开启
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqBodyTemp
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
        ///间隔时间,单位小时
        /// </summary>
        public string arg1;

        /// <summary>
        /// 0 :间隔测量关闭 1 :间隔测量开启
        /// </summary>
        public string arg2;
    }

    /// <summary>
    /// 体温异常提醒设置下发协议[3G*XXXXXXXXXX*LEN*BTWARNSET,lowbt,highbt,open,type,tel]
    /// arg2 ： 2 :间隔时间，单位小时，取值： 1-12（夜间模式不上报）
    /// arg1 ： 0 :间隔测量关闭 1 :间隔测量开启
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqBtWarnSet
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
        ///低温提醒临界点(double 数据类型)
        /// </summary>
        public string lowbt;

        /// <summary>
        /// 低温提醒临界点(double 数据类型)
        /// </summary>
        public string highbt;

        /// <summary>
        ///开关： 0：关 1：开
        /// </summary>
        public string open;

        /// <summary>
        /// 提醒类型： 0：短信 1：电话
        /// </summary>
        public string type;

        /// <summary>
        ///提醒接收号码
        /// </summary>
        public string telephone;
    }

    /// <summary>
    /// 血氧上报协议[3G*XXXXXXXXXX*LEN*oxygen,status]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReqOxygen
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
        ///响应状态码  1：正常 0：处理异常 -2：参数错误
        /// </summary>
        public string state;
    }

    #endregion F10协议（平台下发及回复）该协议中所有数据都按照[包头*设备ID*内容长度*内容]格式,其中包头标识固定为两个字节,内容长度固定为四个字节的 ASSII 码, 高位在前地位在后, 例如 000A表示长度为 10
}