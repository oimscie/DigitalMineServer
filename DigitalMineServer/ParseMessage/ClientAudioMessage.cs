using DigitalMineServer.Utils;
using DigitalMineServer.OrderMessage;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.Util;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalMineServer.ParseMessage
{
    //客户端音频消息
   public class ClientAudioMessage
    {
        private readonly OrderMessageDecode Decode;
        public ClientAudioMessage()
        {
            Decode = new OrderMessageDecode();
        }
        public void ParseOrder(ClientAudioSession Session, byte[] buffer)
        {
            //判断消息长度，音频消息最少324位
            if (buffer.Length > 100)
            {
                SendAudio(buffer, Session);
            }
            else
            {
                switch (Decode.GetMessageHead(buffer))
                {
                    //音频请求
                    case OrderMessageType.AudioAndVideo:
                        AudioAndVideo Audio = Decode.AudioAndVideo(buffer);
                        SendMessage(new REQ_9101().R9101(Audio), Audio.sim);
                        Session.Sim = Audio.sim;
                        break;
                    //音频控制
                    case OrderMessageType.AudioAndVideoControl:
                        /*   AudioAndVideoControl control=Decode.A
                           SendMessage(new REQ9102().R9102(orderItem), orderItem[1]);*/
                        break;
                    default:
                        Session.Close();
                        break;
                }
            }


        }
        private void SendMessage(byte[] buffer, string sim)
        {
            //检查是否有客户端已经发起音频请求，如果存在忽略该请求
            ClientAudioServer Server = JtServerForm.bootstrap.GetServerByName("ClientAudioServer") as ClientAudioServer;
            var sessions = Server.GetSessions(s => s.Sim == sim);
            if (sessions.Count() > 0)
            {
                return;
            }
            //上述检查若不存在已经发起的通话则查找终端并下发通话请求
            Jt808Server Jt808Server = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var Jt808sessions = Jt808Server.GetSessions(s => s.Sim == sim);
            if (Jt808sessions.Count() == 1)
            {
                foreach (var item in Jt808sessions)
                {
                    item.Send(buffer, 0, buffer.Length);
                }
            }
        }
        private void SendAudio(byte[] buffer, ClientAudioSession Session)
        {
            //查找终端连接头并发送用户端上传的音频
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
