using DigitalMineServer.Utils;
using DigitalMineServer.OrderMessage;
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
        private readonly OrderMessageDecode Decode;
        public ClientVideoMessage()
        {
            Decode = new OrderMessageDecode();
        }
        public void ParseOrder(ClientVideoSession session,byte[] buffer)
        {
            switch (Decode.GetMessageHead(buffer))
            {
                //视频请求
                case OrderMessageType.AudioAndVideo:
                    AudioAndVideo video = Decode.AudioAndVideo(buffer);
                    session.Sim = video.sim;
                    session.Id = byte.Parse(video.id);
                    VehicleVideoServer Server = JtServerForm.bootstrap.GetServerByName("VehicleVideoServer") as VehicleVideoServer;
                    var sessions = Server.GetSessions(s => s.Sim == video.sim && s.Id == byte.Parse(video.id));
                    if (sessions.Count() == 0)
                    {
                        SendMessage(new REQ_9101().R9101(video), video.sim, session);                
                    }
                    break;
                //视频控制请求
                case "videoControl":
                   // SendMessage(new REQ9102().R9102(orderItem), orderItem, session);
                    break;
                default:
                    session.Close();
                    break;
            }
        }

        private void SendMessage(byte[] buffer, string sim, ClientVideoSession session)
        {
            //获取终端连接下发指令
            Jt808Server Jt808Server = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = Jt808Server.GetSessions(s => s.Sim == sim);
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
