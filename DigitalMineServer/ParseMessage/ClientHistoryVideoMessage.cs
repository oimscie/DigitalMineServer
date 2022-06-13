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
    class ClientHistoryVideoMessage
    {
        public void ParseOrder(ClientHistoryVideoSession session,byte[] buffer)
        {
            string[] orderItem = Encoding.UTF8.GetString(buffer).Trim('$').Split('!');
            switch (orderItem[0])
            {
                case "hisVideo":
                    session.Sim = orderItem[1];
                    session.Port = byte.Parse(orderItem[5]);
                    VehicleHistoryVideoServer Server = JtServerForm.bootstrap.GetServerByName("VehicleHistoryVideoServer") as VehicleHistoryVideoServer;
                    var sessions = Server.GetSessions(s => s.Sim == orderItem[1] && s.Port == byte.Parse(orderItem[5]));
                    if (sessions.Count() == 0)
                    {
                        SendMessage(new REP9201().R9201(orderItem), orderItem, session);
                    }
                    else
                    {
                        session.Close();
                    }
                    break;
                case "hisVideoControl":
                    SendMessage(new REP9202().R9202(orderItem), orderItem, session);
                    break;
                default:
                    session.Close();
                    break;
            }
        }

        private void SendMessage(byte[] buffer, string[] orderItem, ClientHistoryVideoSession session)
        {
            Jt808Server Jt808Server = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = Jt808Server.GetSessions(s => s.Sim == orderItem[1]);
            if (sessions.Count() == 1)
            {
                sessions.ElementAt(0).Send(buffer, 0, buffer.Length);
             }
            else
            {
                session.Close();
            }
        }
    }
}
