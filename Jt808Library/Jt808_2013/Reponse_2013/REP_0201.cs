
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 位置信息查询应答
    /// </summary>
    public class REP_0201_2013
    {
        public REP_0201_2013()
        {
        }
        public PB0201 Decode(byte[] msgBody)
        {
            PB0201 item = new PB0201()
            {
                SerialNumber = msgBody.ToUInt16(0)
            };

            REP_0200_2013 body0200 = new REP_0200_2013();
            item.PositionInformation = body0200.Decode(msgBody.Copy(2, msgBody.Length - 2));

            return item;
        }
    }
}
