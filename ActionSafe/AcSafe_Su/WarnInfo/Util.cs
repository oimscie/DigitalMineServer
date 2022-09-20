using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.WarnInfo
{
    /// <summary>
    /// 通用解析
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 解码报警标识号
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public WarnNumberBody DecodeWarnNumber(byte[] buffer) {
            int index = 0;
            WarnNumberBody item = new WarnNumberBody
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
