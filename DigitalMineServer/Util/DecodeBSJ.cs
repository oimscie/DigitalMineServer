using JtLibrary;
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.Util
{
    /// <summary>
    /// bsj附加信息扩展指令解析
    /// </summary>
    public class DecodeBSJ
    {
        readonly Dictionary<int, byte[]> dic;
        public DecodeBSJ()
        {
            dic = new Dictionary<int, byte[]>();
        }
        /// <summary>
        /// 解码bsj附加信息扩展指令信息
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Dictionary<int, byte[]> decode(byte[] buffer)
        {
            int index = 0;
            while (index < buffer.Length)
            {
                int length = buffer.ToUInt16(index);
                dic.Add(buffer.ToUInt16(index += 2), buffer.Copy(index += 2, length - 2));
                index += length - 2;
            }
            return dic;
        }
    }
}
