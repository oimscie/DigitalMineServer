using JtLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JtLibrary.Structures.EquipVersion;

namespace JtLibrary.Utils
{
   public static class VersionCheck
    {
        /// <summary>
        /// 判别808版本
        /// </summary>
        /// <param name="IdentifiersVersion">版本标识</param>
        /// <returns></returns>
        public static string Get808Version(byte IdentifiersVersion) {
            switch (IdentifiersVersion) {
                case 1:
                    return Version_808.Ver_808_2019;
                case 0:
                    return Version_808.Ver_808_2013;
                default:
                    return Version_808.Ver_808_null;
            }
        }
        /// <summary>
        /// 判别1078版本
        /// </summary>
        /// <param name="type">版本类型</param>
        /// <returns></returns>
        public static string Get1078Version(string type)
        {
            switch (type)
            {
                case "2016":
                    return Version_1078.Ver_1078_2016;
                case "2016-1019":
                    return Version_1078.Ver_1078_2019;//粤标改变了终端SIM码的位数，执行808-2019版本10位码
                default:
                    return Version_1078.Ver_1078_null;
            }
        }
        /// <summary>
        ///  判别主动安全版本
        /// </summary>
        /// <param name="type">版本类型</param>
        /// <returns></returns>
        public static string GetAcSafeVersion(string type)
        {
            switch (type)
            {
                case "粤标-2019":
                    return Version_AcSafe.Ver_AcSafe_yue;
                case "苏标-2013":
                    return Version_AcSafe.Ver_AcSafe_su;
                default:
                    return Version_AcSafe.Ver_AcSafe_null;
            }
        }
    }
}
