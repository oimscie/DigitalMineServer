using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using JtLibrary;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using SuperSocket.SocketBase;
using System.Linq;

namespace DigitalMineServer.ParseMessage
{
    //终端视频消息
    public class VehicleVideoMessage
    {
        //消息结束符
        private readonly byte[] endMark = new byte[] { 11, 22, 33, 44 };
        public void Parse(VehicleVideoSession session, byte[] buffer)
        {
            //判断是否是首次连接，若是则解析消息获取SIM和通道号
            if (session.Sim == null)
            {
                Video bodyinfo = new RtpDecoding().Decode(buffer);
                session.Sim = Extension.BCDToString(bodyinfo.SIM);
                session.Port = bodyinfo.ID;
            }
            //获取客户端录像请求连接头下发视频流
            ClientVideoServer Server = JtServerForm.bootstrap.GetServerByName("ClientVideoServer") as ClientVideoServer;
            var sessions = Server.GetSessions(s => s.Sim == session.Sim && s.Port == session.Port);
            if (sessions.Count() > 0)
            {
                foreach (var item in sessions)
                {
                    item.Send(buffer.Concat(endMark).ToArray(), 0, buffer.Length + 4);
                }
            }
            else
            {
                session.Close();
            }
        }
    }
}
