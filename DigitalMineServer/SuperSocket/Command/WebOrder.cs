using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.SuperSocket.SocketSession;
using JtLibrary.PacketBody;
using SuperSocket.SocketBase;
using SuperSocket.WebSocket.SubProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DigitalMineServer.SuperSocket.Command
{
    public class WebOrder : SubCommandBase<WebSession>
    {
        public override void ExecuteCommand(WebSession session, SubRequestInfo requestInfo)
        {      
            string[] item = requestInfo.Body.Split('!');
            switch (item[0])
            {
                case "heart":
                    break;
                case "vehicleLive":
                    SendMessage(requestInfo.Body, session);
                    break;
                case "vehiclePlayBack":
                    SendMessage(requestInfo.Body, session);
                    break;
                case "monitorOpen":
                    SendMessage(requestInfo.Body, session);
                    break;
                case "text":
                    SendMessage(new REQ8300().R8300(item[1],item[3]), item[1], session);
                    break;
                case "deleteFence":
                    Resource.fenceFanbidInInfo.TryRemove(item[1], out _);
                    Resource.fenceFanbidOutInfo.TryRemove(item[1], out _);
                    break;
                case "deleteVehicle":
                    Resource.VehicleList.TryRemove(item[1], out _);
                    break;
                default:
                    session.Close();
                    break;
            }
        }
        private void SendMessage(string body, WebSession session)
        {
            ClientServer a = JtServerForm.bootstrap.GetServerByName("ClientServer") as ClientServer;
            string uuid = session.Uuid.IndexOf('-') == -1 ? session.Uuid : session.Uuid.Split('-')[0];
            var sessions = a.GetSessions(s => s.Uuid ==uuid);
            if (sessions.Count() != 0)
            {
                foreach (var ClientSession in sessions)
                {
                    if (!ClientSession.TrySend(Encoding.UTF8.GetBytes(body), 0, Encoding.UTF8.GetBytes(body).Length))
                    {
                        session.TrySend("发送失败，未知错误");
                    }
                }
            }
            else
            {
                session.TrySend("未检测到专用客户端");
            }
        }
        private void SendMessage(byte[] buffer, string sim, WebSession session)
        {
            Jt808Server a = JtServerForm.bootstrap.GetServerByName("Jt808Server") as Jt808Server;
            var sessions = a.GetSessions(s => s.Sim == sim);
            if (sessions.Count() != 0)
            {
                foreach (var vehicleSession in sessions)
                {
                    if (!vehicleSession.TrySend(buffer, 0, buffer.Length))
                    {
                        session.TrySend("发送失败，未知错误");
                    }
                }
            }
            else
            {
                session.TrySend("发送失败，当前车辆离线");
            }
        }
    }
}
