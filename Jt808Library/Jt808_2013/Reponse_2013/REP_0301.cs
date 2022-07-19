
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 事件报告
    /// </summary>
    public class REP_0301_2013
    {
        public REP_0301_2013()
        {
        }
        public PB0301 Decode(byte[] msgBody)
        {
            return new PB0301()
            {
                EventId = msgBody[0]
            };
        }
    }
}
