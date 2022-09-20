using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionSafe.AcSafe_Su.WarnInfo
{
    /// <summary>
    /// 获取驾驶员监测系统报警
    /// </summary>
    public class DriverStateWarn
    {
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
    }
}
