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
        public struct Version_808
        {
            public const string Ver_808_2013 = "Jt808-2013";
            public const string Ver_808_2019 = "Jt808-2019";
            public const string Ver_808_null = "Jt808-null";
        }
        /// <summary>
        /// 1078版本
        /// </summary>
        public struct Version_1078
        {
            public const string Ver_1078_2016 = "Jt1078-2016";
            public const string Ver_1078_2019 = "Jt1078-2016->2019";
            public const string Ver_1078_null = "Jt1078-null";

        }
        /// <summary>
        /// 主动安全版本
        /// </summary>
        public struct Version_AcSafe
        {
            public const string Ver_AcSafe_su = "AcSafe-su";
            public const string Ver_AcSafe_yue = "AcSafe-ue";
            public const string Ver_AcSafe_null = "AcSafe-null";
        }
    }
}
