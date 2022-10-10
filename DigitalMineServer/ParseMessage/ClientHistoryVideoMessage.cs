using DigitalMineServer.Utils;
using DigitalMineServer.OrderMessage;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DigitalMineServer.ParseMessage
{
    //客户端历史视频消息
    class ClientHistoryVideoMessage
    {
        private readonly OrderMessageDecode Decode;
        public ClientHistoryVideoMessage()
        {
            Decode = new OrderMessageDecode();
        }
        public void ParseOrder(ClientHistoryVideoSession session,byte[] buffer)
        {
            //客户端录像请求
            switch (Decode.GetMessageHead(buffer))
            {
                case OrderMessageType.HisVideoAndAudio:
                    HisVideoAndAudio HisVideo = Decode.HisVideoAndAudio(buffer);
                    session.Sim = HisVideo.sim;
                    session.Id = byte.Parse(HisVideo.id);
                    //录像通道唯一，判断是否存在已经发起的录像，如果存在直接断开客户的连接。
                    VehicleHistoryVideoServer Server = JtServerForm.bootstrap.GetServerByName("VehicleHistoryVideoServer") as VehicleHistoryVideoServer;
                    var sessions = Server.GetSessions(s => s.Sim == HisVideo.sim && s.Id == byte.Parse(HisVideo.id));
                    if (sessions.Count() == 0)
                    {
                        SendMessage(new REQ_9201().R9201(HisVideo), HisVideo.sim, session);
                    }
                    else
                    {
                        session.Close();
                    }
                    break;
                //录像控制请求
                case "hisVideoControl":
                  //  HisVideoAndAudioControl HisVideoControl = Decode.HisVideoAndAudio(buffer);
                  //  SendMessage(new REQ9202().R9202(orderItem), orderItem, session);
                    break;
                default:
                    session.Close();
                    break;
            }
        }

        private void SendMessage(byte[] buffer, string sim, ClientHistoryVideoSession session)
        {
            //获取终端连接下发指令
            Jt808Server Jt808Server = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = Jt808Server.GetSessions(s => s.Sim == sim);
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
