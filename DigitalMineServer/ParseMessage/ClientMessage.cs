using DigitalMineServer.implement;
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
        public void ParseOrder(ClientSession session,byte[] buffer)
        {
            string[] orderItem = Encoding.UTF8.GetString(buffer).Trim('$').Split('!');
            switch (orderItem[0])
            {
                //客户所登录
                case "login":
                    session.Uuid = orderItem[1];
                    break;
                //心跳
                case "heart":
                    break;
                default:
                    session.Close();
                    break;
            }
        }
    }
}
