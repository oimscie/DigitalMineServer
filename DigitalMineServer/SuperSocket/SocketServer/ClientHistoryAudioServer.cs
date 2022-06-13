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
    public class ClientHistoryAudioServer : AppServer<ClientHistoryAudioSession, BinaryRequestInfo>
    {
        public ClientHistoryAudioServer() : base(new DefaultReceiveFilterFactory<ClientHistoryAudioReceiveFilter, BinaryRequestInfo>()) { }
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
        protected override void OnNewSessionConnected(ClientHistoryAudioSession session)
        {
            base.OnNewSessionConnected(session);
            implement.Util.ModifyLable(JtServerForm.JtForm.ClientHistoryAudio, JtServerForm.bootstrap.GetServerByName("ClientHistoryAudioServer").SessionCount.ToString());
        }
        protected override void OnSessionClosed(ClientHistoryAudioSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            implement.Util.ModifyLable(JtServerForm.JtForm.ClientHistoryAudio, JtServerForm.bootstrap.GetServerByName("ClientHistoryAudioServer").SessionCount.ToString());
        }
    }
}
