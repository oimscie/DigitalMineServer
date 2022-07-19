
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 消息指令0x0702(驾驶员身份信息采集上报)
    /// </summary>
    public class REP_0702_2013
    {
        private Encoding encoding = null;
        public REP_0702_2013()
        {
            encoding = Encoding.GetEncoding("GBK");
        }
        /// <summary>
        /// 驾驶员身份信息采集上报解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0702 Decode(byte[] msgBody)
        {
            int indexOffset = 0;
            PB0702 item = new PB0702()
            {
                Status = msgBody[0],
                Time = msgBody.Copy(indexOffset += 1, 6)
            };

            if (item.Status == 0x01)
            {
                item.ICReaderResult = msgBody[indexOffset += 6];

                byte len = msgBody[indexOffset += 1];
                item.DriverName = encoding.GetString(msgBody.Copy(indexOffset += 1, len));

                item.QualificationCertificateCoding = encoding.GetString(msgBody.Copy(indexOffset += len, 20));

                len = msgBody[indexOffset += 20];
                item.CertificateAuthorityName = encoding.GetString(msgBody.Copy(indexOffset += 1, len));

                item.CertificateDeadline = msgBody.Copy(indexOffset += len, 4);
            }

            return item;
        }
    }
}
