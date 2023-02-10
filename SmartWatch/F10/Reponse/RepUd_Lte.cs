using SmartWatch.F10.PacketBody;
using System;

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
        public PacketBody.RepUd_Lte_St Decode(string content)
        {
            string[] item = content.Split(',');
            foreach (var i in item)
            {
                System.Diagnostics.Debug.WriteLine(i);
            }
            return new PacketBody.RepUd_Lte_St
            {
                messageId = item[0],
                position = new Position_St
                {
                    time = DateTime.ParseExact(item[1].Substring(4, 2) + "-" + item[1].Substring(2, 2) + "-" + item[1].Substring(0, 2) + " " + item[2].Substring(0, 2) + ":" + item[2].Substring(2, 2) + ":" + item[2].Substring(4, 2), "yy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture).AddHours(8),
                    active = item[3] == "A" ? "定位" : "未定位",
                    lat = item[4],
                    latType = item[5],
                    lon = item[6],
                    lonType = item[7],
                    speed = item[8],
                    battery = item[13],
                    step = item[14],
                    state = item[16],
                    accuracy = item[item.Length - 1],
                }
            };
        }
    }
}