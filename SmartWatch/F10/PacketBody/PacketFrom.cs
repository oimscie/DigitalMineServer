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

        private readonly char splitChar = '*';

        /// <summary>
        /// F10原始数据解包,[包头*设备ID*内容长度*内容]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public F10Packet F10UnPack(byte[] buffer)
        {
            string content = Encoding.ASCII.GetString(buffer);
            content = Util.TrimString(content, StartChar, EndChar);
            string[] PacketItem = content.Split(splitChar);
            return new F10Packet
            {
                FixBody = new FixBody
                {
                    head = PacketItem[0],
                    id = PacketItem[1],
                    length = PacketItem[2]
                },
                content = PacketItem[3]
            };
        }

        /// <summary>
        ///  F10原始数据封包,[包头*设备ID*内容长度*内容]
        /// </summary>
        /// <param name="Packet"></param>
        /// <returns></returns>
        public byte[] F10Pack(F10Packet Packet)
        {
            string length = Packet.content.Length.ToString();
            string content = StartChar + Packet.FixBody.head + splitChar + Packet.FixBody.id + splitChar + length + splitChar + Packet.content + EndChar;
            return Encoding.ASCII.GetBytes(content);
        }
    }
}