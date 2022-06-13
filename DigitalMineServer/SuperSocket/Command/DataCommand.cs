using DigitalMineServer.implement;
using DigitalMineServer.ParseMessage;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket.SocketSession;
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
    public class DataCommand : CommandBase<DataSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(DataSession session, BinaryRequestInfo requestInfo)
        {
            new DataMessage().ParseOrder(session, requestInfo.Body);
        }
    }
}
