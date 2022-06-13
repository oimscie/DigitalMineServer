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
    class VehicleAudioMessage
    {
        private readonly byte[] endMark = new byte[] { 11, 22, 33, 44 };
        public void Parse(VehicleAudioSession session,byte[] buffer)
        {
            if (session.Sim == null)
            {
                Video bodyinfo = new ParseVehicleVideoAndAudio().Decode(buffer);
                session.Sim = Extension.BCDToString(bodyinfo.SIM);
            }
            ClientAudioServer Server = JtServerForm.bootstrap.GetServerByName("ClientAudioServer") as ClientAudioServer;
            var sessions = Server.GetSessions(s => s.Sim == session.Sim);
            if (sessions.Count() > 0)
            {
                foreach (var item in sessions)
                {
                 item.Send(buffer.Concat(endMark).ToArray(), 0, buffer.Length+4);
                }
            }
            else
            {
                session.Close();
            }
        }
    }
}
