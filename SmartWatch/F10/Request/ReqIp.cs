using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqIp
    {
        private readonly char splitChar = ',';

        public ReqIp()
        { }

        /// <summary>
        /// IP设置[3g*XXXXXXXXXX*LEN*IP,IP或域名,端口]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqIp ReqIp)
        {
            return ReqIp.messageId + splitChar + ReqIp.ip + splitChar + ReqIp.port;
        }
    }
}