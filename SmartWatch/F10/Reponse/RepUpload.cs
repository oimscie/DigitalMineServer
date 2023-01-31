using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class RepUpload
    {
        public RepUpload()
        { }

        /// <summary>
        ///  上传间隔设置[3G*XXXXXXXXXX*LEN*UPLOAD]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public PacketBody.RepUpload Decode(string content)
        {
            string[] item = content.Split(',');
            return new PacketBody.RepUpload
            {
                messageId = item[0],
            };
        }
    }
}