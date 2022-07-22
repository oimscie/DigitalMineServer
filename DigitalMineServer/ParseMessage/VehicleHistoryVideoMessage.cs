using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using JtLibrary;
using JtLibrary.Jt1078_2016.RtpPacketDecode;
using JtLibrary.PacketBody;
using JtLibrary.Providers;
using SuperSocket.SocketBase;
using System.Linq;

namespace DigitalMineServer.ParseMessage
{
    //终端录像消息
    class VehicleHistoryVideoMessage
    {
        //消息结束符
        private readonly byte[] endMark = new byte[] { 11, 22, 33, 44 };
        public void Parse(VehicleHistoryVideoSession session, byte[] buffer)
        {
            //判断是否是首次连接，若是则解析消息获取SIM和通道号
            if (session.Sim == null)
            {
                RtpDecoding decode = new RtpDecoding();
                Video bodyinfo = decode.Decode(buffer, decode.Check1078Versioin(buffer));
                session.Sim = Extension.BCDToString(bodyinfo.SIM);
                session.Id = bodyinfo.ID;
            }
            byte[] temp = buffer.Concat(endMark).ToArray();
            //获取客户端录像请求连接头下发录像视频流
            ClientHistoryVideoServer Server = JtServerForm.bootstrap.GetServerByName("ClientHistoryVideoServer") as ClientHistoryVideoServer;
            var sessions = Server.GetSessions(s => s.Sim == session.Sim && s.Id == session.Id);
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
