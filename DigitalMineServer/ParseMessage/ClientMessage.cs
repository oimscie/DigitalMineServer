using DigitalMineServer.Utils;
using DigitalMineServer.OrderMessage;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DigitalMineServer.ParseMessage
{
    //客户端消息
    class ClientMessage
    {
        private readonly OrderMessageDecode Decode;
        public ClientMessage()
        {
            Decode = new OrderMessageDecode();
        }
        public void ParseOrder(ClientSession session,byte[] buffer)
        {
            switch (Decode.GetMessageHead(buffer))
            {
                //客户端登录
                case OrderMessageType.ClientLogin:
                    session.Uuid = Decode.ClientLogin(buffer).uuid;
                    break;
                //心跳
                case OrderMessageType.ClientHeart:
                    break;
                default:
                    session.Close();
                    break;
            }
        }
    }
}
