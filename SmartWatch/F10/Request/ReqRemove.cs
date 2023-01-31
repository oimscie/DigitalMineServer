using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqRemove
    {
        private readonly char splitChar = ',';

        public ReqRemove()
        { }

        /// <summary>
        ///取下手环报警开关[3G*XXXXXXXXXX*LEN*REMOVE,0 或1]0:关闭,1:  打开
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqRemove ReqRemove)
        {
            return ReqRemove.messageId + splitChar + ReqRemove.order;
        }
    }
}