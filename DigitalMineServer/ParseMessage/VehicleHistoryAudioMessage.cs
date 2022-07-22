using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using JtLibrary;
using JtLibrary.Jt1078_2016.RtpPacketDecode;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using SuperSocket.SocketBase;
using System.Linq;
namespace DigitalMineServer.ParseMessage
{
    //终端历史音频，目前未启用
    class VehicleHistoryAudioMessage
    {
        public void Parse(VehicleHistoryAudioSession session, byte[] buffer)
        {
            if (session.Sim == null)
            {
                RtpDecoding decode = new RtpDecoding();
                Video bodyinfo = decode.Decode(buffer, decode.Check1078Versioin(buffer));
                session.Sim = Extension.BCDToString(bodyinfo.SIM);
            }
            ClientHistoryAudioServer Server = JtServerForm.bootstrap.GetServerByName("ClientHistoryAudioServer") as ClientHistoryAudioServer;
            var sessions = Server.GetSessions(s => s.Sim == session.Sim);
            if (sessions.Count() > 0)
            {
                foreach (var item in sessions)
                {
                    try
                    {
                        item.Send(buffer, 0, buffer.Length);
                    }
                    catch
                    {
                        item.Send(buffer, 0, buffer.Length);
                    }
                }
            }
            else
            {
                session.Close();
            }
        }
    }
}
