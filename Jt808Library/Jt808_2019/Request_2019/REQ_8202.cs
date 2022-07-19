
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
    /// <summary>
    /// 临时位置跟踪控制
    /// </summary>
    public class REQ_8202_2019
    {
        public REQ_8202_2019()
        {
        }

        public byte[] Encode(PB8202 info)
        {
            byte[] buffer = new byte[(info.tInterval == 0 ? 2 : 6)];
            buffer[0] = (byte)(info.tInterval >> 8);
            buffer[1] = (byte)info.tInterval;

            if (info.tInterval != 0)
            {
                buffer[2] = (byte)(info.tValidTime >> 24);
                buffer[3] = (byte)(info.tValidTime >> 16);
                buffer[4] = (byte)(info.tValidTime >> 8);
                buffer[5] = (byte)info.tValidTime;
            }
            return buffer;
        }
    }
}
