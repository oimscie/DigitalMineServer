/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:26:38
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 8ec6f0b3-9565-477e-a499-2f8df922e9b5
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    ///<summary>
    /// 终端注册
    ///</summary>
    public class REP_0100_2013
    {
        private Encoding encoding = null;
        public REP_0100_2013()
        {
            encoding = Encoding.GetEncoding("GBK");
        }

        /// <summary>
        /// 终端注册数据包解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0100 Decode(byte[] msgBody)
        {
            return new PB0100()
            {
                ProvincialId = msgBody.ToUInt16(0),
                CityId = msgBody.ToUInt16(2),
                ManufacturerId = msgBody.Copy(4, 5),
                TerminalModel = msgBody.Copy(9, 20),
                TerminalId = msgBody.Copy(29, 7),
                ColorPlates = msgBody[36],
                VehicleIdentification = encoding.GetString(msgBody.Copy(37, msgBody.Length - 37))
            };
        }
    }
}
