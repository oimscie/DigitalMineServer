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
    internal class VehicleVideoReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {
        public VehicleVideoReceiveFilter()
          : base(30)
        {
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            if (BitConvert.ByteToBit(header[offset + 15]).Substring(0, 4) == "0011")
            {
                return header.ToUInt16(offset += 24) - 4;
            }
            return header.ToUInt16(offset += 28);
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            return new BinaryRequestInfo("VehicleVideoCommand", header.Array.Concat(bodyBuffer.CloneRange(offset, length)).ToArray());
        }
    }
}