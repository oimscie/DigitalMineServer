﻿
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
    /// <summary>
    /// 消息指令0x8001(平台通用应答)
    /// </summary>
    public class REQ_8001_2013
    {
        public REQ_8001_2013()
        { }

        /// <summary>
        /// 0x8001通用应答数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8001 info)
        {
            return new byte[5]
            {
                (byte)(info.Serialnumber >> 8),
                (byte)info.Serialnumber,
                (byte)(info.MessageId >> 8),
                (byte)info.MessageId,
                info.Result 
            };
        }
    }
}
