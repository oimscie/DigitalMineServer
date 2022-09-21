﻿using DigitalMineServer.implement;
using DigitalMineServer.ParseMessage;
using DigitalMineServer.Static;
using DigitalMineServer.SuperSocket.SocketSession;
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
    public class FileCommand : CommandBase<MonitorFileSession, BinaryRequestInfo>
    {
        public override void ExecuteCommand(MonitorFileSession session, BinaryRequestInfo requestInfo)
        {
            new FileMessage().ParseOrder(session, requestInfo.Body);
        }
    }
}
