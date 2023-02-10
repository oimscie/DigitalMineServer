using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepBodyTemp
    {
        public RepBodyTemp()
        { }

        /// <summary>
        /// 体温间隔测量下发协议[3G*XXXXXXXXXX*LEN*bodytemp]
        /// arg2 ： 2 :间隔时间，单位小时，取值： 1-12（夜间模式不上报）
        /// arg1 ： 0 :间隔测量关闭 1 :间隔测量开启
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepBodyTemp_St Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepBodyTemp_St
            {
                messageId = item[0],
            };
        }
    }
}