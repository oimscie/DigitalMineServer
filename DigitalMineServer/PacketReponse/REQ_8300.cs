using DigitalMineServer.Static;
using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Jt808_2019.Request_2019;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    class REQ_8300
    {
        public byte[] R8300(string sim, string info)
        {
            switch (Resource.equipVersion[sim].Item1)
            {
                case Version_808.Ver_808_2013:
                    return Packet_8300_2013(sim, info);
                case Version_808.Ver_808_2019:
                    return Packet_8300_2019(sim, info);
                default:
                    return null;
            }
        }
        /// <summary>
        /// 2013消息包装
        /// </summary>
        /// <param name="sim">终端SIM号</param>
        /// <param name="info">下发文本</param>
        /// <returns></returns>
        public byte[] Packet_8300_2013(string sim, string info) {
            byte[] body_8300 = new REQ_8300_2013().Encode(new PB8300()
            {
                EmFlag = 1,
                displayScreen = 1,
                tts = 1,
                adScreen = 1,
                msgType = 0,
                msgContent = info,
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
            {
                msgBody = body_8300,
                msgId = JT808Cmd.REQ_8300,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(sim),
            });
            return buffer;
        }
        /// <summary>
        /// 2019消息包装
        /// </summary>
        /// <param name="sim">终端SIM号</param>
        /// <param name="info">下发文本</param>
        /// <returns></returns>
        public byte[] Packet_8300_2019(string sim, string info)
        {
            byte[] body_8300 = new REQ_8300_2019().Encode(new PB8300()
            {
                EmFlag = 11,
                displayScreen = 1,
                tts = 1,
                adScreen = 0,
                msgType = 0,
                msgContent = info,
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2019(new PacketFrom()
            {
                msgBody = body_8300,
                msgId = JT808Cmd.REQ_8300,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(sim),
            });
            return buffer;
        }
    }
}
