using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.Decode
{
    public class Decode
    {
        /// <summary>
        /// 盲区监测报警类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetBlindAreaWarnInfo(byte id)
        {
            switch (id)
            {
                case 0x01:
                    return "后方接近报警";

                case 0x02:
                    return "左侧后方接近报警";

                case 0x03:
                    return "右侧后方接近报警";

                default:
                    return "未知盲区监测报警";
            }
        }

        /// <summary>
        ///  获取驾驶状态监测系统报警信息类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDriveHelpWarnType(byte id)
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

        /// <summary>
        /// 获取驾驶员监测系统报警类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDriverStateWarnType(byte id)
        {
            switch (id)
            {
                case 0x01:
                    return "疲劳驾驶报警";

                case 0x02:
                    return "拨打电话报警";

                case 0x03:
                    return "抽烟报警";

                case 0x04:
                    return "分神驾驶报警";

                case 0x05:
                    return "驾驶员异常报警";

                case 0x10:
                    return "自动抓拍事件";

                case 0x11:
                    return "驾驶员变更事件";

                default:
                    return "未知驾驶员监测报警";
            }
        }

        /// <summary>
        /// 获取警报级别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetWarnLevel(byte id)
        {
            switch (id)
            {
                case 0x01:
                    return "一级警报";

                case 0x02:
                    return "二级警报";

                default:
                    return "未知等级";
            }
        }

        /// <summary>
        /// 解码报警标识号
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public WarnNumber DecodeWarnNumber(byte[] buffer)
        {
            int index = 0;
            WarnNumber item = new WarnNumber
            {
                ID = buffer.Copy(index, 7),
                Time = buffer.Copy(index += 7, 6),
                Number = buffer[index += 6],
                FileCount = buffer[index += 1]
            };
            return item;
        }
    }
}