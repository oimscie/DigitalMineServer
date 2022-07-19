using JtLibrary;
using JtLibrary.Jt1078_2016.Request;
using JtLibrary.PacketBody;
using JtLibrary.Structures;

namespace DigitalMineServer.PacketReponse
{
    class REP9102
    {
        public byte[] R9102(string[] data)
        {
            byte[] body_9102 = new REQ_9102_2016().Encode(new PB9102()
            {
                id = byte.Parse(data[2]),
                order = byte.Parse(data[3]),
                type = byte.Parse(data[4]),
                datatypes = 1
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode(new PacketFrom()
            {
                msgBody = body_9102,
                msgId = JT1078Cmd.REQ_9102,
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
