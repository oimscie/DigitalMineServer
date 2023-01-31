using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepImg
    {
        public RepImg()
        { }

        /// <summary>
        /// 远程拍照[3G*XXXXXXXXXX*len*img,5,y,z]，参数 Y 表示 ：时间（年月日时分秒： 221112144812） 参数 Z 为照片内容。
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepImg Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepImg
            {
                messageId = item[0],
                unknown = item[1],
                time = item[2],
                container = item[3],
            };
        }
    }
}