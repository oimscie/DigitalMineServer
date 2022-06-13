/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 15:31:00
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 12e8b5fa-0ab6-4025-b31d-9a9c7988cb90
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Request
{
    /// <summary>
    /// 消息指令0x8300(文本下发指令)
    /// </summary>
    public class REQ_8300
    {
        private Encoding encoding = null;

        public REQ_8300()
        {
            encoding = Encoding.GetEncoding("GBK");
        }

        /// <summary>
        /// 文本信息下发消息体封装
        /// </summary>
        /// <param name="EmFlag">（0-1）1:紧急内容</param>
        /// <param name="displayScreen">（0-1）1:终端显示器显示</param>
        /// <param name="tts">(0-1)1:文本到语音报读</param>
        /// <param name="adScreen">(0-1)1:广告屏显示</param>
        /// <param name="msgType">(0-1)0:中心导航信息，1:CAN 故障码信息</param>
        /// <param name="msgContent">短信内容,最大值1024字节</param>
        /// <returns></returns>
        public byte[] Encode(PB8300 info)
        {
            byte[] msgc =encoding.GetBytes(info.msgContent);
            byte[] data = new byte[msgc.Length + 1];

            data[0] = (byte)((info.EmFlag & 0x01)
                | ((info.displayScreen & 0x01) << 2)
                | ((info.tts & 0x01) << 3)
                | ((info.adScreen & 0x01) << 4)
                | ((info.msgType & 0x01) << 5));

            msgc.CopyTo(data, 1);
            return data;
        }
    }
}
