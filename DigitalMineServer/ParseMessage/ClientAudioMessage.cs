using DigitalMineServer.implement;
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
    class ClientAudioMessage
    {
        public void ParseOrder(ClientAudioSession Session,byte[] buffer)
        {
            //判断消息长度，长度大于30直接转发终端，音频消息头26位结束符4位共计30位
            if (buffer.Length > 30)
            {
                SendAudio(buffer, Session);
            }
            else
            {
                string[] orderItem = Encoding.UTF8.GetString(buffer).Split('!');
                switch (orderItem[0])
                {
                    //音频请求
                    case "audio":
                        SendMessage(new REP9101().R9101(orderItem), orderItem[1]);
                        Session.Sim = orderItem[1];
                        break;
                    //音频控制
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
                foreach (var item in Jt808sessions) {
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
