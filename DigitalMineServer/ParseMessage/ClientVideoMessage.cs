using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using JtLibrary.Structures;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DigitalMineServer.ParseMessage
{
    //客户端实时视频请求
    class ClientVideoMessage
    {
        public void ParseOrder(ClientVideoSession session,byte[] buffer)
        {
            string[] orderItem = Encoding.UTF8.GetString(buffer).Trim('$').Split('!');
            switch (orderItem[0])
            {
                //视频请求
                case "video":
                    session.Sim = orderItem[1];
                    session.Port = byte.Parse(orderItem[3]);
                    VehicleVideoServer Server = JtServerForm.bootstrap.GetServerByName("VehicleVideoServer") as VehicleVideoServer;
                    var sessions = Server.GetSessions(s => s.Sim == orderItem[1] && s.Port == byte.Parse(orderItem[3]));
                    if (sessions.Count() == 0)
                    {
                        SendMessage(new REP9101().R9101(orderItem), orderItem, session);                
                    }
                    break;
                //视频控制请求
                case "videoControl":
                    SendMessage(new REP9102().R9102(orderItem), orderItem, session);
                    break;
                default:
                    session.Close();
                    break;
            }
        }

        private void SendMessage(byte[] buffer, string[] orderItem, ClientVideoSession session)
        {
            //获取终端连接下发指令
            Jt808Server Jt808Server = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = Jt808Server.GetSessions(s => s.Sim == orderItem[1]);
            if (sessions.Count() == 1)
            {
                if (!sessions.ElementAt(0).TrySend(buffer, 0, buffer.Length))
                {
                    session.Close();
                }
            }
            else
            {
                session.Close();
            }
        }
    }
}
