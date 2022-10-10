using DigitalMineServer.Utils;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket.ReceiveFilter
{
    class ClientReceiveFilter : BeginEndMarkReceiveFilter<BinaryRequestInfo>
    {
        private readonly static byte[] Mark = new byte[] { (byte)'$' };
        public ClientReceiveFilter() : base(Mark, Mark)  {  }
        protected override BinaryRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {      
            return new BinaryRequestInfo("ClientCommand", readBuffer);
        }
    }
}
