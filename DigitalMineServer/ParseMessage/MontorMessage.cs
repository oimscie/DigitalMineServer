using DigitalMineServer.Utils;
using DigitalMineServer.OrderMessage;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.SuperSocket.SocketSession;
using DigitalMineServer.Util;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalMineServer.ParseMessage
{
    //用户数据处理端与客户端监控连接消息
    class MontorMessage
    {
        private readonly OrderMessageDecode Decode;
        private readonly byte[] mark;
        public MontorMessage()
        {
            Decode = new OrderMessageDecode();
            //消息结束符
            mark = new byte[] { 11, 22, 33, 44 };
        }
        public void ParseOrder(MontorSession Session,byte[] buffer)
        {
          //判断是否是初次连接，以company为准，非初次连接时数据之间下发
            if (Session.Company!=null)
            {
                Send(buffer, Session);
            }
            else
            {
                switch (Decode.GetMessageHead(buffer))
                {
                    //客户端打开监控请求
                    case OrderMessageType.MonitorOpen:
                        MonitorOpen MonitorOpen = Decode.MonitorOpen(buffer);
                        Session.Company = MonitorOpen.Company;
                        Session.CameraIP = MonitorOpen.CameraIP;
                        Session.CameraPort = MonitorOpen.CameraPort;
                        Session.Brand = MonitorOpen.Brand;
                        Session.Type = OrderMessageType.MonitorOpen;
                        //获取监控连接头下发指令
                        MontorServer temp= JtServerForm.bootstrap.GetServerByName("MontorServer") as MontorServer;
                        if (temp.GetSessions(s => s.Type == "upload" && s.Company == Session.Company && s.CameraIP == Session.CameraIP && s.CameraPort == Session.CameraPort).Count()==0) {
                            Send(buffer, Session);
                        }
                        break;
                    //用户数据处理端监控视频上传
                    case OrderMessageType.MonitorUpload:
                        MonitorUpload MonitorUpload = Decode.MonitorUpload(buffer);
                        Session.Company = MonitorUpload.Company;
                        Session.CameraIP = MonitorUpload.CameraIP;
                        Session.CameraPort = MonitorUpload.CameraPort;
                        Session.Brand = MonitorUpload.Brand;
                        Session.Type = OrderMessageType.MonitorUpload;                    
                        Send(buffer, Session);
                        break;
                    default:
                        Session.Close();
                        break;
                }
            }
        }
        private void Send(byte[] buffer, MontorSession Session)
        {
            //判断连接类型
            switch (Session.Type) {
                //客户端
                case OrderMessageType.MonitorOpen:
                    switch (Encoding.UTF8.GetString(buffer).Split('!')[0]) {
                        //控制指令
                        case OrderMessageType.MonitorControl:
                            MontorServer temp = JtServerForm.bootstrap.GetServerByName("MontorServer") as MontorServer;
                            var sessions3 = temp.GetSessions(s => s.Type == OrderMessageType.MonitorUpload && s.Company == Session.Company && s.CameraIP == Session.CameraIP && s.CameraPort == Session.CameraPort);
                            if (sessions3.Count() > 0)
                            {
                                foreach (var item in sessions3)
                                {
                                    item.Send(buffer, 0, buffer.Length);
                                }
                            }
                            break;
                        //默认发起监控视频请求
                        default:
                            //获取用户端数据处理连接下发指令，若不存在则直接断开
                            DataServer DataServer = JtServerForm.bootstrap.GetServerByName("DataServer") as DataServer;
                            var sessions = DataServer.GetSessions(s => s.Company == Session.Company);
                            if (sessions.Count() > 0)
                            {
                                foreach (var item in sessions)
                                {
                                    item.Send(buffer, 0, buffer.Length);
                                }
                            }
                            else {
                                Session.Close();
                            }
                            break;
                    }
                    break;
                //用户端数据处理软件
                case OrderMessageType.MonitorUpload:
                    //获取的客户端连接下发视频流，客户端的session.type是user
                    MontorServer MontorServer = JtServerForm.bootstrap.GetServerByName("MontorServer") as MontorServer;
                    var sessions2 = MontorServer.GetSessions(s => s.Type == OrderMessageType.MonitorOpen && s.Company == Session.Company && s.CameraIP == Session.CameraIP && s.CameraPort == Session.CameraPort);
                    if (sessions2.Count() > 0)
                    {
                        foreach (var item in sessions2)
                        {
                            byte[] temp = buffer.Concat(mark).ToArray();
                            item.Send(temp, 0, temp.Length);
                        }
                    }
                    else {
                        Session.Close();
                    }
                    break;
                default:
                    Session.Close();
                    break;
            }
        }
    }
}
