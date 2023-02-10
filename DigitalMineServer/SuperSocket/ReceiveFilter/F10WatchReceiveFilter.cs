using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket.ReceiveFilter
{
    internal class F10WatchReceiveFilter : BeginEndMarkReceiveFilter<BinaryRequestInfo>
    {
        private static readonly byte[] BeginMark = new byte[1] { (byte)'[' };
        private static readonly byte[] EndMark = new byte[1] { (byte)']' };

        public F10WatchReceiveFilter() : base(BeginMark, EndMark)
        {
        }

        protected override BinaryRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
            return new BinaryRequestInfo("F10WatchCommand", readBuffer);
        }
    }
}