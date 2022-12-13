using ActionSafe.AcSafe_Su.Reauest_Su_2013;
using DigitalMineServer.Static;
using DigitalMineServer.Util;
using JtLibrary;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace DigitalMineServer.PacketReponse
{
    /// <summary>
    /// 报警附件上传指令
    /// </summary>
    public class REQ_9208
    {
        /// <summary>
        /// 报警附件上传指令
        /// </summary>
        /// <param name="sim">sim号</param>
        /// <param name="WarnNumber">报警标识号</param>
        /// <param name="warnId">平台分配的唯一32位ID</param>
        /// <returns></returns>
        public byte[] Packet_9208_Su_2013(string sim, byte[] WarnNumber, byte[] warnId)
        {
            byte[] body_9208 = new ActionSafe.AcSafe_Su.Reauest_Su_2013.REQ_9208().Encoder(new PB9208
            {
                ipLength = (byte)Resource.ServerIp.Length,
                ip = Resource.ServerIp,
                TPort = 8093,
                UPort = 0,
                warnNumber = WarnNumber,
                warnId = warnId,
                reserved = WarnNumber
            });

            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
            {
                msgBody = body_9208,
                msgId = AcSafeCmd.REQ_9208,
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