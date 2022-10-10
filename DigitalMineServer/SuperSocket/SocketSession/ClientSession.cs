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
    public class ClientSession : AppSession<ClientSession, BinaryRequestInfo>
    {

        public string Sim { get; set; }
        public string Uuid { get; set; }
        public string Company { get; set; }
        public string Port { get; set; }
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
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "未知请求");
            base.HandleUnknownRequest(requestInfo);
        }
    }
}
