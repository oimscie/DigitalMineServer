
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
    /// <summary>
    /// 消息指令0x8300(文本下发指令)
    /// </summary>
    public class REQ_8300_2019
    {
        private Encoding encoding = null;

        public REQ_8300_2019()
        {
            encoding = Encoding.GetEncoding("GBK");
        }

        /// <summary>
        /// 文本信息下发消息体封装
        /// </summary>
        /// <param name="EmFlag">（0-1）1:紧急内容</param>
        /// <param name="displayScreen">（0-1）1:终端显示器显示</param>
        /// <param name="tts">(0-1)1:文本到语音报读</param>
        /// <param name="null">保留</param>
        /// <param name="msgType">(0-1)0:中心导航信息，1:CAN 故障码信息</param>
        /// <param name="msgContent">短信内容,最大值1024字节</param>
        /// <returns></returns>
        public byte[] Encode(PB8300 info)
        {
            byte[] msgc =encoding.GetBytes(info.msgContent);
            byte[] data = new byte[msgc.Length + 2];
            data[0] = (byte)((info.EmFlag & 0x3)
                | ((info.displayScreen & 0x01) << 2)
                | ((info.tts & 0x01) << 3)
                | ((info.adScreen & 0x00) << 4)
                | ((info.msgType & 0x01) << 5));
            data[1] = 1;
            msgc.CopyTo(data, 2);
            return data;
        }
    }
}
