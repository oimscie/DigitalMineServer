using DigitalMineServer.implement;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket
{
    public class FileSession : AppSession<FileSession, BinaryRequestInfo>
    {
        public bool HasHeader { get; set; }
        public string FileName { get; set; }
        public string md5Name { get; set; }
        public string VritualPath { get; set; }
        public string FileType { get; set; }
        public string Company { get; set; }
        public string FilePath { get; set; }
        public int TotalSize { get; set; }
        public int ReceSize { get; set; }
        public FileStream fs { get; set; }

        public List<byte[]> FileByteList;
        public override void Send(string message)
        { 
            base.Send(message);
        }
        public override void Send(byte[] data, int offset, int length)
        {
            base.Send(data,offset,length);
        }
        protected override void OnSessionStarted()
        {
            HasHeader = false;
            FileByteList = new List<byte[]>();
            base.OnSessionStarted();
        }
        protected override void OnInit()
        {
            base.OnInit();
        }
        protected override void HandleUnknownRequest(BinaryRequestInfo requestInfo)
        {
            implement.Util.AppendText(JtServerForm.JtForm.infoBox, "未知请求");
            base.HandleUnknownRequest(requestInfo);
        }
    }
}
