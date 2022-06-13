using DigitalMineServer.SuperSocket;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;

namespace DigitalMineServer.ParseMessage
{
    //客户端历史音频连接，目前未启用
    class ClientHistoryAudioMessage
    {
        private readonly byte[] error = Encoding.UTF8.GetBytes("未发现设备");
        public void ParseOrder(ClientHistoryAudioSession session,byte[] buffer)
        {  
            string[] orderItem = Encoding.UTF8.GetString(buffer).Trim('$').Split('!');
            switch (orderItem[0])
            {
                case "audio":
                    session.Sim = orderItem[1];
                    SendMessage(new REP9201().R9201(orderItem), orderItem[1], session);
                    break;
                case "audioControl":
                    session.Sim = orderItem[1];
                    SendMessage(new REP9102().R9102(orderItem), orderItem[1], session);
                    break;
                default:
                    session.Close();
                    break;
            }
        }

        private void SendMessage(byte[] buffer, string sim, ClientHistoryAudioSession session)
        {
            Jt808Server Jt808Server = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = Jt808Server.GetSessions(s => s.Sim == sim);
            if (sessions.Count()== 1)
            {
                if (!sessions.ElementAt(0).TrySend(buffer, 0, buffer.Length))
                {
                    session.Send(error, 0, error.Length);
                } 
            }
            else
            {
                session.Send(error, 0, error.Length);
            }
        }
    }
}
