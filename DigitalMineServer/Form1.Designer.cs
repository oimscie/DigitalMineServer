namespace DigitalMineServer
{
    partial class JtServerForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.infoBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.vehicleOnline = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UserClient = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ClentVideo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.input = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.start = new System.Windows.Forms.Button();
            this.ClentAudio = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.vehicleVideo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.vehicleAudio = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ClientHistoryAudio = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ClientHistoryVideo = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.vehicleHistoryAudio = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.vehicleHistoryVideo = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.webSocket = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.infoBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.infoBox.Location = new System.Drawing.Point(1, 59);
            this.infoBox.Multiline = true;
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(352, 634);
            this.infoBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "在线设备：";
            // 
            // vehicleOnline
            // 
            this.vehicleOnline.AutoSize = true;
            this.vehicleOnline.Location = new System.Drawing.Point(97, 11);
            this.vehicleOnline.Name = "vehicleOnline";
            this.vehicleOnline.Size = new System.Drawing.Size(11, 12);
            this.vehicleOnline.TabIndex = 2;
            this.vehicleOnline.Text = "0";
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Location = new System.Drawing.Point(216, 11);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(11, 12);
            this.Message.TabIndex = 4;
            this.Message.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "消息积压：";
            // 
            // UserClient
            // 
            this.UserClient.AutoSize = true;
            this.UserClient.Location = new System.Drawing.Point(357, 10);
            this.UserClient.Name = "UserClient";
            this.UserClient.Size = new System.Drawing.Size(11, 12);
            this.UserClient.TabIndex = 6;
            this.UserClient.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "客户端：";
            // 
            // ClentVideo
            // 
            this.ClentVideo.AutoSize = true;
            this.ClentVideo.Location = new System.Drawing.Point(524, 9);
            this.ClentVideo.Name = "ClentVideo";
            this.ClentVideo.Size = new System.Drawing.Size(11, 12);
            this.ClentVideo.TabIndex = 8;
            this.ClentVideo.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "客户端视频：";
            // 
            // input
            // 
            this.input.Location = new System.Drawing.Point(1, 699);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(352, 108);
            this.input.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(359, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(915, 748);
            this.dataGridView1.TabIndex = 10;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(1132, 4);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(142, 49);
            this.start.TabIndex = 11;
            this.start.Text = "启动";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.Start_Click);
            // 
            // ClentAudio
            // 
            this.ClentAudio.AutoSize = true;
            this.ClentAudio.Location = new System.Drawing.Point(696, 9);
            this.ClentAudio.Name = "ClentAudio";
            this.ClentAudio.Size = new System.Drawing.Size(11, 12);
            this.ClentAudio.TabIndex = 13;
            this.ClentAudio.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(582, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "客户端音频：";
            // 
            // vehicleVideo
            // 
            this.vehicleVideo.AutoSize = true;
            this.vehicleVideo.Location = new System.Drawing.Point(523, 39);
            this.vehicleVideo.Name = "vehicleVideo";
            this.vehicleVideo.Size = new System.Drawing.Size(11, 12);
            this.vehicleVideo.TabIndex = 15;
            this.vehicleVideo.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(413, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "车载视频：";
            // 
            // vehicleAudio
            // 
            this.vehicleAudio.AutoSize = true;
            this.vehicleAudio.Location = new System.Drawing.Point(695, 39);
            this.vehicleAudio.Name = "vehicleAudio";
            this.vehicleAudio.Size = new System.Drawing.Size(11, 12);
            this.vehicleAudio.TabIndex = 17;
            this.vehicleAudio.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(588, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "车载音频：";
            // 
            // ClientHistoryAudio
            // 
            this.ClientHistoryAudio.AutoSize = true;
            this.ClientHistoryAudio.Location = new System.Drawing.Point(1078, 11);
            this.ClientHistoryAudio.Name = "ClientHistoryAudio";
            this.ClientHistoryAudio.Size = new System.Drawing.Size(11, 12);
            this.ClientHistoryAudio.TabIndex = 21;
            this.ClientHistoryAudio.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(949, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "客户端录像音频：";
            // 
            // ClientHistoryVideo
            // 
            this.ClientHistoryVideo.AutoSize = true;
            this.ClientHistoryVideo.Location = new System.Drawing.Point(893, 9);
            this.ClientHistoryVideo.Name = "ClientHistoryVideo";
            this.ClientHistoryVideo.Size = new System.Drawing.Size(11, 12);
            this.ClientHistoryVideo.TabIndex = 19;
            this.ClientHistoryVideo.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(751, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "客户端录像视频：";
            // 
            // vehicleHistoryAudio
            // 
            this.vehicleHistoryAudio.AutoSize = true;
            this.vehicleHistoryAudio.Location = new System.Drawing.Point(1078, 40);
            this.vehicleHistoryAudio.Name = "vehicleHistoryAudio";
            this.vehicleHistoryAudio.Size = new System.Drawing.Size(11, 12);
            this.vehicleHistoryAudio.TabIndex = 25;
            this.vehicleHistoryAudio.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(956, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "车载录像音频：";
            // 
            // vehicleHistoryVideo
            // 
            this.vehicleHistoryVideo.AutoSize = true;
            this.vehicleHistoryVideo.Location = new System.Drawing.Point(893, 39);
            this.vehicleHistoryVideo.Name = "vehicleHistoryVideo";
            this.vehicleHistoryVideo.Size = new System.Drawing.Size(11, 12);
            this.vehicleHistoryVideo.TabIndex = 23;
            this.vehicleHistoryVideo.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(757, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "车载录像视频：";
            // 
            // webSocket
            // 
            this.webSocket.AutoSize = true;
            this.webSocket.Location = new System.Drawing.Point(357, 38);
            this.webSocket.Name = "webSocket";
            this.webSocket.Size = new System.Drawing.Size(11, 12);
            this.webSocket.TabIndex = 27;
            this.webSocket.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(268, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "浏览器：";
            // 
            // JtServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 808);
            this.Controls.Add(this.webSocket);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.vehicleHistoryAudio);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.vehicleHistoryVideo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ClientHistoryAudio);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ClientHistoryVideo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.vehicleAudio);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.vehicleVideo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ClentAudio);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.start);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.input);
            this.Controls.Add(this.ClentVideo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UserClient);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vehicleOnline);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.infoBox);
            this.Name = "JtServerForm";
            this.Text = "JtServer";
            this.Load += new System.EventHandler(this.JtServerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox infoBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label vehicleOnline;
        public System.Windows.Forms.Label Message;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label UserClient;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label ClentVideo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Button start;
        public System.Windows.Forms.Label ClentAudio;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label vehicleVideo;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label vehicleAudio;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label ClientHistoryAudio;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label ClientHistoryVideo;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label vehicleHistoryAudio;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label vehicleHistoryVideo;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.Label webSocket;
        private System.Windows.Forms.Label label12;
    }
}

