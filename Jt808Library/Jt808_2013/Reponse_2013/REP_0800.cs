
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 消息指令0x0800(多媒体事件信息上传)
    /// </summary>
    public class REP_0800_2013
    {
        public REP_0800_2013()
        {
        }
        public PB0800 Decode(byte[] msgBody)
        {
            return new PB0800()
            {
                MultimediaDataId = msgBody.ToUInt32(0),
                MultmediaType = msgBody[4],
                MultimediaFormat = msgBody[5],
                EventItemCoding = msgBody[6],
                ChannelId = msgBody[7]
            };
        }
    }
}
