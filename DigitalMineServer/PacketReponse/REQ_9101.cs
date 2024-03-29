﻿using DigitalMineServer.OrderMessage;
using DigitalMineServer.Redis;
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
    internal class REQ_9101
    {
        private readonly RedisHelper Redis = new RedisHelper();

        public byte[] R9101(AudioAndVideo AudioAndVideo)
        {
            int port = AudioAndVideo.datatype == "2" ? 8086 : 8087;
            ValueTuple<string, string, string, int> equipVersion = Redis.GetEquipVersion(AudioAndVideo.sim);
            switch (equipVersion.Item1)
            {
                case Version_808.Ver_808_2019:
                    return decode_9101_2019(AudioAndVideo, port);

                default:
                    return decode_9101_2013(AudioAndVideo, port);
            }
        }

        /// <summary>
        /// 2013版9101编码
        /// </summary>
        /// <param name="AudioAndVideo"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private byte[] decode_9101_2013(AudioAndVideo AudioAndVideo, int port)
        {
            byte[] body_9101 = new REQ_9101_2016().Encode(new PB9101()
            {
                length = (byte)Resource.ServerIp.Length,
                ip = Resource.ServerIp,
                port = (ushort)port,
                ports = 0000,
                id = byte.Parse(AudioAndVideo.id),
                datatype = byte.Parse(AudioAndVideo.datatype),
                datatypes = byte.Parse(AudioAndVideo.datatypes)
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
            {
                msgBody = body_9101,
                msgId = JT1078Cmd.REQ_9101,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(AudioAndVideo.sim),
            });
            return buffer;
        }

        /// <summary>
        ///  2019版9101编码
        /// </summary>
        /// <param name="AudioAndVideo"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private byte[] decode_9101_2019(AudioAndVideo AudioAndVideo, int port)
        {
            byte[] body_9101 = new REQ_9101_2016().Encode(new PB9101()
            {
                length = (byte)Resource.ServerIp.Length,
                ip = Resource.ServerIp,
                port = (ushort)port,
                ports = 0000,
                id = byte.Parse(AudioAndVideo.id),
                datatype = byte.Parse(AudioAndVideo.datatype),
                datatypes = byte.Parse(AudioAndVideo.datatypes)
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2019(new PacketFrom()
            {
                msgBody = body_9101,
                msgId = JT1078Cmd.REQ_9101,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(AudioAndVideo.sim),
            });
            return buffer;
        }
    }
}