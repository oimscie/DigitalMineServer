/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 15:17:43
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 2bf89b23-d150-450c-b9bb-cb5ef17bed5c
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.PacketBody.Request
{
    /// <summary>
    /// 终端注册返回应答
    /// </summary>
    public class REQ_8100
    {
        private readonly Encoding encoding = Encoding.GetEncoding("GBK");
        public REQ_8100()
        {
        }

        /// <summary>
        /// 数据打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8100 info)
        {
            byte[] codes = null;
            int len = 3;
            if (info.Result == 0)
            {
                codes = encoding.GetBytes(info.AuthenticationCode);
                len = 3 + codes.Length;
            }
            byte[] data = new byte[len];

            data[0] = (byte)(info.Serialnumber >> 8);
            data[1] = (byte)info.Serialnumber;
            data[2] = info.Result;
            if (info.Result == 0)
            {
                codes.CopyTo(data, 3);
            }
            return data;
        }
    }
}
