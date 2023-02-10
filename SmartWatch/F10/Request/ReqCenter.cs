using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqCenter
    {
        private readonly char splitChar = ',';

        public ReqCenter()
        { }

        /// <summary>
        /// 短信报警接收号码[3G*9617624925*0006*CENTER,电话号码]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqCenter_St ReqCenter)
        {
            return ReqCenter.messageId + splitChar + ReqCenter.telephone;
        }
    }
}