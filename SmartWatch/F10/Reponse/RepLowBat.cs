using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepLowBat
    {
        public RepLowBat()
        { }

        /// <summary>
        ///低电短信报警开关[3G*XXXXXXXXXX*LEN*LOWBAT]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepLowBat Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepLowBat
            {
                messageId = item[0],
            };
        }
    }
}