using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using JtLibrary;
using JtLibrary.PacketBody;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
namespace DigitalMineServer.ParseMessage
{
    //终端历史音频，目前未启用
    class VehicleHistoryAudioMessage
    {
        public void Parse(VehicleHistoryAudioSession session,byte[] buffer)
        {
            if (session.Sim == null)
            {
                Video bodyinfo = new ParseVehicleVideoAndAudio().Decode(buffer);
                session.Sim = Extension.BCDToString(bodyinfo.SIM);
            }
            ClientHistoryAudioServer Server = JtServerForm.bootstrap.GetServerByName("ClientHistoryAudioServer") as ClientHistoryAudioServer;
            var sessions = Server.GetSessions( s => s.Sim == session.Sim);
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
