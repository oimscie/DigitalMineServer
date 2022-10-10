using DigitalMineServer.Utils;
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
    public class MonitorFileSession : AppSession<MonitorFileSession, BinaryRequestInfo>
    {
        /// <summary>
        /// 是否存在文件信息头
        /// </summary>
        public bool HasHeader { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件MD5名称
        /// </summary>
        public string md5Name { get; set; }

        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string VritualPath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 归属公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 真实路径
        /// </summary>
        public string RealFilePath { get; set; }

        /// <summary>
        /// 文件体积
        /// </summary>
        public int TotalSize { get; set; }

        /// <summary>
        /// 已接收文件体积
        /// </summary>
        public int ReceSize { get; set; }

        /// <summary>
        /// 文件流头
        /// </summary>
        public FileStream fs { get; set; }

        /// <summary>
        /// 文件数据流暂存列表
        /// </summary>
        public List<byte[]> FileByteList;

        public override void Send(string message)
        {
            base.Send(message);
        }

        public override void Send(byte[] data, int offset, int length)
        {
            base.Send(data, offset, length);
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
            Utils.Util.AppendText(JtServerForm.JtForm.infoBox, "未知请求");
            base.HandleUnknownRequest(requestInfo);
        }
    }
}