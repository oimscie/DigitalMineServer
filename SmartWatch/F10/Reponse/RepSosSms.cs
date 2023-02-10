using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepSosSms
    {
        public RepSosSms()
        { }

        /// <summary>
        ///  SOS短信报警开关[3G*9617624925*0006*SOSSMS]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepSosSms_St Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepSosSms_St
            {
                messageId = item[0],
            };
        }
    }
}