using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepSos
    {
        public RepSos()
        { }

        /// <summary>
        ///SOS号码设置[3G*9617624925*0003*SOS]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepSos_St Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepSos_St
            {
                messageId = item[0],
            };
        }
    }
}