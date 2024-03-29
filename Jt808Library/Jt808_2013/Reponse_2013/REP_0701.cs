﻿
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Reponse_2013
{
    /// <summary>
    /// 消息指令0x0701(电子运单上报)
    /// </summary>
    public class REP_0701_2013
    {
        public REP_0701_2013()
        {
        }
        public PB0701 Decode(byte[] msgBody)
        {
            return new PB0701()
            {
                ElectronicWaybill = msgBody.Copy(4, msgBody.Length - 4)
            };
        }
    }
}
