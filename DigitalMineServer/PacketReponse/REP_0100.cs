using DigitalMineServer.Redis;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.Jt808_2019.Request_2019;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using JtLibrary.Utils;
using System;
using static DigitalMineServer.Structures.Comprehensive;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    internal class REP_0100
    {
        private readonly RedisHelper Redis = new RedisHelper();

        public void R0100(PacketMessage msg, IPacketProvider pConvert, Jt808Session Session)
        {
            string sim = Extension.BCDToString(msg.pmPacketHead.hSimNumber);
            //判断808版本
            string Version808 = VersionCheck.Get808Version(msg.pmPacketHead.phPacketHeadAttribute.IdentifiersVersion);
            if (Version808 == Version_808.Ver_808_null)
            {
                return;
            }
            ValueTuple<string, string, string, int> val = new ValueTuple<string, string, string, int>
            {
                Item1 = Version808,
                Item2 = Version_1078.Ver_1078_null,
                Item3 = Version_AcSafe.Ver_AcSafe_null,
                Item4 = msg.pmPacketHead.protocolVersion
            };
            //存入字典
            Redis.Set(sim + Redis_key_ext.equipVersion, Utils.Util.ObjectSerializ(val), -1);
            switch (Version808)
            {
                case Version_808.Ver_808_2013:
                    byte[] buffer_2013 = Packet_0100_2013(msg, pConvert);
                    Session.Send(buffer_2013, 0, buffer_2013.Length);
                    break;

                case Version_808.Ver_808_2019:
                    byte[] buffer_2019 = Packet_0100_2019(msg, pConvert);
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
        private byte[] Packet_0100_2013(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0100 = new REQ_8100_2013().Encode(new PB8100()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                Result = 0,
                AuthenticationCode = "111111"
            });
            byte[] buffer = pConvert.Encode_2013(new PacketFrom()
            {
                msgBody = body_0100,
                msgId = JT808Cmd.REQ_8100,
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
        private byte[] Packet_0100_2019(PacketMessage msg, IPacketProvider pConvert)
        {
            byte[] body_0100 = new REQ_8100_2019().Encode(new PB8100()
            {
                Serialnumber = msg.pmPacketHead.phSerialnumber,
                Result = 0,
                AuthenticationCode = "111111"
            });
            byte[] buffer = pConvert.Encode_2019(new PacketFrom()
            {
                msgBody = body_0100,
                msgId = JT808Cmd.REQ_8100,
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