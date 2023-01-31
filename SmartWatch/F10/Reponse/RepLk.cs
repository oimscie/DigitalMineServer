using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepLk
    {
        public RepLk()
        { }

        /// <summary>
        ///  心跳[3G*XXXXXXXXXX*LEN*LK,步数,翻滚次数,电量百分数]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepLk Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepLk
            {
                messageId = item[0],
                step = item[1],
                roll = item[2],
                battery = item[3]
            };
        }
    }
}