using SmartWatch.F10.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWatch.F10.PacketBody
{
    /// <summary>
    /// 封包解包
    /// </summary>
    public class PacketFrom
    {
        private readonly char StartChar = '[';

        private readonly char EndChar = ']';

        private readonly char BodySplitChar = '*';

        private readonly char ContentSplitChar = ',';

        /// <summary>
        /// F10原始数据解包,[包头*设备ID*内容长度*内容]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public F10Packet F10UnPack(byte[] buffer)
        {
            try
            {
                string content = Encoding.ASCII.GetString(buffer);
                content = Util.TrimString(content, StartChar, EndChar);
                string[] PacketItem = content.Split(BodySplitChar);
                ///检查内容长度项是否与内容长度相同
                if (Convert.ToInt32(PacketItem[2], 16) != PacketItem[3].Length)
                {
                    return new F10Packet
                    {
                        content = null
                    };
                }
                return new F10Packet
                {
                    FixBody = new FixBody
                    {
                        head = PacketItem[0],
                        id = PacketItem[1].PadLeft(20, '0'),//补足20位，不足补0
                        length = PacketItem[2]
                    },
                    content = PacketItem[3]
                };
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        ///  F10原始数据封包,[包头*设备ID*内容长度*内容]
        /// </summary>
        /// <param name="Packet"></param>
        /// <returns></returns>
        public byte[] F10Pack(F10Packet Packet)
        {
            string content = StartChar + Packet.FixBody.head + BodySplitChar + Packet.FixBody.id + BodySplitChar + string.Format("{0:x4}", Packet.content.Length) + BodySplitChar + Packet.content + EndChar;
            return Encoding.ASCII.GetBytes(content);
        }

        /// <summary>
        /// 获取F10内容中的消息类型
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string F10GetHeadName(string content)
        {
            return content.Split(ContentSplitChar)[0];
        }
    }
}