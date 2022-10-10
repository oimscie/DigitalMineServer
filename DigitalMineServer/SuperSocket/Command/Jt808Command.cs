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
    public class Jt808Command : CommandBase<Jt808Session, BinaryRequestInfo>
    {
        public override void ExecuteCommand(Jt808Session session, BinaryRequestInfo requestInfo)
        {
            Resource.OriginalDataQueues.Enqueue(new ValueTuple<byte[], Jt808Session>
            {
                Item1 = requestInfo.Body,
                Item2 = session
            });
        }
    }
}