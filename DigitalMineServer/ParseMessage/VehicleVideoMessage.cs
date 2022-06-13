using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using JtLibrary;
using JtLibrary.PacketBody;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalMineServer.ParseMessage
{
    public class VehicleVideoMessage
    {
        private readonly byte[] endMark = new byte[] { 11, 22, 33, 44 };
        public void Parse(VehicleVideoSession session,byte[]buffer)
        {
            if (session.Sim == null)
            {
                Video bodyinfo = new ParseVehicleVideoAndAudio().Decode(buffer);
                session.Sim = Extension.BCDToString(bodyinfo.SIM);
                session.Port = bodyinfo.ID;
            }
            ClientVideoServer Server = JtServerForm.bootstrap.GetServerByName("ClientVideoServer") as ClientVideoServer;
            var sessions = Server.GetSessions(s => s.Sim == session.Sim && s.Port == session.Port);
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
