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
using DigitalMineServer.Redis;
using static DigitalMineServer.Structures.Comprehensive;
using JtLibrary.Utils;
using System.ComponentModel.DataAnnotations;
using static ServiceStack.Script.Lisp;

namespace DigitalMineServer.SuperSocket.Command
{
    public class WebOrder : SubCommandBase<WebSession>
    {
        private readonly OrderMessageDecode Decode;

        private readonly RedisHelper redis;

        public WebOrder()
        {
            Decode = new OrderMessageDecode();
            redis = new RedisHelper();
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

                case OrderMessageType.deleteFenceBySim:
                    DeleteFence deleteFenceBySim = Decode.DeleteFenceBySim(requestInfo.Body);
                    redis.Delete(deleteFenceBySim.sim + deleteFenceBySim.fenchType);
                    break;

                case OrderMessageType.deleteFenceByName:
                    DeleteFence deleteFenceByName = Decode.DeleteFenceByName(requestInfo.Body);
                    foreach (string sim in deleteFenceByName.simList)
                    {
                        Dictionary<string, (string, string, string, string, string, List<Point>)> FenceByName = redis.GetFench(sim, deleteFenceByName.fenchType);
                        foreach (var val in FenceByName)
                        {
                            if (val.Key == deleteFenceByName.name)
                            {
                                FenceByName.Remove(val.Key);
                                break;
                            }
                        }
                    }
                    break;

                case OrderMessageType.deleteFenceByNameAndSim:
                    DeleteFence deleteFenceByNameAndSim = Decode.DeleteFenceByNameAndSim(requestInfo.Body);
                    Dictionary<string, (string, string, string, string, string, List<Point>)> FenceByNameAndSim = redis.GetFench(deleteFenceByNameAndSim.sim, deleteFenceByNameAndSim.fenchType);
                    foreach (var val in FenceByNameAndSim)
                    {
                        if (val.Key == deleteFenceByNameAndSim.name)
                        {
                            FenceByNameAndSim.Remove(val.Key);
                            break;
                        }
                    }
                    break;

                case OrderMessageType.deleteVehicle:
                    DeleteVehicle deleteVehicle = Decode.DeleteVehicle(requestInfo.Body);
                    redis.Delete(deleteVehicle.sim + Redis_key_ext.vehicle);
                    break;

                case OrderMessageType.deletePerson:
                    DeletePerson DeletePerson = Decode.DeletePerson(requestInfo.Body);
                    redis.Delete(DeletePerson.sim + Redis_key_ext.person);
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