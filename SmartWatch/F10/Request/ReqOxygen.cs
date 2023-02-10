using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqOxygen
    {
        private readonly char splitChar = ',';

        public ReqOxygen()
        { }

        /// <summary>
        ///血氧上报协议[3G*XXXXXXXXXX*LEN*oxygen,status]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqOxygen_St ReqOxygen)
        {
            return ReqOxygen.messageId + splitChar + ReqOxygen.state;
        }
    }
}