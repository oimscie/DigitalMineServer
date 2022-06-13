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
    public class ClientVideoSession : AppSession<ClientVideoSession, BinaryRequestInfo>
    {

        public string Sim { get; set; }
        public byte Port { get; set; }
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
