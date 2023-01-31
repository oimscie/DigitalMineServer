using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepUd_Lte
    {
        public RepUd_Lte()
        { }

        /// <summary>
        ///位置数据解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepUd_Lte Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepUd_Lte
            {
                messageId = item[0],
                position = new Position
                {
                    messageNumber = item[1],
                    time = item[2].Substring(4, 2) + "-" + item[2].Substring(2, 2) + "-" + item[2].Substring(0, 2) + " " + item[3].Substring(0, 2) + ":" + item[3].Substring(2, 2) + ":" + item[3].Substring(4, 2),
                    active = item[4] == "A" ? "定位" : "未定位",
                    lat = item[5],
                    latType = item[6],
                    lon = item[7],
                    lonType = item[8],
                    speed = item[9],
                    battery = item[14],
                    step = item[15],
                    state = item[17],
                    accuracy = item[item.Length],
                }
            };
        }
    }
}