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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.button2 = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.previous = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.mintor = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.DataText = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.infoBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.infoBox.Location = new System.Drawing.Point(1, 544);
            this.infoBox.Multiline = true;
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(1443, 197);
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
            this.Message.Location = new System.Drawing.Point(96, 39);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(11, 12);
            this.Message.TabIndex = 4;
            this.Message.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "消息积压：";
            // 
            // UserClient
            // 
            this.UserClient.AutoSize = true;
            this.UserClient.Location = new System.Drawing.Point(204, 11);
            this.UserClient.Name = "UserClient";
            this.UserClient.Size = new System.Drawing.Size(11, 12);
            this.UserClient.TabIndex = 6;
            this.UserClient.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "客户端：";
            // 
            // ClentVideo
            // 
            this.ClentVideo.AutoSize = true;
            this.ClentVideo.Location = new System.Drawing.Point(348, 10);
            this.ClentVideo.Name = "ClentVideo";
            this.ClentVideo.Size = new System.Drawing.Size(11, 12);
            this.ClentVideo.TabIndex = 8;
            this.ClentVideo.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "客户端视频：";
            // 
            // input
            // 
            this.input.Location = new System.Drawing.Point(1, 744);
            this.input.Multiline = true;
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(1443, 63);
            this.input.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.Location = new System.Drawing.Point(1, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1443, 482);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.White;
            this.start.Location = new System.Drawing.Point(1010, 6);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(206, 48);
            this.start.TabIndex = 11;
            this.start.Text = "启动";
            this.start.UseVisualStyleBackColor = false;
            this.start.Click += new System.EventHandler(this.Start_Click);
            // 
            // ClentAudio
            // 
            this.ClentAudio.AutoSize = true;
            this.ClentAudio.Location = new System.Drawing.Point(492, 10);
            this.ClentAudio.Name = "ClentAudio";
            this.ClentAudio.Size = new System.Drawing.Size(11, 12);
            this.ClentAudio.TabIndex = 13;
            this.ClentAudio.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(396, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "客户端音频：";
            // 
            // vehicleVideo
            // 
            this.vehicleVideo.AutoSize = true;
            this.vehicleVideo.Location = new System.Drawing.Point(347, 40);
            this.vehicleVideo.Name = "vehicleVideo";
            this.vehicleVideo.Size = new System.Drawing.Size(11, 12);
            this.vehicleVideo.TabIndex = 15;
            this.vehicleVideo.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(253, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "车载视频：";
            // 
            // vehicleAudio
            // 
            this.vehicleAudio.AutoSize = true;
            this.vehicleAudio.Location = new System.Drawing.Point(491, 40);
            this.vehicleAudio.Name = "vehicleAudio";
            this.vehicleAudio.Size = new System.Drawing.Size(11, 12);
            this.vehicleAudio.TabIndex = 17;
            this.vehicleAudio.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(402, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "车载音频：";
            // 
            // ClientHistoryAudio
            // 
            this.ClientHistoryAudio.AutoSize = true;
            this.ClientHistoryAudio.Location = new System.Drawing.Point(825, 12);
            this.ClientHistoryAudio.Name = "ClientHistoryAudio";
            this.ClientHistoryAudio.Size = new System.Drawing.Size(11, 12);
            this.ClientHistoryAudio.TabIndex = 21;
            this.ClientHistoryAudio.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(703, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "客户端录像音频：";
            // 
            // ClientHistoryVideo
            // 
            this.ClientHistoryVideo.AutoSize = true;
            this.ClientHistoryVideo.Location = new System.Drawing.Point(646, 10);
            this.ClientHistoryVideo.Name = "ClientHistoryVideo";
            this.ClientHistoryVideo.Size = new System.Drawing.Size(11, 12);
            this.ClientHistoryVideo.TabIndex = 19;
            this.ClientHistoryVideo.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(531, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "客户端录像视频：";
            // 
            // vehicleHistoryAudio
            // 
            this.vehicleHistoryAudio.AutoSize = true;
            this.vehicleHistoryAudio.Location = new System.Drawing.Point(825, 40);
            this.vehicleHistoryAudio.Name = "vehicleHistoryAudio";
            this.vehicleHistoryAudio.Size = new System.Drawing.Size(11, 12);
            this.vehicleHistoryAudio.TabIndex = 25;
            this.vehicleHistoryAudio.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(710, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "车载录像音频：";
            // 
            // vehicleHistoryVideo
            // 
            this.vehicleHistoryVideo.AutoSize = true;
            this.vehicleHistoryVideo.Location = new System.Drawing.Point(646, 40);
            this.vehicleHistoryVideo.Name = "vehicleHistoryVideo";
            this.vehicleHistoryVideo.Size = new System.Drawing.Size(11, 12);
            this.vehicleHistoryVideo.TabIndex = 23;
            this.vehicleHistoryVideo.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(537, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "车载录像视频：";
            // 
            // webSocket
            // 
            this.webSocket.AutoSize = true;
            this.webSocket.Location = new System.Drawing.Point(204, 39);
            this.webSocket.Name = "webSocket";
            this.webSocket.Size = new System.Drawing.Size(11, 12);
            this.webSocket.TabIndex = 27;
            this.webSocket.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(128, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "浏览器：";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1336, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 22);
            this.button2.TabIndex = 29;
            this.button2.Text = "刷新";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // next
            // 
            this.next.BackColor = System.Drawing.Color.White;
            this.next.Location = new System.Drawing.Point(1222, 32);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(109, 22);
            this.next.TabIndex = 30;
            this.next.Text = "下一页";
            this.next.UseVisualStyleBackColor = false;
            this.next.MouseClick += new System.Windows.Forms.MouseEventHandler(this.next_MouseClick);
            // 
            // previous
            // 
            this.previous.BackColor = System.Drawing.Color.White;
            this.previous.Location = new System.Drawing.Point(1222, 6);
            this.previous.Name = "previous";
            this.previous.Size = new System.Drawing.Size(109, 22);
            this.previous.TabIndex = 31;
            this.previous.Text = "上一页";
            this.previous.UseVisualStyleBackColor = false;
            this.previous.MouseClick += new System.Windows.Forms.MouseEventHandler(this.previous_MouseClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1337, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(107, 20);
            this.comboBox1.TabIndex = 32;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // mintor
            // 
            this.mintor.AutoSize = true;
            this.mintor.Location = new System.Drawing.Point(969, 39);
            this.mintor.Name = "mintor";
            this.mintor.Size = new System.Drawing.Size(11, 12);
            this.mintor.TabIndex = 36;
            this.mintor.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(878, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 35;
            this.label14.Text = "监控通道：";
            // 
            // DataText
            // 
            this.DataText.AutoSize = true;
            this.DataText.Location = new System.Drawing.Point(969, 15);
            this.DataText.Name = "DataText";
            this.DataText.Size = new System.Drawing.Size(11, 12);
            this.DataText.TabIndex = 34;
            this.DataText.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(878, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 33;
            this.label16.Text = "Data监控：";
            // 
            // JtServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1447, 808);
            this.Controls.Add(this.mintor);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.DataText);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.previous);
            this.Controls.Add(this.next);
            this.Controls.Add(this.button2);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button previous;
        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Label mintor;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label DataText;
        private System.Windows.Forms.Label label16;
    }
}

