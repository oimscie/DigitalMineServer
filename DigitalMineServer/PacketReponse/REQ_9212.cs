using ActionSafe.AcSafe_Su.PacketBody;
using ActionSafe.AcSafe_Su.Reauest_Su_2013;
using DigitalMineServer.Redis;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using JtLibrary;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    /// <summary>
    /// 文件上传完成消息应答
    /// </summary>
    public class REQ_9212
    {
        private readonly RedisHelper Redis = new RedisHelper();

        /// <summary>
        /// 文件上传完成消息应答
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pConvert"></param>
        /// <param name="PB9212"></param>
        /// <param name="Session"></param>
        public void Default(PacketMessage msg, IPacketProvider pConvert, PB9212 PB9212, AcSafeFileSession Session)
        {
            ValueTuple<string, string, string, int> equipVersion = Redis.GetEquipVersion(Extension.BCDToString(msg.pmPacketHead.hSimNumber));
            switch (equipVersion.Item3)
            {
                case Version_AcSafe.Ver_AcSafe_su_2013:
                    byte[] buffer_9212 = Packet_9212_Su_2013(msg, PB9212, pConvert);
                    Session.Send(buffer_9212, 0, buffer_9212.Length);
                    break;

                case Version_AcSafe.Ver_AcSafe_yue_2019:

                    break;
            }
        }

        /// <summary>
        /// 文件上传完成消息应封装
        /// </summary>
        /// <param name="sim">sim号</param>
        /// <param name="WarnNumber">报警标识号</param>
        /// <param name="warnId">平台分配的唯一32位ID</param>
        /// <returns></returns>
        public byte[] Packet_9212_Su_2013(PacketMessage msg, PB9212 PB9212, IPacketProvider pConvert)
        {
            byte[] body_9212 = new ActionSafe.AcSafe_Su.Reauest_Su_2013.REQ_9212().Encoder(PB9212);
            byte[] buffer = pConvert.Encode_2013(new PacketFrom()
            {
                msgBody = body_9212,
                msgId = AcSafeCmd.REQ_9212,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = msg.pmPacketHead.hSimNumber
            });
            return buffer;
        }
    }
}