using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.Structures
{
    public class Comprehensive
    {
        /// <summary>
        /// redis键类型后缀
        /// </summary>
        public struct Redis_key_ext
        {
            /// <summary>
            /// 禁出电子围栏
            /// </summary>
            public const string fench_out = "-f-o";

            /// <summary>
            /// 禁入电子围栏
            /// </summary>
            public const string fench_in = "-f-i";

            /// <summary>
            /// 打卡围栏
            /// </summary>
            public const string clock_in = "-c-i";

            /// <summary>
            /// 车辆信息
            /// </summary>
            public const string vehicle = "-v";

            /// <summary>
            /// 人员信息
            /// </summary>
            public const string person = "-p";

            /// <summary>
            /// 主动安全报警
            /// </summary>
            public const string acSafeWarn = "-a-w";

            /// <summary>
            /// 终端版本号
            /// </summary>
            public const string equipVersion = "-e-v";
        }
    }
}