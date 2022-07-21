using DigitalMineServer.implement;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket
{
    public class ClientHistoryVideoSession : AppSession<ClientHistoryVideoSession, BinaryRequestInfo>
    {
        /// <summary>
        /// 终端sim
        /// </summary>
        public string Sim { get; set; }
        /// <summary>
        /// 通道号
        /// </summary>
        public byte Id { get; set; }
        public override void Send(string message)
        { 
            base.Send(message);
        }
        public override void Send(byte[] data, int offset, int length)
        {
            base.Send(data,offset,length);
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
            implement.Util.AppendText(JtServerForm.JtForm.infoBox, "未知请求");
            base.HandleUnknownRequest(requestInfo);
        }
    }
}
