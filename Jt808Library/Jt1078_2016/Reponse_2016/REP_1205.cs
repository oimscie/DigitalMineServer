using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
   public class REP_1205
    {
        /// <summary>
        /// 终端音视频资源列表解码
        /// </summary>
        public PB1205 Decode(byte[] msgBody) {
            return new PB1205() {
                serialNumber = msgBody.ToUInt16(0),
                count = msgBody.ToUInt32(2)
            };
        }
    }
}
