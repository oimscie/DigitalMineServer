using DigitalMineServer.implement;
using DigitalMineServer.OrderMessage;
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
        private readonly OrderMessageDecode Decode;
        public DataMessage()
        {
            Decode = new OrderMessageDecode();
        }
        public void ParseOrder(DataSession Session, byte[] buffer)
        {
            switch (Decode.GetMessageHead(buffer))
            {
                //心跳
                case OrderMessageType.LocalHeart:
                    break;
                //本地数据终端上报所属公司
                case OrderMessageType.LocalLogin:
                    Session.Company = Decode.LocalLogin(buffer).Company;
                    break;
                default:
                    Session.Close();
                    break;
            }
        }
    }
}
