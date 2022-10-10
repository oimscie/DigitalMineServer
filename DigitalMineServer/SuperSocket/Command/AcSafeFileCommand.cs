using DigitalMineServer.Utils;
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
    public class AcSafeFileCommand : CommandBase<AcSafeFileSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(AcSafeFileSession session, BinaryRequestInfo requestInfo)
        {
            new AcSafeFileMessage().FilterMessage(session, requestInfo.Body);
        }
    }
}