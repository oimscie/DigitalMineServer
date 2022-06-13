using DigitalMineServer.implement;
using DigitalMineServer.SuperSocket.ReceiveFilter;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket.SocketServer
{
    public class ClientHistoryVideoServer : AppServer<ClientHistoryVideoSession, BinaryRequestInfo>
    {
        public ClientHistoryVideoServer() : base(new DefaultReceiveFilterFactory<ClientHistoryVideoReceiveFilter, BinaryRequestInfo>()) { }
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
        protected override void OnNewSessionConnected(ClientHistoryVideoSession session)
        {
            base.OnNewSessionConnected(session);
            implement.Util.ModifyLable(JtServerForm.JtForm.ClientHistoryVideo, JtServerForm.bootstrap.GetServerByName("ClientHistoryVideoServer").SessionCount.ToString());
        }
        protected override void OnSessionClosed(ClientHistoryVideoSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            implement.Util.ModifyLable(JtServerForm.JtForm.ClientHistoryVideo, JtServerForm.bootstrap.GetServerByName("ClientHistoryVideoServer").SessionCount.ToString());
        }
    }
}
