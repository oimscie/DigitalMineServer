using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;

namespace DigitalMineServer.PacketReponse
{
    /// <summary>
    /// 弃用
    /// </summary>
    public class REQ8604
    {
        public byte[] R8604(string sim, List<UInt32UInt32> polygonItemsInfo)
        {
            byte[] body_8604 = new REQ_8604_2013().Encode(new PB8604()
            {
                polygonId = 1,
                polygonProperty = AreaAttribute(new AreaAttribute()
                {
                    accordingTime = 0,
                    limitSpeed = 0,
                    inareaUpDriver = 0,
                    inareaUpPltf = 1,
                    outareaUpDriver = 0,
                    outareaUpPltf = 1,
                    latflag = 0,
                    lngflag = 0,
                    openflag = 0,
                    communicationflag = 0,
                    samplingflag = 1
                }),
                stime = DateTime.Parse("1970-1-1 00:00:00"),
                etime = DateTime.Parse("2170-1-1 00:00:00"),
                maxSpeed = 0,
                overSpeedingTime = 0,
                polygonItemsInfo = polygonItemsInfo
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
            {
                msgBody = body_8604,
                msgId = JT808Cmd.REQ_8604,
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
        /// 区域属性,所有参数均为0或1值
        /// </summary>
        /// <returns></returns>
        public UInt16 AreaAttribute(AreaAttribute info)
        {
            UInt16 attr = 0;
            attr |= info.accordingTime;
            attr |= (UInt16)(info.limitSpeed << 1);
            attr |= (UInt16)(info.inareaUpDriver << 2);
            attr |= (UInt16)(info.inareaUpPltf << 3);
            attr |= (UInt16)(info.outareaUpDriver << 4);
            attr |= (UInt16)(info.outareaUpPltf << 5);
            attr |= (UInt16)(info.latflag << 6);
            attr |= (UInt16)(info.lngflag << 7);
            attr |= (UInt16)(info.openflag << 8);
            attr |= (UInt16)(info.communicationflag << 14);
            attr |= (UInt16)(info.samplingflag << 15);
            return attr;
        }
    }
}
