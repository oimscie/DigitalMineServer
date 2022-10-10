using DigitalMineServer.Utils;
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
    public class ClientAudioCommand : CommandBase<ClientAudioSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(ClientAudioSession session, BinaryRequestInfo requestInfo)
        {
           new ClientAudioMessage().ParseOrder( session, requestInfo.Body );
        }
    }
}
