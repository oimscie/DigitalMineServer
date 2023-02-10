using DigitalMineServer.Utils;
using DigitalMineServer.Mysql;
using DigitalMineServer.Static;
using DigitalMineServer.Util;
using DigitalMineServer.InfoInit;
using JtLibrary;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using DigitalMineServer.Redis;
using DigitalMineServer.ParseMessage;
using CloseReason = System.Windows.Forms.CloseReason;

namespace DigitalMineServer
{
    public partial class JtServerForm : Form
    {
        public static JtServerForm JtForm;
        public static IBootstrap bootstrap = null;
        private Jt808Message Jt808Message;
        private MySqlHelper MySqlHelper;
        private F10WatchMessage F10WatchMessage;
        private List<VehicleStateEntity> VehicleStateEntity;
        private List<PersonStateEntity> PersonStateEntity;

        /// <summary>
        /// view每页行数
        /// </summary>
        private readonly int count = 20;

        /// <summary>
        /// 页数
        /// </summary>
        private int size = 0;

        /// <summary>
        /// 车辆信息更新计时器
        /// </summary>
        private System.Timers.Timer VehicleInfo;

        /// <summary>
        /// 人员信息更新计时器
        /// </summary>
        private System.Timers.Timer PersonInfo;

        /// <summary>
        /// 数据源更新计时器
        /// </summary>
        private System.Timers.Timer dataSouth;

        private string sqlCompany;
        private string sql;

        public JtServerForm()
        {
            JtForm = this;
            InitializeComponent();
        }

        private void JtServerForm_Load(object sender, EventArgs e)
        {
            //设置大小端
            BitConvert.islittleEndian = BitConvert.CheckSysIsBigEndian();
            //初始化存储集合
            _ = new Resource();
            Jt808Message = new Jt808Message();
            F10WatchMessage = new F10WatchMessage();
            MySqlHelper = new MySqlHelper();
            RedisHelper.Execute();
            this.infoBox.AppendText("静态资源初始化完成\r\n");
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.infoBox.AppendText("监听引导启动初始化\r\n");

            #region 初始化Socket

            bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                this.infoBox.AppendText("监听引导启动初始化失败\r\n");
                return;
            }
            this.start.Enabled = false;
            bootstrap.Start();
            foreach (var server in bootstrap.AppServers)
            {
                if (server.State == ServerState.Running)
                {
                    this.infoBox.AppendText(server.Name + "监听引导启动完成\r\n");
                }
                else
                {
                    this.infoBox.AppendText(server.Name + "监听引导启动失败\r\n");
                }
            }

            #endregion 初始化Socket

            #region 初始化车辆人员信息

            new Vehicle().VehicleInfo(null, null);

            new Person().PersonInfo(null, null);
            Thread VehicleInfo = new Thread(CarInfoCheckTimers)
            {
                IsBackground = true
            };
            VehicleInfo.Start();
            Thread PersonInfo = new Thread(PersonInfoCheckTimers)
            {
                IsBackground = true
            };
            PersonInfo.Start();
            this.infoBox.AppendText("车辆人员信息初始化完成\r\n");

            #endregion 初始化车辆人员信息

            #region 初始化解析服务

            Thread Jt808Parsr = new Thread(Jt808Message.ParseMessages)
            {
                IsBackground = true
            };
            Jt808Parsr.Start();
            this.infoBox.AppendText("解析服务初始化完成\r\n");

            Thread F10Parsr = new Thread(F10WatchMessage.ParseMessage)
            {
                IsBackground = true
            };
            F10Parsr.Start();

            #endregion 初始化解析服务

            #region 初始化存储服务

            Thread InDB = new Thread(Jt808Message.ParseVehicle0200)
            {
                IsBackground = true
            };
            InDB.Start();

            Thread InDB_P = new Thread(Jt808Message.ParsePerson0200)
            {
                IsBackground = true
            };
            InDB_P.Start();

            this.infoBox.AppendText("存储服务初始化完成\r\n");

            #endregion 初始化存储服务

            #region 初始化数据源

            getCompany();
            sqlCompany = "";
            UpdateState(null, null);
            //禁止列排序
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Thread dataSource = new Thread(dataSouthTimers)
            {
                IsBackground = true
            };
            dataSource.Start();

