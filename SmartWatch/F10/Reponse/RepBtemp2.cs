using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepBtemp2
    {
        public RepBtemp2()
        { }

        /// <summary>
        /// 体温上报协议[3G*9617624925*000E*btemp2,1,36.51]
        /// Type ： 1（手腕模式）
        /// Temp ：体温
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepBtemp2_St Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepBtemp2_St
            {
                messageId = item[0],
                type = item[1],
                temp = item[2],
            };
        }
    }
}