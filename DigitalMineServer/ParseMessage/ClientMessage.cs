using DigitalMineServer.implement;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DigitalMineServer.ParseMessage
{
    class ClientMessage
    {
        public void ParseOrder(ClientSession session,byte[] buffer)
        {
            string[] orderItem = Encoding.UTF8.GetString(buffer).Trim('$').Split('!');
            switch (orderItem[0])
            {
                case "login":
                    session.Uuid = orderItem[1];
                    break;
                case "heart":
                    break;
                default:
                    session.Close();
                    break;
            }
        }
    }
}
