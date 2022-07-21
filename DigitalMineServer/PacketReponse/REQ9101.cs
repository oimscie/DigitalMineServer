using DigitalMineServer.OrderMessage;
using JtLibrary;
using JtLibrary.Jt1078_2016.Request_2016;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using JtLibrary.Structures;

namespace DigitalMineServer.PacketReponse
{
    class REQ9101
    {
        public byte[] R9101(AudioAndVideo AudioAndVideo)
        {
            int ports = AudioAndVideo.id == "2" ? 8086 : 8087;
            byte[] body_9101 = new REQ_9101_2016().Encode(new PB9101()
            {
                length = 12,
                ip = "120.27.8.104",
                port = (ushort)ports,
                ports = 0000,
                id = byte.Parse(AudioAndVideo.id),
                datatype = byte.Parse(AudioAndVideo.datatype),
                datatypes = byte.Parse(AudioAndVideo.datatypes)
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode_2013(new PacketFrom()
            {
                msgBody = body_9101,
                msgId = JT1078Cmd.REQ_9101,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(AudioAndVideo.sim),
            });
            return buffer;
        }
    }
}
