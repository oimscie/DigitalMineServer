
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Reponse_2019
{
    /// <summary>
    /// 消息指令0x0901(数据压缩上报)
    /// </summary>
    public class REP_0901_2019
    {
        public REP_0901_2019()
        {
        }
        /// <summary>
        /// (GZip压缩)压缩的数据
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0901 Decode(byte[] msgBody)
        {
            return new PB0901()
            {
                CompressData = msgBody.Copy(4, msgBody.Length - 4)
            };
        }
    }
}
