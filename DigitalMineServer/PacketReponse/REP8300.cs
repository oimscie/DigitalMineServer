using JtLibrary;
using JtLibrary.Jt808_2013.Request_2013;
using JtLibrary.PacketBody;
using JtLibrary.Structures;

namespace DigitalMineServer.PacketReponse
{
    class REP8300
    {
        public byte[] R8300(string sim, string info)
        {
            byte[] body_8300 = new REQ_8300_2013().Encode(new PB8300()
            {
                EmFlag = 1,
                displayScreen = 1,
                tts = 1,
                adScreen = 1,
                msgType = 0,
                msgContent = info,
            });
            byte[] buffer = PacketProvider.CreateProvider().Encode(new PacketFrom()
            {
                msgBody = body_8300,
                msgId = JT808Cmd.REQ_8300,
                msgSerialnumber = 0,
                pEncryptFlag = 0,
                pSerialnumber = 1,
                pSubFlag = 0,
                pTotal = 1,
                simNumber = Extension.ToBCD(sim),
            });
            return buffer;
        }
    }
}
