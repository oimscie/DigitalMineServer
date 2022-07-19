
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 消息指令0x0704(定位数据批量上传)
    /// </summary>
    public class REP_0704_2013
    {
        public REP_0704_2013()
        {
        }
        /// <summary>
        /// 定位数据批量上传解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0704 Decode(byte[] msgBody)
        {
            REP_0200_2013 body0200 = new REP_0200_2013();
            int indexOffset = 3;
            UInt16 dlen = 0;
            UInt16 itemCount = msgBody.ToUInt16(0);

            PB0704 item = new PB0704()
            {
                LocationDataTypes = msgBody[2]
            };
            item.PositionInformationItems = new List<PB0200>(itemCount);

            for (int i = 0; i < itemCount; ++i)
            {
                //位置汇报数据长度
                dlen = msgBody.ToUInt16(indexOffset);
                //解析位置汇报数据
                item.PositionInformationItems.Add(body0200.Decode(msgBody.Copy(indexOffset += 2, dlen)));

                indexOffset += dlen;
            }

            return item;
        }
    }
}
