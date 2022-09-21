using JtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ActionSafe.AcSafe_Su.PacketBody.PacketBody;

namespace ActionSafe.AcSafe_Su.Reponse_Su_2013
{
    /// <summary>
    /// 盲区监测报警
    /// </summary>
    public class REP_0X67
    {
        /// <summary>
        /// 盲区监测报警
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PB0X67 DecodeBlindAreaWarn(byte[] buffer)
        {
            int index = 0;
            PB0X67 item = new PB0X67
            {
                ID = buffer.ToUInt32(index),
                WarnState = buffer[index += 1],
                EventType = buffer[index += 1],
                VehicleSpeed = buffer[index += 1],
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