using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Jt808_2019.Request_2019;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    //默认回复
    public class REQ_8001_AcSafe
    {
        public void Default(PacketMessage msg, IPacketProvider pConvert, AcSafeFileSession Session)
        {
            switch (Resource.equipVersion[Extension.BCDToString(msg.pmPacketHead.hSimNumber)].Item1)
            {
                case Version_808.Ver_808_2013:
                    byte[] buffer_2013 = Packet_default_2013(msg, pConvert);
                    Session.Send(buffer_2013, 0, buffer_2013.Length);
                    break;

                case Version_808.Ver_808_2019:
                    byte[] buffer_2019 = Packet_default_2019(msg, pConvert);
                    Session.Send(buffer_2019, 0, buffer_2019.Length);
                    break;
            }
        }

        /// <summary>
        /// 2013消息打包
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pConvert"></param>
        /// <returns></returns>
        private byte[] Packet_default_2013(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_default = new REQ_8001_2013().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2013(new PacketFrom()
            {
                msgBody = body_default,
                msgId = JT808Cmd.REQ_8001,
                msgSerialnumber = msg.pmPacketHead.phSerialnumber,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber,
            });
            return buffer;
        }

        /// <summary>
        /// 2019消息打包
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pConvert"></param>
        /// <returns></returns>
        private byte[] Packet_default_2019(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_default = new REQ_8001_2019().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2019(new PacketFrom()
            {
                msgBody = body_default,
                msgId = JT808Cmd.REQ_8001,
                msgSerialnumber = msg.pmPacketHead.phSerialnumber,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber,
            });
            return buffer;
        }
    }
}