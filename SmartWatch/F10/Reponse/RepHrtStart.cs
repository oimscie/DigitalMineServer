using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepHrtStart
    {
        public RepHrtStart()
        { }

        /// <summary>
        /// 心率协议（同时测量心率、血压、血氧）[3G*9617624925*0008*hrtstart]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepHrtStart Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepHrtStart
            {
                messageId = item[0],
            };
        }
    }
}