using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using JtLibrary;
using JtLibrary.Jt1078_2016.RtpPacketDecode;
using JtLibrary.PacketBody;
using SuperSocket.SocketBase;
using System.Linq;
using static JtLibrary.Structures.EquipVersion;

namespace DigitalMineServer.ParseMessage
{
    //终端音频消息
    class VehicleAudioMessage
    {
        //消息结束符
        private readonly byte[] endMark = new byte[] { 11, 22, 33, 44 };
        public void Parse(VehicleAudioSession session, byte[] buffer)
        {
            //判断session是否是首次连接，如果是则解析消息体获取SIM
            if (session.Sim == null)
            {
                RtpDecoding decode = new RtpDecoding();
                Video bodyinfo = decode.Decode(buffer, decode.Check1078Versioin(buffer));
                session.Sim = Extension.BCDToString(bodyinfo.SIM);
            }
            //获取客户端音频请求头并下发音频流
            ClientAudioServer Server = JtServerForm.bootstrap.GetServerByName("ClientAudioServer") as ClientAudioServer;
            var sessions = Server.GetSessions(s => s.Sim == session.Sim);
            if (sessions.Count() > 0)
            {
                foreach (var item in sessions)
                {
                    item.Send(buffer.Concat(endMark).ToArray(), 0, buffer.Length + 4);
                }
            }
            else
            {
                session.Close();
            }
        }
    }
}
