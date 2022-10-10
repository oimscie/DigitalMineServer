﻿
using DigitalMineServer.Utils;
using DigitalMineServer.SuperSocket.ReceiveFilter;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using CloseReason = SuperSocket.SocketBase.CloseReason;

namespace DigitalMineServer.SuperSocket.SocketServer
{
    public class VehicleHistoryAudioServer : AppServer<VehicleHistoryAudioSession, BinaryRequestInfo>
    {
        public VehicleHistoryAudioServer() : base(new DefaultReceiveFilterFactory<VehicleHistoryAudioReceiveFilter, BinaryRequestInfo>()) { }
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "正在准备" + Config.Name + "配置文件");
            return base.Setup(rootConfig, config);
        }
        protected override void OnStarted()
        {
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, Config.Name + "监听服务已开始");
            base.OnStarted();
        }
        protected override void OnStopped()
        {
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, Config.Name + "监听服务已停止");
            base.OnStopped();
        }
        protected override void OnNewSessionConnected(VehicleHistoryAudioSession session)
        {
            base.OnNewSessionConnected(session);
            Utils.Util.ModifyLable(JtServerForm.JtForm.vehicleHistoryAudio, JtServerForm.bootstrap.GetServerByName("VehicleHistoryAudioServer").SessionCount.ToString());
        }
        protected override void OnSessionClosed(VehicleHistoryAudioSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            Utils.Util.ModifyLable(JtServerForm.JtForm.vehicleHistoryAudio, JtServerForm.bootstrap.GetServerByName("VehicleHistoryAudioServer").SessionCount.ToString());
        }
    }

}