            #endregion 初始化数据源
        }

        /// <summary>
        ///  车辆信息更新
        /// </summary>
        public void CarInfoCheckTimers()
        {
            VehicleInfo = new System.Timers.Timer(1000 * 60 * 1);
            VehicleInfo.Elapsed += new ElapsedEventHandler(new Vehicle().VehicleInfo);
            VehicleInfo.AutoReset = true;
            VehicleInfo.Enabled = true;
        }

        /// <summary>
        ///  人员信息更新
        /// </summary>
        public void PersonInfoCheckTimers()
        {
            PersonInfo = new System.Timers.Timer(1000 * 60 * 1);
            PersonInfo.Elapsed += new ElapsedEventHandler(new Person().PersonInfo);
            PersonInfo.AutoReset = true;
            PersonInfo.Enabled = true;
        }

        /// <summary>
        ///  数据源更新
        /// </summary>
        public void dataSouthTimers()
        {
            dataSouth = new System.Timers.Timer(1000 * 30);
            dataSouth.Elapsed += new ElapsedEventHandler(UpdateState);
            dataSouth.AutoReset = true;
            dataSouth.Enabled = true;
        }

        private void UpdateState(object source, ElapsedEventArgs e)
        {
            switch (this.equipType.Text)
            {
                case "车辆":
                    sql = "select VEHICLE_ID ,VEHICLE_SIM ,VEHICLE_TYPE ,VEHICLE_DRIVER,POSI_STATE,POSI_X,POSI_Y,POSI_SPEED,REAl_FUEL,ACC,POSI_NUM,COMPANY,ADD_TIME from (select FID,POSI_STATE,POSI_X,POSI_Y,POSI_SPEED,REAl_FUEL,ACC,POSI_NUM,COMPANY,ADD_TIME from vehicle_state " + sqlCompany + " order by ADD_TIME desc limit " + size * count + "," + count + ")a inner join (select ID,VEHICLE_ID,VEHICLE_SIM,VEHICLE_TYPE,VEHICLE_DRIVER from LIST_VEHICLE " + sqlCompany + ")b on FID=ID";
                    VehicleStateEntity = MySqlHelper.MultipleSelect_v(sql);
                    Utils.Util.UpdataSource_v(dataGridView1, VehicleStateEntity);
                    break;

                case "人员":
                    sql = "select PERSON_ID ,PERSON_SIM ,PERSON_TYPE,POSI_STATE,POSI_X,POSI_Y,ACC,BATTERY,STEP,STATE,HEARTRATE,BLPRES,POSI_NUM,COMPANY,ADD_TIME from (select FID,POSI_STATE,POSI_X,POSI_Y,ACC,BATTERY,STEP,STATE,HEARTRATE,BLPRES,POSI_NUM,COMPANY,ADD_TIME from person_state " + sqlCompany + " order by ADD_TIME desc limit " + size * count + "," + count + ")a inner join (select ID,PERSON_ID,PERSON_SIM,PERSON_TYPE from LIST_PERSON " + sqlCompany + ")b on FID=ID";
                    PersonStateEntity = MySqlHelper.MultipleSelect_p(sql);
                    Utils.Util.UpdataSource_p(dataGridView1, PersonStateEntity);
                    break;
            }
        }

        /// <summary>
        /// 单元格内容居中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void previous_MouseClick(object sender, MouseEventArgs e)
        {
            if (size == 0)
            {
                MessageBox.Show("已到达第一页");
                return;
            }
            size--;
            UpdateState(null, null);
        }

        private void next_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count < 20)
            {
                MessageBox.Show("已到达最后一页");
                return;
            }
            size++;
            UpdateState(null, null);
        }

        private void getCompany()
        {
            string sql = "select distinct Company from list_user";
            ArrayList data = MySqlHelper.MultipleSelect(sql, "Company");
            comboBox1.DataSource = data;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = 0;
            sqlCompany = "where company='" + this.comboBox1.Text + "'";
            UpdateState(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            size = 0;
            sqlCompany = "";
            UpdateState(null, null);
        }

        private void equipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = 0;
            UpdateState(null, null);
        }

        private void JtServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("是否关闭?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    if (bootstrap != null)
                    {
                        bootstrap.Stop();
                    }
                    Resource.IsActive = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}