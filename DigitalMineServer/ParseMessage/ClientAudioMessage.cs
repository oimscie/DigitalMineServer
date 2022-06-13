using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalMineServer.ParseMessage
{
    class ClientAudioMessage
    {
        public void ParseOrder(ClientAudioSession Session,byte[] buffer)
        {
            if (buffer.Length > 30)
            {
                SendAudio(buffer, Session);
            }
            else
            {
                string[] orderItem = Encoding.UTF8.GetString(buffer).Split('!');
                switch (orderItem[0])
                {
                    case "audio":
                        SendMessage(new REP9101().R9101(orderItem), orderItem[1]);
                        Session.Sim = orderItem[1];
                        break;
                    case "audioControl":
                        SendMessage(new REP9102().R9102(orderItem), orderItem[1]);
                        break;
                    default:
                        Session.Close();
                        break;
                }
            }


        }
        private void SendMessage(byte[] buffer, string sim)
        {
            ClientAudioServer Server = JtServerForm.bootstrap.GetServerByName("ClientAudioServer") as ClientAudioServer;
            var sessions = Server.GetSessions(s => s.Sim == sim);
            if (sessions.Count() > 0)
            {
                return;
            }
            Jt808Server Jt808Server = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var Jt808sessions = Jt808Server.GetSessions(s => s.Sim == sim);
            if (Jt808sessions.Count() == 1)
            {
                foreach (var item in Jt808sessions) {
                    item.Send(buffer, 0, buffer.Length);
                }
            }
        }
        private void SendAudio(byte[] buffer, ClientAudioSession Session)
        {
            VehicleAudioServer VehicleAudioServer = JtServerForm.bootstrap.GetServerByName("VehicleAudioServer") as VehicleAudioServer;
            var sessions = VehicleAudioServer.GetSessions(s => s.Sim == Session.Sim);
            if (sessions.Count() == 1)
            {
                foreach (var item in sessions)
                {
                    item.Send(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
