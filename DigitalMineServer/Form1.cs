using DigitalMineServer.Static;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Threading;
using System.Windows.Forms;
namespace DigitalMineServer
{
    public partial class JtServerForm : Form
    {
        public static JtServerForm JtForm;
        public static IBootstrap bootstrap;

        public JtServerForm()
        {
            JtForm = this;
            InitializeComponent();
        }
        private void JtServerForm_Load(object sender, EventArgs e)
        {
            //初始化存储集合
            _ = new Resource();
            this.infoBox.AppendText("静态资源初始化完成\r\n");
        }

        private void Start_Click(object sender, EventArgs e)
        {

            this.infoBox.AppendText("引导启动初始化\r\n");
            #region 初始化Socket  
            bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                this.infoBox.AppendText("引导启动初始化失败\r\n");
                return;
            }
            this.start.Enabled = false;
            bootstrap.Start();
            foreach (var server in bootstrap.AppServers)
            {
                if (server.State == ServerState.Running)
                {
                    this.infoBox.AppendText(server.Name + "启动完成\r\n");
                }
                else
                {
                    this.infoBox.AppendText(server.Name + "启动失败\r\n");
                }
            }
            #endregion

            #region 初始化解析

            Thread parsr = new Thread(new Jt808Message().ParseMessages)
            {
                IsBackground = true
            };
            parsr.Start();
            #endregion
        }
    }
}
