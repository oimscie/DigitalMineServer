using DigitalMineServer.implement;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket.SocketSession
{
    public class WebSession : WebSocketSession<WebSession>
    {
        public string Uuid { get; set; }
        protected override void OnSessionStarted()
        {
            Uuid = Path.Trim('/');
        }
        protected override void OnSessionClosed(CloseReason reason) {
            implement.Util.ModifyLable(JtServerForm.JtForm.webSocket, JtServerForm.bootstrap.GetServerByName("WebSocketServer").SessionCount.ToString());
        }
    }
 
}
