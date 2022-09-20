using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionSafe.AcSafe_Su.WarnInfo
{
    /// <summary>
    /// 获取驾驶状态监测系统报警
    /// </summary>
    public class DriveHelpWarn
    {
        /// <summary>
        ///  获取驾驶状态监测系统报警信息类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  string GetDriveHelpWarnType(byte id)
        {
            switch (id)
            {
                case 0x01:
                    return "前向碰撞报警";
                case 0x02:
                    return "车道偏离报警";
                case 0x03:
                    return "车距过近报警";
                case 0x04:
                    return "行人碰撞报警";
                case 0x05:
                    return "频繁变道报警";
                case 0x06:
                    return "道路标识超限报警";
                case 0x07:
                    return "障碍物报警";
                case 0x10:
                    return "道路标志识别事件";
                case 0x11:
                    return "主动抓拍事件";
                case 0x12:
                    return "实线变道报警";
                case 0x13:
                    return "车厢过道行人检测报警";
                default:
                    return "未知驾驶状态报警";
            }
        }
    }
}
