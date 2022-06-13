using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.SuperSocket;
using DigitalMineServer.SuperSocket.SocketServer;
using DigitalMineServer.SuperSocket.SocketSession;
using SuperSocket.SocketBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalMineServer.ParseMessage
{
    //用户端数据处理软件消息
    class DataMessage
    {
        public void ParseOrder(DataSession Session,byte[] buffer)
        {
            string[] info = Encoding.UTF8.GetString(buffer).Split('!');
            switch (info[0]) {
                //心跳
                case "Heart":
                    break;
                //登录
                case "Company":
                    Session.Company = Encoding.UTF8.GetString(buffer).Split('!')[1];
                    break;
                default:
                    Session.Close();
                    break;
            }
        }
    }
}
