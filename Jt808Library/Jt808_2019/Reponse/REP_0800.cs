/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:43:09
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 720befc7-b26a-48f6-9815-cb4e4e4ae557
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JtLibrary.Jt808_2019.Reponse_2019
{
    /// <summary>
    /// 消息指令0x0800(多媒体事件信息上传)
    /// </summary>
    public class REP_0800_2019
    {
        public REP_0800_2019()
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
