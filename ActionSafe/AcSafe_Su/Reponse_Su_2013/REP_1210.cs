using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.Reponse_Su_2013
{
    public class REP_1210
    {
        public PB1210 Decode(byte[] buffer)
        {
            int index = 0;
            PB1210 PB1210 = new PB1210
            {
                id = buffer.Copy(index, 7),
                waenNumber = buffer.Copy(index += 7, 16),
                warnId = buffer.Copy(index += 16, 32),
                type = buffer[index += 32],
                attachmentCount = buffer[index + 1]
            };
            PB1210.list = decode(buffer.Copy(index += 1, buffer.Length - index), PB1210.attachmentCount);
            return PB1210;
        }

        /// <summary>
        /// 解码附件信息列表
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<attachmentInfoStructure> decode(byte[] buffer, int count)
        {
            List<attachmentInfoStructure> list = new List<attachmentInfoStructure>();
            int index = 0;
            int temp = 0;
            while (temp < count)
            {
                attachmentInfoStructure item = new attachmentInfoStructure
                {
                    length = buffer[index],
                    fileName = Encoding.GetEncoding("GBK").GetString(buffer.Copy(index += 1, buffer[0])),
                    fileSize = buffer.ToUInt32(index += buffer[0])
                };
                index += 4;
                temp++;
                list.Add(item);
            }
            return list;
        }
    }
}