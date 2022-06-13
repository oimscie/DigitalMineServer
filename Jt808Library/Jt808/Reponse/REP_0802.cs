/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:44:50
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 2ddcbff0-64b5-42c8-9f4d-4250d716cb5f
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Reponse
{
    public class REP_0802
    {
        public REP_0802()
        {
        }
        /// <summary>
        /// 多媒体检索数据体解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0802 Decode(byte[] msgBody)
        {
            PB0802 item = new PB0802()
            {
                SerialNumber = msgBody.ToUInt16(0)
            };

            int indexOffset = 2;
            UInt16 itemCount = msgBody.ToUInt16(indexOffset);

            REP_0200 body0200 = new REP_0200();
            item.MultimediaIndexItems = new List<IndexItem>(itemCount);

            for (int i = 0; i < itemCount; ++i)
            {
                if (indexOffset >= msgBody.Length) break;

                IndexItem indexItem = new IndexItem();
                //多媒体ID
                indexItem.MultimediaDataId = msgBody.ToUInt32(indexOffset += 2);
                //多媒体类型
                indexItem.MultmediaType = msgBody[indexOffset += 4];
                //通道ID
                indexItem.ChannelId = msgBody[indexOffset += 1];
                //事件项编码
                indexItem.EventItemCoding = msgBody[indexOffset += 1];
                //位置信息汇报
                indexItem.PositionInformation = body0200.Decode(msgBody.Copy(indexOffset += 1, 28));

                item.MultimediaIndexItems.Add(indexItem);

                indexOffset += 28;
            }
            return item;
        }
    }
}
