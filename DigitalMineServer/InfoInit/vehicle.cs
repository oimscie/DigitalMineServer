using DigitalMineServer.Utils;
using DigitalMineServer.Mysql;
using DigitalMineServer.Static;
using DigitalMineServer.Util;
using JtLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalMineServer.Redis;
using static ServiceStack.Script.Lisp;
using static DigitalMineServer.Structures.Comprehensive;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;

namespace DigitalMineServer.InfoInit
{
    public class Vehicle
    {
        private readonly MySqlHelper mySql;
        private readonly RedisHelper Redis;

        public Vehicle()
        {
            mySql = new MySqlHelper();
            Redis = new RedisHelper();
        }

        public void VehicleInfo(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Resource.isVehicleUpdate = true;
                //车辆信息字段List(数据库ID，终端Sim，车辆类型，车辆限速值，归属公司，车辆编号，驾驶员)
                List<string> fileName = new List<string> { "ID", "VEHICLE_SIM", "VEHICLE_TYPE", "VEHICLE_SPEED", "COMPANY", "VEHICLE_ID", "VEHICLE_DRIVER" };
                //更新车辆信息
                string sql = "select ID,VEHICLE_ID,VEHICLE_SIM,VEHICLE_TYPE,VEHICLE_SPEED,VEHICLE_DRIVER,COMPANY from list_vehicle";
                List<Dictionary<string, string>> result = mySql.MultipleSelect(sql, fileName);
                //临时信息存储，供围栏信息使用
                Dictionary<string, ValueTuple<string, string, string>> temp = new Dictionary<string, (string, string, string)>();
                //服务器车辆写入redis
                if (result == null)
                {
                    return;
                }

                foreach (var item in result)
                {
                    item.TryGetValue("VEHICLE_SIM", out string sim);
                    item.TryGetValue("ID", out string id);
                    item.TryGetValue("VEHICLE_TYPE", out string type);
                    item.TryGetValue("COMPANY", out string company);
                    item.TryGetValue("VEHICLE_SPEED", out string speed);
                    item.TryGetValue("VEHICLE_ID", out string vid);
                    item.TryGetValue("VEHICLE_DRIVER", out string driver);
                    temp.Add(sim, new ValueTuple<string, string, string>(type, vid, driver));
                    //检查车辆信息List里是否存在此车辆，存在则更改，不存在则新增
                    Redis.Set(sim + Redis_key_ext.vehicle, Utils.Util.ObjectSerializ(new ValueTuple<string, string, string, string, string, string>(id, type, company, speed, vid, driver)));
                }

                //围栏信息字段List（终端SIM，经度，纬度，围栏名称，围栏类型，车辆归属公司）
                fileName = new List<string> { "SIM", "XY", "NAME", "TYPES", "COMPANY" };
                sql = "select SIM,XY,NAME,TYPES,COMPANY from list_fence inner join (select VEHICLE_SIM from list_vehicle ) a on a.VEHICLE_SIM=SIM";
                new Fence().UpdateFence(mySql.MultipleSelect(sql, fileName), temp, Redis);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("车辆信息更新错误", ex);
            }
            finally
            {
                Resource.isVehicleUpdate = false;
            }
        }
    }
}