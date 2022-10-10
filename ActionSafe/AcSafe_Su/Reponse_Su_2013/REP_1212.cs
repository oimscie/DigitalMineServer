using ActionSafe.AcSafe_Su.PacketBody;
using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionSafe.AcSafe_Su.Reponse_Su_2013
{
    /// <summary>
    /// 文件上传完成消息
    /// </summary>
    public class REP_1212
    {
        /// <summary>
        /// 文件上传完成消息解码
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PB1212 Decode(byte[] buffer)
        {
            int index = 0;
            PB1212 pb1212 = new PB1212
            {
                fileNameLength = buffer[0],
                fileName = Encoding.GetEncoding("GBK").GetString(buffer.Copy(index += 1, buffer[0])),
                type = buffer[index += buffer[0]],
                fileSize = buffer.ToUInt32(index += 1)
            };
            return pb1212;
        }
    }
}