using DigitalMineServer.implement;
using DigitalMineServer.SuperSocket.Command;
using DigitalMineServer.SuperSocket.ReceiveFilter;
using DigitalMineServer.SuperSocket.SocketSession;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.WebSocket;
using SuperSocket.WebSocket.SubProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket.SocketServer
{
    public class WebSocketServer : WebSocketServer<WebSession>
    {

         public WebSocketServer()
        {

        }
        protected override void OnNewSessionConnected(WebSession session) {

            base.OnNewSessionConnected(session);
            implement.Util.ModifyLable(JtServerForm.JtForm.webSocket, JtServerForm.bootstrap.GetServerByName("WebSocketServer").SessionCount.ToString());
        }
    
    }
    
}
