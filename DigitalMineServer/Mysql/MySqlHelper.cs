using DigitalMineServer.Utils;
using DigitalMineServer.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DigitalMineServer.Mysql
{
    internal class MySqlHelper
    {
        /// <summary>
        /// 数据库连接头
        /// </summary>
        private MySqlConnection Conn;

        /// <summary>
        /// 数据库连接句柄
        /// </summary>
        private MySqlCommand Command;

        /// <summary>
        /// 读取头
        /// </summary>
        private MySqlDataReader Reader;

        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string ConnStr = "server=" + ConfigurationManager.AppSettings["ServerIp"] + ";database=product;user=server;password=$qwertyuiop$.;charset=utf8;SslMode=None";

        public MySqlHelper()
        {
            Conn = new MySqlConnection(ConnStr);
            Command = new MySqlCommand
            {
                Connection = Conn
            };
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            try
            {
                Conn.Close();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("mysql关闭错误", e);
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                Conn.Open();
                if (Conn.State == ConnectionState.Open)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Utils.Util.AppendText(JtServerForm.JtForm.infoBox, e.Message);
                LogHelper.WriteLog("数据库打开错误", e);
                return false;
            }
        }

        //检查连接头并打开，打开返回true，关闭未false
        public bool CheckConn()
        {
            if (Conn.State == ConnectionState.Closed)
            {
                return Open();
            }
            if (Conn.State == ConnectionState.Broken)
            {
                Close();
                return Open();
            }
            if (Conn.State == ConnectionState.Connecting)
            {
                return false;
            }
            if (Conn.State == ConnectionState.Executing || Conn.State == ConnectionState.Fetching)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 多行查询，查询失败或结果为空均返回null
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="fieldName">要查询的数据字段名称集合</param>
        /// <returns>List<Dictionary<string, string>>，以输入的字段名称为key值</returns>
        public List<Dictionary<string, string>> MultipleSelect_List_dic(string sql, List<string> fieldName)
        {
            if (!CheckConn()) { return null; }
            Command.CommandText = sql;
            Reader = Command.ExecuteReader();
            List<Dictionary<string, string>> back = new List<Dictionary<string, string>>();
            int index = 0;
            try
            {
                while (Reader.Read())
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    for (int i = 0; i < Reader.FieldCount; i++)
                    {
                        dic.Add(fieldName[index], Reader.GetString(fieldName[index]));
                        index++;
                    }
                    index = 0;
                    back.Add(dic);
                }
                Reader.Close();
                if (back.Count == 0) { return null; };
                return back;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("sql查询错误----" + sql, e);
                return null;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// datagridView数据源更新，查询失败或结果为空均返回null
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="fieldName">List<vehicleStateEntity>datagridView数据源</param>
        /// <returns>List<vehicleStateEntity></returns>
        public List<VehicleStateEntity> MultipleSelect_List_dic_v(string sql)
        {
            if (!CheckConn()) { return null; }
            Command.CommandText = sql;
            Reader = Command.ExecuteReader();
            List<VehicleStateEntity> back = new List<VehicleStateEntity>();
            try
            {
                while (Reader.Read())
                {
                    back.Add(new VehicleStateEntity()
                    {
                        VEHICLE_ID = Reader.GetString("VEHICLE_ID"),
                        VEHICLE_SIM = Reader.GetString("VEHICLE_SIM"),
                        VEHICLE_TYPE = Reader.GetString("VEHICLE_TYPE"),
                        VEHICLE_DRIVER = Reader.GetString("VEHICLE_DRIVER"),
                        POSI_STATE = Reader.GetString("POSI_STATE"),
                        POSI_X = Reader.GetString("POSI_X"),
                        POSI_Y = Reader.GetString("POSI_Y"),
                        POSI_SPEED = Reader.GetString("POSI_SPEED"),
                        REAl_FUEL = Reader.GetString("REAl_FUEL"),
                        ACC = Reader.GetString("ACC"),
                        POSI_NUM = Reader.GetString("POSI_NUM"),
                        COMPANY = Reader.GetString("COMPANY"),
                        ADD_TIME = Reader.GetString("ADD_TIME"),
                    });
                }
                Reader.Close();
                if (back.Count == 0) { return null; };
                return back;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("数据源查询错误----" + sql, e);
                return null;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// datagridView数据源更新，查询失败或结果为空均返回null
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="fieldName">List<vehicleStateEntity>datagridView数据源</param>
        /// <returns>List<vehicleStateEntity></returns>
        public List<PersonStateEntity> MultipleSelect_List_dic_p(string sql)
        {
            if (!CheckConn()) { return null; }
            Command.CommandText = sql;
            Reader = Command.ExecuteReader();
            List<PersonStateEntity> back = new List<PersonStateEntity>();
            try
            {
                while (Reader.Read())
                {
                    back.Add(new PersonStateEntity()
                    {
                        PERSON_ID = Reader.GetString("PERSON_ID"),
                        PERSON_SIM = Reader.GetString("PERSON_SIM"),
                        PERSON_TYPE = Reader.GetString("PERSON_TYPE"),
                        POSI_STATE = Reader.GetString("POSI_STATE"),
                        POSI_X = Reader.GetString("POSI_X"),
                        POSI_Y = Reader.GetString("POSI_Y"),
                        ACC = Reader.GetString("ACC"),
                        BATTERY = Reader.GetString("BATTERY"),
                        STEP = Reader.GetString("STEP"),
                        STATE = Reader.GetString("STATE"),
                        HEARTRATE = Reader.GetString("HEARTRATE"),
                        BLPRES = Reader.GetString("BLPRES"),
                        POSI_NUM = Reader.GetString("POSI_NUM"),
                        COMPANY = Reader.GetString("COMPANY"),
                        ADD_TIME = Reader.GetString("ADD_TIME"),
                    });
                }
                Reader.Close();
                if (back.Count == 0) { return null; };
                return back;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("数据源查询错误----" + sql, e);
                return null;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 单字段多行查询，查询失败或结果为空均返回null
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="fieldName">要查询的数据字段名称</param>
        /// <returns>ArrayList</returns>
        public ArrayList MultipleSelect_Arr(string sql, string fieldName)
        {
            if (!CheckConn()) { return null; }
            Command.CommandText = sql;
            Reader = Command.ExecuteReader();
            ArrayList back = new ArrayList();
            try
            {
                while (Reader.Read())
                {
                    back.Add(Reader.GetString(fieldName));
                }
                Reader.Close();
                if (back.Count == 0) { return null; };
                return back;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("sql查询错误----" + sql, e);
                return null;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 数据源查询（单列，参数fieldName）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public DataTable MultipleSelect_DataTable(string sql, string fieldName)
        {
            if (!CheckConn()) { return null; }
            Command.CommandText = sql;
            Reader = Command.ExecuteReader();
            DataTable back = new DataTable();
            back.Columns.Add(new DataColumn(fieldName));
            try
            {
                while (Reader.Read())
                {
                    DataRow row = back.NewRow();
                    row[fieldName] = Reader.GetString(fieldName);
                    back.Rows.Add(row);
                }
                Reader.Close();
                if (back.Rows.Count == 0) { return null; };
                return back;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("sql查询错误----" + sql, e);
                return null;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 单字段多行查询，查询失败或结果为空均返回null
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="fieldName">要查询的数据字段名称</param>
        /// <returns>Dictionary<string, string>，以输入的字段名称为key值</returns>
        public Dictionary<string, string> SingleSelect_Dic(string sql, string fieldName)
        {
            if (!CheckConn()) { return null; }
            Command.CommandText = sql;
            Reader = Command.ExecuteReader();
            Dictionary<string, string> back = new Dictionary<string, string>();
            try
            {
                while (Reader.Read())
                {
                    back.Add(fieldName, Reader.GetString(fieldName));
                    break;
                }
                Reader.Close();
                if (back.Count == 0) { return null; };
                return back;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("sql查询错误----" + sql, e);
                return null;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 单字段多行查询，查询失败或结果为空均返回null
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="fieldName">要查询的数据字段名称</param>
        /// <returns>Dictionary<string, string>，以输入的字段名称为key值</returns>
        public string SingleSelect_Str(string sql, string fieldName)
        {
            if (!CheckConn()) { return null; }
            Command.CommandText = sql;
            Reader = Command.ExecuteReader();
            try
            {
                if (Reader.HasRows)
                {
                    return Reader.GetString(fieldName);
                }
                Reader.Close();
                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("sql查询错误----" + sql, e);
                return null;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 插入、修改、删除
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int UpdOrInsOrdel(string sql)
        {
            try
            {
                if (!CheckConn()) { return 0; }
                Command.CommandText = sql;
                return Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("sql查询错误----" + sql, e);
                return 0;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 查找目标是否存在，必须是select count（ID） as Count.....
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetCount(string sql)
        {
            try
            {
                int back = 0;
                if (!CheckConn()) { return -1; }
                Command.CommandText = sql;
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    back = Reader.GetInt32("Count");
                }
                Reader.Close();
                return back;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("sql查询错误----" + sql, e);
                return 0;
            }
            finally
            {
                Close();
            }
        }
    }
}