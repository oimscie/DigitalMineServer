namespace ActionSafe.AcSafe_Su.WarnInfo
{
    /// <summary>
    /// 获取警报级别
    /// </summary>
    public class WarnLevel
    {
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
    }
}