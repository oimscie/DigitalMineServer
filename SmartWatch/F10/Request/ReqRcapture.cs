using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqRcapture
    {
        private readonly char splitChar = ',';

        public ReqRcapture()
        { }

        /// <summary>
        /// 远程拍照[3G*XXXXXXXXXX*len*rcapture]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqCenter ReqRcapture)
        {
            return ReqRcapture.messageId;
        }
    }
}