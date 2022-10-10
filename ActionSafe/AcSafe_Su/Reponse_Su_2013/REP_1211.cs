using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.Reponse_Su_2013
{
    /// <summary>
    /// 文件信息上传
    /// </summary>
    public class REP_1211
    {
        /// <summary>
        /// 解码文件上传消息
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PB1211 Decode(byte[] buffer)
        {
            int index = 0;
            PB1211 PB1211 = new PB1211
            {
                nameLength = buffer[index],
                fileName = Encoding.GetEncoding("GBK").GetString(buffer.Copy(index += 1, buffer[0])),
                type = buffer[index += buffer[0]],
                fileSize = buffer.ToUInt32(index += 1)
            };
            return PB1211;
        }
    }
}