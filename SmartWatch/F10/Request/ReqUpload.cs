using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqUpload
    {
        private readonly char splitChar = ',';

        public ReqUpload()
        { }

        /// <summary>
        /// 上传间隔设置[3G*9617624925*000A*UPLOAD,600]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqUpload_St ReqUpload)
        {
            return ReqUpload.messageId + splitChar + ReqUpload.time;
        }
    }
}