using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepCenter
    {
        public RepCenter()
        { }

        /// <summary>
        /// 短信报警接收号码[3G*9617624925*0006*CENTER]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepCenter Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepCenter
            {
                messageId = item[0],
            };
        }
    }
}