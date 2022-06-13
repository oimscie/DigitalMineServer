using DigitalMineServer.implement;
using DigitalMineServer.PacketReponse;
using DigitalMineServer.ParseMessage;
using DigitalMineServer.Static;
using JtLibrary;
using JtLibrary.PacketBody;
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
    public class VehicleHistoryVideoCommand : CommandBase<VehicleHistoryVideoSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(VehicleHistoryVideoSession session, BinaryRequestInfo requestInfo)
        {
            new VehicleHistoryVideoMessage().Parse(session, requestInfo.Body );
        }
    }
}
