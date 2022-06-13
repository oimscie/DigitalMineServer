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
    public class VehicleHistoryAudioCommand : CommandBase<VehicleHistoryAudioSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(VehicleHistoryAudioSession session, BinaryRequestInfo requestInfo)
        {
           new VehicleHistoryAudioMessage().Parse( session, requestInfo.Body );
        }
    }
}
