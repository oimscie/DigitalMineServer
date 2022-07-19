using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Structures
{
    /// <summary>
    /// 终端各协议版本
    /// </summary>
    public class EquipVersion
    {
        /// <summary>
        /// 808版本
        /// </summary>
        enum Version_808
        {
            Ver_808_2013 = 2013,
            Ver_808_2019 = 2019
        }
        /// <summary>
        /// 1078版本
        /// </summary>
        enum Version_1078
        {
            Ver_1078_2013 = 2013,
            Ver_1078_2019 = 2019
        }
        /// <summary>
        /// 主动安全版本
        /// </summary>
        enum Version_AcSafe
        {
            Ver_AcSafe_su = 2013,
            Ver_AcSafe_yue = 2019
        }
    }
}
