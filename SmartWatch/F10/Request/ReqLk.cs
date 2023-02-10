using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqLk
    {
        private readonly char splitChar = ',';

        public ReqLk()
        { }

        /// <summary>
        ///  心跳[3G*XXXXXXXXXX*LEN*LK,步数,翻滚次数,电量百分数]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqLk_St ReqLk)
        {
            return ReqLk.messageId;
        }
    }
}