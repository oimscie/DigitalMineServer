using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqRemoveSms
    {
        private readonly char splitChar = ',';

        public ReqRemoveSms()
        { }

        /// <summary>
        ///取下手表报警开关[3G*XXXXXXXXXX*LEN*REMOVESMS,0 或1]0:关闭,1:  打开
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqRemoveSms ReqRemoveSms)
        {
            return ReqRemoveSms.messageId + splitChar + ReqRemoveSms.order;
        }
    }
}