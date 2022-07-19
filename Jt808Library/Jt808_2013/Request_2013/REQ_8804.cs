
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
   public class REQ_8804_2013
    {
        /// <summary>
        /// 消息指令0x8804数据体打包
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] Encode(PB8804 info)
        {
            return new byte[5]
            {
            info.recordingCmd,
            (byte)(info.recordingTime >> 8),
            (byte)info.recordingTime,
            info.recordingSaveFlag,
            info.recordingSampleFre
            };
        }
    }
}
