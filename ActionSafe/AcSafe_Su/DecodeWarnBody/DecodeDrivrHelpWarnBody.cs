using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.DecodeWarnBody
{
    /// <summary>
    /// 解码高级驾驶服务报警信息
    /// </summary>
    public class DecodeDrivrHelpWarnBody
    {
        /// <summary>
        /// 解码高级驾驶服务报警信息
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public DriveHelpWarnBody DecodeDrivrHelpWarn(byte[] buffer)
        {
            int index = 0;
            DriveHelpWarnBody item = new DriveHelpWarnBody
            {
                ID = buffer.ToUInt32(index),
                WarnState = buffer[index += 4],
                WarnType = buffer[index += 1],
                WarnLevel = buffer[index += 1],
                FrontVehicleSpeed = buffer[index += 1],
                FrontDistance = buffer[index += 1],
                DeviateType = buffer[index += 1],
                RoadSignType = buffer[index += 1],
                RoadSignData = buffer[index += 1],
                VehicleSpeed = buffer[index += 1],
                High = buffer.ToUInt16(index+=1),
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
