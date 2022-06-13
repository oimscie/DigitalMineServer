using DigitalMineServer.implement;
using DigitalMineServer.ParseMessage;
using DigitalMineServer.Static;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalMineServer.SuperSocket
{
    public class ClientHistoryVideoCommand : CommandBase<ClientHistoryVideoSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(ClientHistoryVideoSession session, BinaryRequestInfo requestInfo)
        {
            new ClientHistoryVideoMessage().ParseOrder(session, requestInfo.Body);
        }
    }
}
