using DigitalMineServer.Utils;
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
    public class F10WatchCommand : CommandBase<F10WatchSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(F10WatchSession session, BinaryRequestInfo requestInfo)
        {
            Resource.OriginalWatchDataQueues.Enqueue(new ValueTuple<byte[], F10WatchSession>
            {
                Item1 = requestInfo.Body,
                Item2 = session
            });
        }
    }
}