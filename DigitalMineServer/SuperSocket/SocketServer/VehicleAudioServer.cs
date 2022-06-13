
using DigitalMineServer.implement;
using DigitalMineServer.SuperSocket.ReceiveFilter;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using CloseReason = SuperSocket.SocketBase.CloseReason;

namespace DigitalMineServer.SuperSocket.SocketServer
{
    public class VehicleAudioServer : AppServer<VehicleAudioSession, BinaryRequestInfo>
    {
        public VehicleAudioServer() : base(new DefaultReceiveFilterFactory<VehicleAudioReceiveFilter, BinaryRequestInfo>()) { }
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            implement.Util.AppendText(JtServerForm.JtForm.infoBox, "正在准备" + Config.Name + "配置文件");
            return base.Setup(rootConfig, config);
        }
        protected override void OnStarted()
        {
            implement.Util.AppendText(JtServerForm.JtForm.infoBox, Config.Name + "监听服务已开始");
            base.OnStarted();
        }
        protected override void OnStopped()
        {
            implement.Util.AppendText(JtServerForm.JtForm.infoBox, Config.Name + "监听服务已停止");
            base.OnStopped();
        }
        protected override void OnNewSessionConnected(VehicleAudioSession session)
        {
            base.OnNewSessionConnected(session);
            implement.Util.ModifyLable(JtServerForm.JtForm.vehicleAudio, JtServerForm.bootstrap.GetServerByName("VehicleAudioServer").SessionCount.ToString());
        }
        protected override void OnSessionClosed(VehicleAudioSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            implement.Util.ModifyLable(JtServerForm.JtForm.vehicleAudio, JtServerForm.bootstrap.GetServerByName("VehicleAudioServer").SessionCount.ToString());
        }
    }

}
