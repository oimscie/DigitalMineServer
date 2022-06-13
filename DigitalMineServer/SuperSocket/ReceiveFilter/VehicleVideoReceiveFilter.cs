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

        class VehicleVideoReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
        {

            public VehicleVideoReceiveFilter()
              : base(30)
            {

            }

            protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
            {
                return header.ToUInt16(offset += 28);
            }

            protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
            {
                return new BinaryRequestInfo("VehicleVideoCommand", header.Array.Concat(bodyBuffer.CloneRange(offset, length)).ToArray());
            }
        
    }
}
