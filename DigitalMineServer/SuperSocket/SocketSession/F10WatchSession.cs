using DigitalMineServer.Utils;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket
{
    public class F10WatchSession : AppSession<F10WatchSession, BinaryRequestInfo>
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string Type { get; set; }

        public override void Send(string message)
        {
            base.Send(message);
        }

        public override void Send(byte[] data, int offset, int length)
        {
            base.Send(data, offset, length);
        }

        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void HandleUnknownRequest(BinaryRequestInfo requestInfo)
        {
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "未知请求");
            base.HandleUnknownRequest(requestInfo);
        }
    }
}