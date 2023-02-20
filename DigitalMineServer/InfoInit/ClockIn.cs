using DigitalMineServer.Mysql;
using DigitalMineServer.Redis;
using DigitalMineServer.Static;
using DigitalMineServer.Util;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DigitalMineServer.Structures.Comprehensive;

namespace DigitalMineServer.InfoInit
{
    public class ClockIn
    {
        private readonly MySqlHelper mySqlHelper;

        private readonly RedisHelper redisHelper;

        public ClockIn()
        {
            mySqlHelper = new MySqlHelper();

            redisHelper = new RedisHelper();
        }

        public void ClockInInfo(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Resource.isClockInUpdate = true;
                //信息字段List(xy位置，归属公司)
                List<string> fileName = new List<string> { "XY", "COMPANY" };
                string sql = "select XY,COMPANY from list_clock_in_Fence";
                List<Dictionary<string, string>> result = mySqlHelper.MultipleSelect_List_dic(sql, fileName);
                //判断服务器返回的信息是否为空
                if (result == null)
                {
                    return;
                }
                //写入redis
                foreach (var item in result)
                {
                    redisHelper.Set(item.Last().Value + Redis_key_ext.clock_in, Utils.Util.ObjectSerializ(Utils.Util.SerializationPoint(item.First().Value)));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("人员信息更新错误", ex);
            }
            finally
            {
                Resource.isClockInUpdate = false;
            }
        }
    }
}