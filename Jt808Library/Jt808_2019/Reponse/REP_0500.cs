
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Reponse_2019
{
    /// <summary>
    /// 消息指令0x0500(车辆控制应答)
    /// </summary>
    public class REP_0500_2019
    {
        public REP_0500_2019()
        {
        }
        /// <summary>
        /// 车辆控制应答解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0500 Decode(byte[] msgBody)
        {
            PB0500 item = new PB0500()
            {
                SerialNumber = msgBody.ToUInt16(0)
            };

            REP_0200_2019 body0200 = new REP_0200_2019();
            item.PositionInformation = body0200.Decode(msgBody.Copy(2, msgBody.Length - 2));

            return item;
        }
    }
}
