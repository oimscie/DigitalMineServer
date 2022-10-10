using ActionSafe.AcSafe_Su.PacketBody;
using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionSafe.AcSafe_Su.Reauest_Su_2013
{
    /// <summary>
    /// 文件上传完成消息应答
    /// </summary>
    public class REQ_9212
    {
        public byte[] Encoder(PB9212 info)
        {
            List<byte> list = new List<byte>
            {
                info.fileNameLength
            };

            list.AddRange(Encoding.GetEncoding("GBK").GetBytes(info.fileName));

            list.Add(info.type);

            list.Add(info.result);

            list.Add(info.fillCount);
            for (int i = 0; i < info.fillStructureList.Count; i++)
            {
                list.AddRange(info.fillStructureList[i].offset.ToBytes());
                list.AddRange(info.fillStructureList[i].length.ToBytes());
            }
            return list.ToArray();
        }
    }
}