using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.Jt808_2013.Reponse_2013;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Jt808_2019.Reponse_2019;
using JtLibrary.Jt808_2019.Request_2019;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using System.Drawing;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    internal class REP_0200
    {
        public void R0200(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            string sim = Extension.BCDToString(msg.pmPacketHead.hSimNumber);
            switch (Resource.equipVersion[sim].Item1)
            {
                case Version_808.Ver_808_2013:
                    byte[] buffer_2013 = Packet_0200_2013(msg, pConvert);
                    Session.Send(buffer_2013, 0, buffer_2013.Length);
                    if (Resource.VehicleList.ContainsKey(sim))
                    {
                        Resource.Vehicle0200DataQueues.Enqueue(new ValueTuple<string, PB0200>
                        {
                            Item1 = sim,
                            Item2 = new REP_0200_2013().Decode(msg.pmMessageBody)
                        });
                    }
                    else if (Resource.PersonList.ContainsKey(sim))
                    {
                        Resource.Person0200DataQueues.Enqueue(new ValueTuple<string, PB0200>
                        {
                            Item1 = sim,
                            Item2 = new REP_0200_2013().Decode(msg.pmMessageBody)
                        });
                    }
                    break;

                case Version_808.Ver_808_2019:
                    byte[] buffer_2019 = Packet_0200_2019(msg, pConvert);
                    Session.Send(buffer_2019, 0, buffer_2019.Length);
                    if (Resource.VehicleList.ContainsKey(sim))
                    {
                        Resource.Vehicle0200DataQueues.Enqueue(new ValueTuple<string, PB0200>
                        {
                            Item1 = Extension.BCDToString(msg.pmPacketHead.hSimNumber),
                            Item2 = new REP_0200_2019().Decode(msg.pmMessageBody)
                        });
                    }
                    else if (Resource.PersonList.ContainsKey(sim))
                    {
                        Resource.Person0200DataQueues.Enqueue(new ValueTuple<string, PB0200>
                        {
                            Item1 = Extension.BCDToString(msg.pmPacketHead.hSimNumber),
                            Item2 = new REP_0200_2019().Decode(msg.pmMessageBody)
                        });
                    }
                    break;
            }
        }

        /// <summary>
        /// 2013消息打包
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pConvert"></param>
        /// <returns></returns>
        private byte[] Packet_0200_2013(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0200 = new REQ_8001_2013().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2013(new PacketFrom()
            {
                msgBody = body_0200,
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
        private byte[] Packet_0200_2019(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0200 = new REQ_8001_2019().Encode(new PB8001()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                MessageId = msg.pmPacketHead.phMessageId,
                Result = 0,
            });
            byte[] buffer = pConvert.Encode_2019(new PacketFrom()
            {
                msgBody = body_0200,
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