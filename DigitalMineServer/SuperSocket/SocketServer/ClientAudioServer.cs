using DigitalMineServer.Utils;
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
    public class ClientAudioServer : AppServer<ClientAudioSession, BinaryRequestInfo>
    {
        public ClientAudioServer() : base(new DefaultReceiveFilterFactory<ClientAudioReceiveFilter, BinaryRequestInfo>()) { }
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
        protected override void OnNewSessionConnected(ClientAudioSession session)
        {
            base.OnNewSessionConnected(session);
            Utils.Util.ModifyLable(JtServerForm.JtForm.ClentAudio, JtServerForm.bootstrap.GetServerByName("ClientAudioServer").SessionCount.ToString());
        }
        protected override void OnSessionClosed(ClientAudioSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
            Utils.Util.ModifyLable(JtServerForm.JtForm.ClentAudio, JtServerForm.bootstrap.GetServerByName("ClientAudioServer").SessionCount.ToString());
        }
    }
}
