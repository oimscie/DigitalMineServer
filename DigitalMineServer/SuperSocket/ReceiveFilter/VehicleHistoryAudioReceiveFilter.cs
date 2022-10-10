using DigitalMineServer.Utils;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket.ReceiveFilter
{
    class VehicleHistoryAudioReceiveFilter : FixedSizeReceiveFilter<BinaryRequestInfo>
    {

        public VehicleHistoryAudioReceiveFilter()
         : base(700)
        {

        }
        protected override BinaryRequestInfo ProcessMatchedRequest(byte[] buffer, int offset, int length, bool toBeCopied)
        {
            return new BinaryRequestInfo("VehicleHistoryAudioCommand", buffer.CloneRange(offset, length));
        }
    }
}
