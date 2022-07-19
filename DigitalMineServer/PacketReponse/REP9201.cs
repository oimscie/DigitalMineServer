
using JtLibrary;
using JtLibrary.Jt1078_2016.Request;
using JtLibrary.PacketBody;
using JtLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.PacketReponse
{
    class REP9201
    {
        public byte[] R9201(string[] data)
        {
            ushort ports = 8089;
            byte id = byte.Parse(data[5]);
            if (data[2]== "1")
            {
                ports = 8088;
            }
            byte[] body_9201 = new REQ_9201_2016().Encode(new PB9201()
            {
                length = 12,
                ip = "120.27.8.104",
                port = ports,
                ports = 0,
                id = id,
                datatype =2,
                datatypes = 1,
                memoryType = 0,
                ReviewType = 0,
                FastOrSlow =0,
                StartTime = Extension.TimeFormatToBCD(Convert.ToDateTime(data[3])),
                OverTime = Extension.TimeFormatToBCD(Convert.ToDateTime(data[4])),
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode(new PacketFrom()
            {
                msgBody = body_9201,
                msgId = JT1078Cmd.REQ_9201,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(data[1]),
            });
            return buffer;
        }
    }
}
