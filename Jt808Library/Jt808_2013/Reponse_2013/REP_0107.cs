/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:30:05
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 3306b40c-a953-4430-9b61-db4a4fc0582e
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 0x0107消息指令解析(查询终端属性应答)
    /// </summary>
    public class REP_0107_2013
    {
        private Encoding encoding = null;

        public REP_0107_2013()
        {
            encoding = Encoding.GetEncoding("GBK");
        }
        /// <summary>
        /// MSGBody模块属性
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] MSGBodyAttribute(byte data)
        {
            return new byte[]
            { 
             (byte)(data & 0x0001),
             (byte)((data & 0x0002)>>1),
             (byte)((data & 0x0004)>>2),
             (byte)((data & 0x0008)>>3)
            };
        }

        /// <summary>
        /// 查询终端属性应答解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0107 Decode(byte[] msgBody)
        {
            int indexOffset = 0;
            byte len = 0;

            PB0107 item = new PB0107()
            {
                TerminalType = msgBody.ToUInt16(indexOffset),
                ManufacturerId = msgBody.Copy(indexOffset += 2, 5),
                TerminalModel = msgBody.Copy(indexOffset += 5, 20),
                TerminalId = msgBody.Copy(indexOffset += 20, 7),
                TerminalSIM = msgBody.Copy(indexOffset += 7, 10)
            };

            len = msgBody[indexOffset];
            item.TerminalHardwareVersionNumber = encoding.GetString(msgBody.Copy(indexOffset += 1, len));

            len = msgBody[indexOffset += len];
            item.TerminalFirmwareVersionNumber = encoding.GetString(msgBody.Copy(indexOffset += 1, len));

            item.GnssModuleProperties = msgBody[indexOffset += len];

            item.CommunicationModuleProperties = msgBody[indexOffset += 1];

            return item;
        }
    }
}
