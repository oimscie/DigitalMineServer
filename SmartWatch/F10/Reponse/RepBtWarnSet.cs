using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepBtWarnSet
    {
        public RepBtWarnSet()
        { }

        /// <summary>
        /// 体温异常提醒设置下发协议[3G*XXXXXXXXXX*LEN*BTWARNSET]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepBtWarnSet_St Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepBtWarnSet_St
            {
                messageId = item[0]
            };
        }
    }
}