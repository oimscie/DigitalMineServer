﻿using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.DecodeDriverStateWarnBody
{
    public class DecodeDriverStateWarnBody
    {
        /// <summary>
        /// 解码驾驶员状态监测系统报警
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public DriverStateWarnBody DecodeDriverStateWarn(byte[] buffer)
        {
            int index = 0;
            DriverStateWarnBody item = new DriverStateWarnBody
            {
                ID = buffer.ToUInt32(index),
                WarnState = buffer[index += 4],
                WarnType = buffer[index += 1],
                WarnLevel = buffer[index += 1],
                FatigueLevel = buffer[index += 1],
                Reserved = buffer.Copy(index += 1, 4),
                VehicleSpeed = buffer[index += 4],
                High = buffer.ToUInt16(index += 1),
                latitude = buffer.ToUInt32(index += 2),
                longitude = buffer.ToUInt32(index += 4),
                Time = buffer.Copy(index += 4, 6),
                VehicleState = buffer.ToUInt16(index += 6),
                WarnNumber = buffer.Copy(index += 2, 16)
            };
            return item;
        }
    }
}
