using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionSafe.AcSafe_Su.WarnInfo
{
    /// <summary>
    /// 盲区监测报警
    /// </summary>
    public class BlindAreaWarn
    {
        /// <summary>
        /// 盲区监测报警类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  string GetBlindAreaWarnInfo(byte id)
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
    }
}
