using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepOxygen
    {
        public RepOxygen()
        { }

        /// <summary>
        /// 心率血压数据同时上传（同时测量心率、血压、血氧）[3G*XXXXXXXXXX*len*bphrt,参数 1：高压,参数 2：低压,参数 3：心率,参数 4,参数 5,参数 6,参数
        /// 7]，数值为 0 代表无效
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepOxygen_St Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepOxygen_St
            {
                messageId = item[0],
                type = item[1],
                oxy = item[2],
            };
        }
    }
}