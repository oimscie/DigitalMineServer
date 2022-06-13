﻿using DigitalMineServer.implement;
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
    public class ClientAudioReceiveFilter : TerminatorReceiveFilter<BinaryRequestInfo>
    {
        public ClientAudioReceiveFilter() : base(new byte[] {11,22,33,44}) { }
        protected override BinaryRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
            return new BinaryRequestInfo("ClientAudioCommand", readBuffer.CloneRange(offset, length));
        }

    }
}
