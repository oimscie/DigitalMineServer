﻿using DigitalMineServer.Utils;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket.ReceiveFilter;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloseReason = SuperSocket.SocketBase.CloseReason;
using DigitalMineServer.Redis;
using static DigitalMineServer.Structures.Comprehensive;

namespace DigitalMineServer.SuperSocket.SocketServer
{
    public class Jt808Server : AppServer<Jt808Session, BinaryRequestInfo>
    {
        private RedisHelper Redis = new RedisHelper();

        public Jt808Server() : base(new DefaultReceiveFilterFactory<Jt808ReceiveFilter, BinaryRequestInfo>())
        {
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "正在准备" + this.Config.Name + "配置文件");
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, this.Config.Name + "监听服务已开始");
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, this.Config.Name + "监听服务已停止");
            base.OnStopped();
        }

        protected override void OnNewSessionConnected(Jt808Session session)
        {
            base.OnNewSessionConnected(session);
            Utils.Util.ModifyLable(JtServerForm.JtForm.vehicleOnline, JtServerForm.bootstrap.GetServerByName("Jt808Server").SessionCount.ToString());
        }

        protected override void OnSessionClosed(Jt808Session session, CloseReason reason)
        {
            string sim = session.Sim + Redis_key_ext.equipVersion;
            base.OnSessionClosed(session, reason);
            Redis.Delete(sim);
            Utils.Util.ModifyLable(JtServerForm.JtForm.vehicleOnline, JtServerForm.bootstrap.GetServerByName("Jt808Server").SessionCount.ToString());
        }
    }
}