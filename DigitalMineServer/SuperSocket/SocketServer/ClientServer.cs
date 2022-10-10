
using DigitalMineServer.Utils;
using DigitalMineServer.SuperSocket.ReceiveFilter;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using CloseReason = SuperSocket.SocketBase.CloseReason;
namespace DigitalMineServer.SuperSocket.SocketServer
{
    public class ClientServer : AppServer<ClientSession, BinaryRequestInfo>
    {
        public ClientServer() : base(new DefaultReceiveFilterFactory<ClientReceiveFilter, BinaryRequestInfo>()) { }
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
        protected override void OnNewSessionConnected(ClientSession session)
        {
            base.OnNewSessionConnected(session);
            Utils.Util.ModifyLable(JtServerForm.JtForm.UserClient, JtServerForm.bootstrap.GetServerByName("ClientServer").SessionCount.ToString());
        }
        protected override void OnSessionClosed(ClientSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            Utils.Util.ModifyLable(JtServerForm.JtForm.UserClient, JtServerForm.bootstrap.GetServerByName("ClientServer").SessionCount.ToString());
        }
    }

}
