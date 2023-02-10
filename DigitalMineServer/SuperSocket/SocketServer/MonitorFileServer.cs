using DigitalMineServer.Utils;
using DigitalMineServer.SuperSocket.ReceiveFilter;
using DigitalMineServer.SuperSocket.SocketSession;
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
    public class MonitorFileServer : AppServer<MonitorFileSession, BinaryRequestInfo>
    {
        public MonitorFileServer() : base(new DefaultReceiveFilterFactory<MonitorFileReceiveFilter, BinaryRequestInfo>())
        {
        }

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

        protected override void OnNewSessionConnected(MonitorFileSession session)
        {
            base.OnNewSessionConnected(session);
            Utils.Util.ModifyLable(JtServerForm.JtForm.monitorFile, JtServerForm.bootstrap.GetServerByName("MonitorFileServer").SessionCount.ToString());
        }

        protected override void OnSessionClosed(MonitorFileSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            Utils.Util.ModifyLable(JtServerForm.JtForm.monitorFile, JtServerForm.bootstrap.GetServerByName("MonitorFileServer").SessionCount.ToString());
        }
    }
}