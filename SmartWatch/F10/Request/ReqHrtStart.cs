using SmartWatch.F10.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.Reponse
{
    public class ReqHrtStart
    {
        private readonly char splitChar = ',';

        public ReqHrtStart()
        { }

        /// <summary>
        /// 心率协议（同时测量心率、血压、血氧）[3G*XXXXXXXXXX*len*hrtstart,X]
        /// 说明： x 为上传间隔时间，单位秒,连续上传时最小时间不小于 300秒，最大不超过 65535。
        /// x 为 1 则代表终端心率单次上传，上传完后自动关闭。 x 为 0 则代表终端心率上传关闭。
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encode(PacketBody.ReqHrtStart ReqHrtStart)
        {
            return ReqHrtStart.messageId + splitChar + ReqHrtStart.order;
        }
    }
}