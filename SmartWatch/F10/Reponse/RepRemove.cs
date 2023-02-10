using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepRemove
    {
        public RepRemove()
        { }

        /// <summary>
        ///取下手环报警开关[3G*XXXXXXXXXX*LEN*REMOVE]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepRemove_St Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepRemove_St
            {
                messageId = item[0],
            };
        }
    }
}