using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqSos
    {
        private readonly char splitChar = ',';

        public ReqSos()
        { }

        /// <summary>
        ///SOS号码设置[3G*9617624925*0003*SOS,电话号码,电话号码,电话号码]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqSos ReqSos)
        {
            return ReqSos.messageId + splitChar + ReqSos.telephone1 + splitChar + ReqSos.telephone2 + splitChar + ReqSos.telephone3;
        }
    }
}