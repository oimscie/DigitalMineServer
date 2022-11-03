using DigitalMineServer.Utils;
using DigitalMineServer.OrderMessage;
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
using DigitalMineServer.Util;
using SuperSocket.SocketBase.Protocol;

namespace DigitalMineServer.SuperSocket.Command
{
    public class WebOrder : SubCommandBase<WebSession>
    {
        private readonly OrderMessageDecode Decode;

        public WebOrder()
        {
            Decode = new OrderMessageDecode();
        }

        public override void ExecuteCommand(WebSession session, SubRequestInfo requestInfo)
        {
            switch (Decode.GetMessageHead(requestInfo.Body))
            {
                case OrderMessageType.WebOrderHeart:
                    break;

                case OrderMessageType.AudioAndVideo:
                    SendMessage(requestInfo.Body, session);
                    break;

                case OrderMessageType.HisVideoAndAudio:
                    SendMessage(requestInfo.Body, session);
                    break;

                case OrderMessageType.MonitorOpen:
                    SendMessage(requestInfo.Body, session);
                    break;

                case OrderMessageType.WebText:
                    WebText WebText = Decode.WebText(requestInfo.Body);
                    SendMessage(new REQ_8300().R8300(WebText.sim, WebText.text), WebText.sim, session);
                    break;

                case OrderMessageType.deleteFence:
                    DeleteFence deleteFence = Decode.DeleteFence(requestInfo.Body);
                    Resource.VehicleFenceFanbidInInfo.TryRemove(deleteFence.sim, out _);
                    Resource.VehicleFenceFanbidOutInfo.TryRemove(deleteFence.sim, out _);
                    break;

                case OrderMessageType.deleteVehicle:
                    DeleteVehicle deleteVehicle = Decode.DeleteVehicle(requestInfo.Body);
                    Resource.VehicleList.TryRemove(deleteVehicle.sim, out _);
                    break;

                case OrderMessageType.deletePerson:
                    DeletePerson DeletePerson = Decode.DeletePerson(requestInfo.Body);
                    Resource.PersonList.TryRemove(DeletePerson.sim, out _);
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
            var sessions = a.GetSessions(s => s.Uuid == uuid);
            if (sessions.Count() != 0)
            {
                foreach (var ClientSession in sessions)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(body);
                    if (!ClientSession.TrySend(buffer, 0, buffer.Length))
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