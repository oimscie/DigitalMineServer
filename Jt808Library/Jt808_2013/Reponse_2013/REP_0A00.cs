
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 消息指令0x0A00(终端RSA公钥{e,n})
    /// </summary>
    public class REP_0A00_2013
    {
        public REP_0A00_2013()
        {
        }
        /// <summary>
        /// 终端公钥数据体解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0A00 Decode(byte[] msgBody)
        {
            return new PB0A00()
            {
                RSA_e = msgBody.ToUInt32(0),
                RSA_n = msgBody.Copy(4, msgBody.Length - 4)
            };
        }
    }
}
