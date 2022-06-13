﻿using DigitalMineServer.implement;
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
   public class FileServer : AppServer<FileSession, BinaryRequestInfo>
    {
        public FileServer() : base(new DefaultReceiveFilterFactory<FileReceiveFilter, BinaryRequestInfo>()) { }
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
        protected override void OnNewSessionConnected(FileSession session)
        {
            base.OnNewSessionConnected(session);
        }
        protected override void OnSessionClosed(FileSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
        }
    }
}
