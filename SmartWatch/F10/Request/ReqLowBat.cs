using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqLowBat
    {
        private readonly char splitChar = ',';

        public ReqLowBat()
        { }

        /// <summary>
        ///低电短信报警开关[3G*XXXXXXXXXX*LEN*LOWBAT,0 或1]0:关闭,1:  打开
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqLowBat ReqLowBat)
        {
            return ReqLowBat.messageId + splitChar + ReqLowBat.order;
        }
    }
}