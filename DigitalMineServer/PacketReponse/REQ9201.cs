
using DigitalMineServer.OrderMessage;
using DigitalMineServer.Static;
using JtLibrary;
using JtLibrary.Jt1078_2016.Request_2016;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.PacketReponse
{
    class REQ9201
    {
        public byte[] R9201(HisVideoAndAudio HisVideoAndAudio)
        {
            int port = HisVideoAndAudio.datatype == "1" ? 8088 : 8089;
            switch (Resource.equipVersion[HisVideoAndAudio.sim].Item1)
            {
                case Version_808.Ver_808_2019:
                    return decode_9201_2019(HisVideoAndAudio, port);
                default:
                    return decode_9201_2013(HisVideoAndAudio, port);
            }
        }

        private byte[] decode_9201_2013(HisVideoAndAudio HisVideoAndAudio, int port) {
            byte[] body_9201 = new REQ_9201_2016().Encode(new PB9201()
            {
                length = 12,
                ip = "120.27.8.104",
                port = (ushort)port,
                ports = 0,
                id = byte.Parse(HisVideoAndAudio.id),
                datatype = byte.Parse(HisVideoAndAudio.datatype),
                datatypes = byte.Parse(HisVideoAndAudio.datatypes),
                memoryType = 0,
                ReviewType = byte.Parse(HisVideoAndAudio.ReviewType),
                FastOrSlow = byte.Parse(HisVideoAndAudio.FastOrSlow),
                StartTime = Extension.TimeFormatToBCD(Convert.ToDateTime(HisVideoAndAudio.StartTime)),
                OverTime = Extension.TimeFormatToBCD(Convert.ToDateTime(HisVideoAndAudio.OverTime)),
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
            {
                msgBody = body_9201,
                msgId = JT1078Cmd.REQ_9201,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(HisVideoAndAudio.sim),
            });
            return buffer;
        }
        private byte[] decode_9201_2019(HisVideoAndAudio HisVideoAndAudio, int port)
        {
            byte[] body_9201 = new REQ_9201_2016().Encode(new PB9201()
            {
                length = 12,
                ip = "120.27.8.104",
                port = (ushort)port,
                ports = 0,
                id = byte.Parse(HisVideoAndAudio.id),
                datatype = byte.Parse(HisVideoAndAudio.datatype),
                datatypes = byte.Parse(HisVideoAndAudio.datatypes),
                memoryType = 0,
                ReviewType = byte.Parse(HisVideoAndAudio.ReviewType),
                FastOrSlow = byte.Parse(HisVideoAndAudio.FastOrSlow),
                StartTime = Extension.TimeFormatToBCD(Convert.ToDateTime(HisVideoAndAudio.StartTime)),
                OverTime = Extension.TimeFormatToBCD(Convert.ToDateTime(HisVideoAndAudio.OverTime)),
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2019(new PacketFrom()
            {
                msgBody = body_9201,
                msgId = JT1078Cmd.REQ_9201,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(HisVideoAndAudio.sim),
            });
            return buffer;
        }
    }
}
