﻿
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
   public class REQ_8805_2013
    {
        /// <summary>
        /// 单条存储多媒体数据检索上传命令数据打包
        /// </summary>
        /// <param name="mediaId">多媒体ID,必需大于0</param>
        /// <param name="deleteFlag">0：保留；1：删除</param>
        /// <returns></returns>
        public byte[] Encode(PB8805 info)
        {
            byte[] buffer = new byte[5];
            info.mediaId.ToBytes().CopyTo(buffer, 0);
            buffer[4] = info.deleteFlag;
            return buffer;
        }
    }
}
