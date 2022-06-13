using DigitalMineServer.implement;
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
    class MontorMessage
    {
        private readonly byte[] mark = new byte[] {11, 22, 33, 44 };
        public void ParseOrder(MontorSession Session,byte[] buffer)
        {
          
            if (Session.Company!=null)
            {
                Send(buffer, Session);
            }
            else
            {
              
                string[] orderItem = Encoding.UTF8.GetString(buffer).Split('!');
                switch (orderItem[0])
                {
                    case "monitorOpen":
                        Session.Company = orderItem[1];
                        Session.CameraIP = orderItem[2];
                        Session.CameraPort = orderItem[3];
                        Session.Brand = orderItem[6];
                        Session.Type = "user";
                        MontorServer temp= JtServerForm.bootstrap.GetServerByName("MontorServer") as MontorServer;
                        if (temp.GetSessions(s => s.Type == "upload" && s.Company == Session.Company && s.CameraIP == Session.CameraIP && s.CameraPort == Session.CameraPort).Count()==0) {
                            Send(buffer, Session);
                        }
                        break;
                    case "monitorUpload":
                        Session.Company = orderItem[1];
                        Session.CameraIP = orderItem[2];
                        Session.CameraPort = orderItem[3];
                        Session.Brand = orderItem[6];
                        Session.Type ="upload";                    
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
            switch (Session.Type) {
                case "user":
                    switch (Encoding.UTF8.GetString(buffer).Split('!')[0]) {
                        case "Control":
                            MontorServer temp = JtServerForm.bootstrap.GetServerByName("MontorServer") as MontorServer;
                            var sessions3 = temp.GetSessions(s => s.Type == "upload" && s.Company == Session.Company && s.CameraIP == Session.CameraIP && s.CameraPort == Session.CameraPort);
                            if (sessions3.Count() > 0)
                            {
                                foreach (var item in sessions3)
                                {
                                    item.Send(buffer, 0, buffer.Length);
                                }
                            }
                            break;
                        default:
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
                case "upload":
                    MontorServer MontorServer = JtServerForm.bootstrap.GetServerByName("MontorServer") as MontorServer;
                    var sessions2 = MontorServer.GetSessions(s => s.Type == "user" && s.Company == Session.Company && s.CameraIP == Session.CameraIP && s.CameraPort == Session.CameraPort);
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
