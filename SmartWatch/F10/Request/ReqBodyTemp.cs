using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqBodyTemp
    {
        private readonly char splitChar = ',';

        public ReqBodyTemp()
        { }

        /// <summary>
        /// 体温间隔测量下发协议[3G*XXXXXXXXXX*LEN*bodytemp,arg1， arg2]
        /// arg2 ： 2 :间隔时间，单位小时，取值： 1-12（夜间模式不上报）
        /// arg1 ： 0 :间隔测量关闭 1 :间隔测量开启
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqBodyTemp ReqBodyTemp)
        {
            return ReqBodyTemp.messageId + splitChar + ReqBodyTemp.arg1 + splitChar + ReqBodyTemp.arg2;
        }
    }
}