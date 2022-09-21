using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ActionSafe.AcSafe_Su.PacketBody
{
    public class PacketBody
    {
        /// <summary>
        /// 苏标主动安全0200附加信息警报指令
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct AcSafeSuWarn
        {
            /// <summary>
            /// 高级驾驶辅助系统报警
            /// </summary>
            public const byte DriveHelp = 0x64;

            /// <summary>
            /// 驾驶员状态监测系统报警
            /// </summary>
            public const byte DriverState = 0x65;

            /// <summary>
            /// 胎压监测系统报警
            /// </summary>
            public const byte TirePressure = 0x66;

            /// <summary>
            /// 盲区检测报警
            /// </summary>
            public const byte BlindArea = 0x67;
        }

        /// <summary>
        /// 高级驾驶辅助系统报警信息格式
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PB0X64
        {
            /// <summary>
            /// 报警ID
            /// </summary>
            public UInt32 ID;

            /// <summary>
            /// 标志状态（0x00：不可用，0x01：开始，0x02：结束）
            /// </summary>
            public byte WarnState;

            /// <summary>
            /// 报警/事件类型
            /// 0x01:前向碰撞报警
            /// 0x02:车道偏离报警
            /// 0x03:车距过近报警
            /// 0x04:行人碰撞报警
            /// 0x05:频繁变道报警
            /// 0x06:道路标识超限报警
            /// 0x07:障碍物报警
            /// 0x10:道路标志识别事件
            /// 0x11:主动抓拍事件
            /// 0x12:实线变道报警
            /// 0x13:车厢过道行人检测报警
            /// </summary>
            public byte WarnType;

            /// <summary>
            /// 报警级别
            /// 0x01:一级报警
            /// 0x02:二级报警
            /// </summary>
            public byte WarnLevel;

            /// <summary>
            /// 前车车速，单位KM/h,仅警报类型为0x01,0x02,0x04时有效
            /// </summary>
            public byte FrontVehicleSpeed;

            /// <summary>
            /// 前车/人距离，单位100ms，仅警报类型为0x01,0x02,0x04时有效
            /// </summary>
            public byte FrontDistance;

            /// <summary>
            /// 偏离类型，仅警报类型0x02时有效
            /// 0x01：左侧偏离
            /// 0x02：右侧偏离
            /// </summary>
            public byte DeviateType;

            /// <summary>
            /// 道路标志识别类型，仅警报类型0x06和0x10时有效
            /// 0x01：限速标志
            /// 0x02：限高标志
            /// 0x03：险种标识
            /// </summary>
            public byte RoadSignType;

            /// <summary>
            /// 道路识别标识数据
            /// </summary>
            public byte RoadSignData;

            /// <summary>
            /// 车速 KM/h
            /// </summary>
            public byte VehicleSpeed;

            /// <summary>
            /// 高程
            /// </summary>
            public UInt16 High;

            /// <summary>
            ///纬度，以度为单位的纬度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 latitude;

            /// <summary>
            /// 经度，以度为单位的经度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 longitude;

            /// <summary>
            /// 时间，6位BCD
            /// </summary>
            public byte[] Time;

            /// <summary>
            /// 车辆状态
            /// </summary>
            public UInt16 VehicleState;

            /// <summary>
            /// 报警标识号，16位
            /// </summary>
            public byte[] WarnNumber;
        }

        /// <summary>
        /// 驾驶状态监测系统报警信息数据格式
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PB0X65
        {
            /// <summary>
            /// 报警 ID,按照报警先后，从 0 开始循环累加，不区分报警类型
            /// </summary>
            public UInt32 ID;

            /// <summary>
            /// 标志状态（0x00：不可用，0x01：开始，0x02：结束）
            /// </summary>
            public byte WarnState;

            /// <summary>
            /// 报警/事件类型
            /// 0x01:疲劳驾驶报警
            /// 0x02:接打手持电话报警
            /// 0x03:抽烟报警
            /// 0x04:不目视前方报警
            /// 0x05:驾驶员异常报警
            /// 0x06:驾驶员异常报警
            /// 0x07:用户自定义
            /// 0x08:超时驾驶报警
            /// 0x09:用户自定义
            /// 0x0A:未系安全带报警
            /// 0x0B:红外阻断型墨镜失效报警
            /// 0x0C:双脱把报警（双手同时脱离方向盘）
            /// 0x0D:玩手机报警
            /// 0x10:自动抓拍事件
            /// 0x11:驾驶员变更事件
            /// </summary>
            public byte WarnType;

            /// <summary>
            /// 报警级别
            /// 0x01:一级报警
            /// 0x02:二级报警
            /// </summary>
            public byte WarnLevel;

            /// <summary>
            /// 疲劳程度,范围 1-9， 数值越大表示疲劳程度越严重，仅在报警类型为 0x01 时有效
            /// </summary>
            public byte FatigueLevel;

            /// <summary>
            /// 预留,4位
            /// </summary>
            public byte[] Reserved;

            /// <summary>
            /// 车速 KM/h
            /// </summary>
            public byte VehicleSpeed;

            /// <summary>
            /// 高程
            /// </summary>
            public UInt16 High;

            /// <summary>
            ///纬度，以度为单位的纬度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 latitude;

            /// <summary>
            /// 经度，以度为单位的经度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 longitude;

            /// <summary>
            /// 时间，6位BCD
            /// </summary>
            public byte[] Time;

            /// <summary>
            /// 车辆状态
            /// </summary>
            public UInt16 VehicleState;

            /// <summary>
            /// 报警标识号，16位
            /// </summary>
            public byte[] WarnNumber;
        }

        /// <summary>
        ///  胎压监测系统报警信息数据格式
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PB0X66
        {
            /// <summary>
            /// 报警 ID,按照报警先后，从 0 开始循环累加，不区分报警类型
            /// </summary>
            public UInt32 ID;

            /// <summary>
            /// 标志状态（0x00：不可用，0x01：开始，0x02：结束）
            /// </summary>
            public byte WarnState;

            /// <summary>
            /// 车速 KM/h
            /// </summary>
            public byte VehicleSpeed;

            /// <summary>
            /// 高程
            /// </summary>
            public UInt16 High;

            /// <summary>
            ///纬度，以度为单位的纬度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 latitude;

            /// <summary>
            /// 经度，以度为单位的经度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 longitude;

            /// <summary>
            /// 时间，6位BCD
            /// </summary>
            public byte[] Time;

            /// <summary>
            /// 车辆状态
            /// </summary>
            public UInt16 VehicleState;

            /// <summary>
            /// 报警标识号，16位
            /// </summary>
            public byte[] WarnNumber;

            /// <summary>
            /// 报警/事件列表总 数
            /// </summary>
            public byte EventCount;

            /// <summary>
            /// 报警/事件信息列表
            /// </summary>
            public List<PB0X66Eventlist> TirePressure_Event_list;
        }

        /// <summary>
        /// 胎压监测系统报警/事件信息列表格式
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PB0X66Eventlist
        {
            /// <summary>
            /// 报警轮胎位置编号（从左前轮开始以 Z 字形从 00 依次编号，编号与是否安装 TPMS 无关）
            /// </summary>
            public byte Number;

            /// <summary>
            /// 报警/事件类型
            /// 0 表示无报警， 1 表示有报警
            /// bit0：胎压（定时上报）
            /// bit1：胎压过高报警
            /// bit2：胎压过低报警
            /// bit3：胎温过高报警
            /// bit4：传感器异常报警
            /// bit5：胎压不平衡报警
            /// bit6：慢漏气报警
            /// bit7：电池电量低报警
            /// bit8~bit15：自定义
            /// </summary>
            public UInt16 EventType;

            /// <summary>
            /// 胎压,单位 Kpa
            /// </summary>
            public UInt16 TirePressure;

            /// <summary>
            /// 胎温,单位 ℃
            /// </summary>
            public UInt16 TireTemperature;

            /// <summary>
            /// 单位 ℃,单位 %
            /// </summary>
            public UInt16 battery;
        }

        /// <summary>
        /// 盲区监测系统报警定义数据格式
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PB0X67
        {
            /// <summary>
            /// 报警 ID,按照报警先后，从 0 开始循环累加，不区分报警类型
            /// </summary>
            public UInt32 ID;

            /// <summary>
            /// 标志状态（0x00：不可用，0x01：开始，0x02：结束）
            /// </summary>
            public byte WarnState;

            /// <summary>
            /// 报警/事件类型
            /// 0x01：后方接近报警
            /// 0x02：左侧后方接近报警
            /// 0x03： 右侧后方接近报警
            /// </summary>
            public UInt16 EventType;

            /// <summary>
            /// 车速 KM/h
            /// </summary>
            public byte VehicleSpeed;

            /// <summary>
            /// 高程
            /// </summary>
            public UInt16 High;

            /// <summary>
            ///纬度，以度为单位的纬度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 latitude;

            /// <summary>
            /// 经度，以度为单位的经度值乘以10的6次方，精确到百万分之一
            /// </summary>
            public UInt32 longitude;

            /// <summary>
            /// 时间，6位BCD
            /// </summary>
            public byte[] Time;

            /// <summary>
            /// 车辆状态
            /// </summary>
            public UInt16 VehicleState;

            /// <summary>
            /// 报警标识号，16位
            /// </summary>
            public byte[] WarnNumber;
        }

        /// <summary>
        /// 报警标识号格式
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct WarnNumber
        {
            /// <summary>
            /// 30 个字节， 由大写字母和数字组成
            /// </summary>
            public byte[] ID;

            /// <summary>
            /// 时间，6位BCD
            /// </summary>
            public byte[] Time;

            /// <summary>
            /// 序号,同一时间点报警的序号， 从 0 循环累加
            /// </summary>
            public byte Number;

            /// <summary>
            /// 附件数量,表示该报警对应的附件数量
            /// </summary>
            public byte FileCount;
        }

        /// <summary>
        /// 外设立即拍照指令平台向终端下发 0x8801 立即拍照指令，终端使用 0x0805 回应平台
        /// </summary>
        public struct PB8801
        {
            /// <summary>
            /// 通道 ID
            /// 0x00~0x25： 主机使用摄像头通道进行拍照
            /// 0x64： 控制ADAS拍照
            /// 0x64： 控制ADAS拍照
            /// </summary>
            public byte ID;

            /// <summary>
            /// 拍摄命令,0 表示停止拍摄。 0xFFFF 表示录像。 其他表示拍照张数，仅主机拍照时有效
            /// </summary>
            public UInt16 Order;

            /// <summary>
            /// 拍照间隔/录像时间,秒， 0 表示按最下间隔拍照或一直录像，仅主机拍照时有效
            /// </summary>
            public UInt16 IntervalTime;

            /// <summary>
            /// 保存标志,仅主机拍照时有效
            /// 1：保存
            /// 0：实时上传
            /// </summary>
            public byte Mark;

            /// <summary>
            /// 分辨率,仅主机拍照时有效
            /// 0x01:320*240
            /// 0x02:640*480
            /// 0x03:800*600
            /// 0x04:1024*768
            /// 0x05:176*144， [Qcif]
            /// 0x06:352*288， [Cif]
            /// 0x07:704*288， [HALF D1]
            /// 0x08:704*576， [D1]
            /// </summary>
            public byte Resolution;

            /// <summary>
            /// 图像/视频质量,1-10， 1 代表质量损失最下， 10 表示压缩比例最大，仅主机拍照时有效
            /// </summary>
            public byte Quality;

            /// <summary>
            /// 亮度,0-255，仅主机拍照时有效
            /// </summary>
            public byte Brightness;

            /// <summary>
            /// 对比度,0-127,机拍照时有效
            /// </summary>
            public byte Contrast;

            /// <summary>
            /// 对比度,0-127，仅主机拍照时有效
            /// </summary>
            public byte Saturation;

            /// <summary>
            /// 色度,0-255，仅主机拍照时有效
            /// </summary>
            public byte Chroma;
        }

        /// <summary>
        /// 报警附件上传指令
        /// </summary>
        public struct PB9208
        {
            /// <summary>
            /// 附件服务器ip长度
            /// </summary>
            public byte ipLength;

            /// <summary>
            /// 附件服务器ip地址
            /// </summary>
            public string ip;

            /// <summary>
            /// 附件服务器tcp端口
            /// </summary>
            /// <param name=""></param>
            /// <returns></returns>
            public UInt16 TPort;

            /// <summary>
            /// 附件服务器udp端口
            /// </summary>
            public UInt16 UPort;

            /// <summary>
            /// 16位报警标识号（由报警信息携带）
            /// </summary>
            public byte[] warnNumber;

            /// <summary>
            /// 32位报警编号（平台分配的唯一编号）
            /// </summary>
            public byte[] warnId;

            /// <summary>
            /// 16位预留，
            /// </summary>
            public byte[] reserved;
        }

        /// <summary>
        /// 报警附件信息消息
        /// </summary>
        public struct PB1210
        {
            /// <summary>
            /// 7位终端ID，厂商自定义
            /// </summary>
            public byte[] id;

            /// <summary>
            /// 16位报警标识号，报警信息携带
            /// </summary>
            public byte[] waenNumber;

            /// <summary>
            /// 32位报警编号，平台分配的唯一编号
            /// </summary>
            public byte[] warnId;

            /// <summary>
            /// 信息类型
            /// 0x00:正常报警文件信息
            /// 0x01:补传报警信息文件
            /// </summary>
            public byte[] type;

            /// <summary>
            /// 附加数量
            /// </summary>
            public byte attachmentCount;

            /// <summary>
            /// 附件信息列表
            /// </summary>
            public attachmentInfoStructure list;
        }

        /// <summary>
        /// 附件信息列表结构
        /// </summary>
        public struct attachmentInfoStructure
        {
            /// <summary>
            /// 文件名称长度
            /// </summary>
            public byte length;

            /// <summary>
            /// 文件名称字符串
            /// </summary>
            public string fileName;

            /// <summary>
            /// 文件大小
            /// </summary>
            public UInt32 fileSize;
        }

        /// <summary>
        /// 文件信息上传
        /// </summary>
        public struct PB1211
        {
            /// <summary>
            /// 文件名称长度k
            /// </summary>
            public byte nameLength;

            /// <summary>
            /// 文件名称
            /// </summary>
            public string fileName;

            /// <summary>
            /// 文件类型
            /// 0x00：图片
            /// 0x01：音频
            /// 0x02：视频
            /// 0x03：文本
            /// 0x04：其他
            /// </summary>
            public byte type;

            /// <summary>
            /// 文件大小
            /// </summary>
            public UInt32 fileSize;
        }

        /// <summary>
        /// 文件码流
        /// </summary>
        public struct FileStream
        {
            /// <summary>
            /// 帧头标识 0x30,0x31,0x63,0x64
            /// </summary>
            public UInt32 head;

            /// <summary>
            /// 50位文件名称
            /// </summary>
            public byte[] fileName;

            /// <summary>
            /// 数据偏移量
            /// </summary>
            public UInt32 offest;

            /// <summary>
            /// 数据体长度
            /// </summary>
            public UInt32 length;

            /// <summary>
            /// 数据体，默认长度64K，文件小于64K则为实际长度
            /// </summary>
            public byte[] fileBody;
        }
    }

    /// <summary>
    /// 文件上传完成信息
    /// </summary>
    public struct PB1212
    {
        /// <summary>
        /// 文件名称长度K
        /// </summary>
        public byte fileNameLength;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string fileName;

        /// <summary>
        /// 文件类型
        /// 0x00：图片
        /// 0x01：音频
        /// 0x02：视频
        /// 0x03：文本
        /// 0x04：其他
        /// </summary>
        public byte type;

        /// <summary>
        /// 文件大小
        /// </summary>
        public UInt32 fileSize;
    }

    /// <summary>
    /// 文件上传文成消息应答
    /// </summary>
    public struct PB9212
    {
        /// <summary>
        /// 文件名称长度K
        /// </summary>
        public byte fileNameLength;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string fileName;

        /// <summary>
        /// 上传结构
        /// 0x00：完成
        /// 0x01：需要补传
        /// </summary>
        public byte result;

        /// <summary>
        /// 补传数据包数量
        /// </summary>
        public byte fillCount;

        /// <summary>
        /// 补传数据包列表
        /// </summary>
        public fillStructure fillStructureList;
    }

    /// <summary>
    /// 补传数据包结构
    /// </summary>
    public struct fillStructure
    {
        /// <summary>
        /// 数据偏移量
        /// </summary>
        public UInt32 offset;

        /// <summary>
        /// 数据长度
        /// </summary>
        public UInt32 length;
    }
}