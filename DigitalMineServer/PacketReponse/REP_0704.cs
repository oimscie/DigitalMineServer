using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.Util;
using JtLibrary;
using JtLibrary.Jt808_2013.Reponse_2013;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Jt808_2019.Reponse_2019;
using JtLibrary.Jt808_2019.Request_2019;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    class REP_0704
    {
        public void R0704(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            switch (Resource.equipVersion[Extension.BCDToString(msg.pmPacketHead.hSimNumber)].Item1)
            {
                case Version_808.Ver_808_2013:
                    byte[] buffer_2013 = Packet_0704_2013(msg, pConvert);
                    Session.Send(buffer_2013, 0, buffer_2013.Length);
                    GetPB0200(new REP_0704_2013().Decode(msg.pmMessageBody), Extension.BCDToString(msg.pmPacketHead.hSimNumber));
                    break;
                case Version_808.Ver_808_2019:
                    byte[] buffer_2019 = Packet_0704_2019(msg, pConvert);
                    Session.Send(buffer_2019, 0, buffer_2019.Length);
                    GetPB0200(new REP_0704_2019().Decode(msg.pmMessageBody), Extension.BCDToString(msg.pmPacketHead.hSimNumber));
                    break;
            }
        }
        /// <summary>
        /// 2013消息打包
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pConvert"></param>
        /// <returns></returns>
        private byte[] Packet_0704_2013(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0704 = new REQ_8001_2013().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2013(new PacketFrom()
            {
                msgBody = body_0704,
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
        private byte[] Packet_0704_2019(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0704 = new REQ_8001_2019().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2019(new PacketFrom()
            {
                msgBody = body_0704,
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
        /// 取出定位消息批量上传数据体里的0200数据
        /// </summary>
        /// <param name="bodyinfo_0704"></param>
        /// <param name="sim"></param>
        private void GetPB0200(PB0704 bodyinfo_0704, string sim)
        {
            for (int i = 0; i < bodyinfo_0704.PositionInformationItems.Count; i++)
            {
                Resource.Vehicle0200DataQueues.Enqueue(new ValueTuple<string, PB0200>
                {
                    Item1 = sim,
                    Item2 = bodyinfo_0704.PositionInformationItems[i]
                });
            }
        }
    }
}
