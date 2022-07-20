using JtLibrary;
using JtLibrary.Jt1078_2016.Request_2016;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;

namespace DigitalMineServer.PacketReponse
{
    class REQ9202
    {
        public byte[] R9202(string[] data)
        {
            byte[] body_9202 = new REQ_9202_2016().Encode(new PB9202()
            {
                id = byte.Parse(data[2]),
                type = byte.Parse(data[3]),
                order = byte.Parse(data[4]),
                time = Extension.ToBCD(data[5])
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
            {
                msgBody = body_9202,
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
