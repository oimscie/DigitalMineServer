using JtLibrary;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.Reauest_Su_2013
{
    /// <summary>
    /// 报警附件上传指令
    /// </summary>
    public class REQ_9208
    {
        /// <summary>
        /// 报警附件上传指令
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] Encoder(PB9208 info)
        {
            List<byte> list = new List<byte>
            {
                info.ipLength
            };
            list.AddRange(Encoding.GetEncoding("GBK").GetBytes(info.ip));
            list.AddRange(info.TPort.ToBytes());
            list.AddRange(info.UPort.ToBytes());
            list.AddRange(info.warnNumber);
            list.AddRange(info.warnId);
            list.AddRange(info.reserved);
            return list.ToArray();
        }
    }
}