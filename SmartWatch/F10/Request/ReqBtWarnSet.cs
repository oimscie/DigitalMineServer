using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqBtWarnSet
    {
        private readonly char splitChar = ',';

        public ReqBtWarnSet()
        { }

        /// <summary>
        /// 体温异常提醒设置下发协议[3G*XXXXXXXXXX*LEN*BTWARNSET,lowbt,highbt,open,type,tel]
        /// arg2 ： 2 :间隔时间，单位小时，取值： 1-12（夜间模式不上报）
        /// arg1 ： 0 :间隔测量关闭 1 :间隔测量开启
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqBtWarnSet ReqBtWarnSet)
        {
            return ReqBtWarnSet.messageId + splitChar + ReqBtWarnSet.lowbt + splitChar + ReqBtWarnSet.highbt + splitChar + ReqBtWarnSet.open + splitChar + ReqBtWarnSet.type + splitChar + ReqBtWarnSet.telephone;
        }
    }
}