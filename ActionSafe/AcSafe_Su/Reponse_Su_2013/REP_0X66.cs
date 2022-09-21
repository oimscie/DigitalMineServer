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
    /// 胎压监测系统报警
    /// </summary>
    public class REP_0X66
    {
        /// <summary>
        /// 胎压监测系统报警
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PB0X66 DecodeTirePressureWarn(byte[] buffer)
        {
            int index = 0;
            PB0X66 item = new PB0X66
            {
                ID = buffer.ToUInt32(index),
                WarnState = buffer[index += 4],
                VehicleSpeed = buffer[index += 1],
                High = buffer.ToUInt16(index += 1),
                latitude = buffer.ToUInt32(index += 1),
                longitude = buffer.ToUInt32(index += 4),
                Time = buffer.Copy(index += 4, 6),
                VehicleState = buffer.ToUInt16(index += 6),
                WarnNumber = buffer.Copy(index += 2, 16),
                EventCount = buffer[index += 16]
            };
            item.TirePressure_Event_list = DecodeTirePressureEventlist(buffer.Copy(index += 1, buffer.Length - index), item.EventCount);
            return item;
        }

        /// <summary>
        /// 解码胎压监测系统报警/事件信息列表
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private List<PB0X66Eventlist> DecodeTirePressureEventlist(byte[] buffer, int count)
        {
            int index = 0;
            int temp = 0;
            List<PB0X66Eventlist> list = new List<PB0X66Eventlist>();
            while (temp < count)
            {
                PB0X66Eventlist item = new PB0X66Eventlist
                {
                    Number = buffer[index],
                    EventType = buffer.ToUInt16(index += 2),
                    TirePressure = buffer.ToUInt16(index += 2),
                    TireTemperature = buffer.ToUInt16(index += 2),
                    battery = buffer.ToUInt16(index += 2)
                };
                index += 2;
                temp++;
                list.Add(item);
            }
            return list;
        }
    }
}