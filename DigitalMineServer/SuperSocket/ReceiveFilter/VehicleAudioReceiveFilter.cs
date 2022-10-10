using DigitalMineServer.Utils;
using JtLibrary;
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
    class VehicleAudioReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {

        public VehicleAudioReceiveFilter()
          : base(26)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return header.ToUInt16(offset += 24);
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            return new BinaryRequestInfo("VehicleAudioCommand", header.Array.Concat(bodyBuffer.CloneRange(offset, length)).ToArray());
        }

    }


}
