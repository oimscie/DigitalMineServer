using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqSosSms
    {
        private readonly char splitChar = ',';

        public ReqSosSms()
        { }

        /// <summary>
        ///  SOS短信报警开关[3G*9617624925*0006*SOSSMS,0 或1]0:关闭,1:  打开
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqSosSms ReqSosSms)
        {
            return ReqSosSms.messageId + splitChar + ReqSosSms.order;
        }
    }
}