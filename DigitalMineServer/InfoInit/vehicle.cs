using DigitalMineServer.implement;
using DigitalMineServer.Mysql;
using DigitalMineServer.Static;
using JtLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.VehicleInfo
{
    public class Vehicle
    {
        private readonly MySqlHelper mySql;
        public Vehicle()
        {
            mySql = new MySqlHelper();
        }

        public void VehicleInfo(object source, System.Timers.ElapsedEventArgs e)
        {
           //车辆信息字段List(数据库ID，终端Sim，车辆类型，车辆限速值，归属公司，车辆编号，驾驶员)
            List<string> fileName = new List<string> { "ID", "VEHICLE_SIM", "VEHICLE_TYPE", "VEHICLE_SPEED",  "COMPANY",  "VEHICLE_ID", "VEHICLE_DRIVER" };
            //更新车辆信息
            string sql = "select ID,VEHICLE_ID,VEHICLE_SIM,VEHICLE_TYPE,VEHICLE_SPEED,VEHICLE_DRIVER,COMPANY from list_vehicle";
            List<Dictionary<string, string>> result = mySql.MultipleSelect(sql, fileName);
            //临时信息存储，供围栏信息使用
            Dictionary<string, ValueTuple<string, string, string>> temp = new Dictionary<string, (string, string, string)>();
            //判断服务器返回的车辆信息是否为空，为空就清除车辆信息List
            if (result != null)
            {
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
                    if (Resource.VehicleList.ContainsKey(sim))
                    {
                        Resource.VehicleList[sim] = new ValueTuple<string, string, string, string, string, string>(id, type, company, speed, vid, driver);
                    }
                    else
                    {
                        Resource.VehicleList.TryAdd(sim, new ValueTuple<string, string, string, string, string, string>(id, type, company, speed, vid, driver));
                    }
                }
            }
            else {
                Resource.VehicleList.Clear();
            }
            //围栏信息字段List（终端SIM，经度，纬度，围栏名称，围栏类型，车辆归属公司）
            fileName = new List<string> { "SIM", "LON", "LAT", "NAME", "TYPES", "COMPANY" };
            sql = "select SIM,LON,LAT,NAME,TYPES,COMPANY from list_fence";
            result = mySql.MultipleSelect(sql, fileName);
            //判断服务器返回的信息是否为空，为空就清除车辆围栏List
            if (result != null)
            {
                foreach (var item in result)
                {
                    item.TryGetValue("SIM", out string sim);
                    item.TryGetValue("LON", out string lon);
                    item.TryGetValue("LAT", out string lat);
                    item.TryGetValue("NAME", out string name);
                    item.TryGetValue("COMPANY", out string company);
                    item.TryGetValue("TYPES", out string type);
                    string driver = "未记录";
                    string vid = "未记录";
                    string vType = "未记录";
                    if (temp.ContainsKey(sim))
                    {
                        vType = temp[sim].Item1;
                        vid = temp[sim].Item2;
                        driver = temp[sim].Item3;
                    }
                    //判断围栏类型
                    switch (type)
                    {
                        //禁止驶入围栏
                        case "forbid_in":
                            if (Resource.fenceFanbidInInfo.ContainsKey(sim))
                            {
                                List<Point> temp2 = Resource.fenceFanbidInInfo[sim].Item6;
                                temp2.Add(new Point(int.Parse(lon), int.Parse(lat)));
                                Resource.fenceFanbidInInfo[sim] = new ValueTuple<string, string, string, string, string, List<Point>>(name, company, vType, vid, driver, temp2);
                            }
                            else
                            {
                                Resource.fenceFanbidInInfo.TryAdd(sim, new ValueTuple<string, string, string, string, string, List<Point>>(name, company, vType, vid, driver, new List<Point>() { new Point(int.Parse(lon), int.Parse(lat)) }));
                            }
                            break;
                        //禁止驶出围栏
                        case "forbid_out":
                            if (Resource.fenceFanbidOutInfo.ContainsKey(sim))
                            {
                                List<Point> temp2 = Resource.fenceFanbidOutInfo[sim].Item6;
                                temp2.Add(new Point(int.Parse(lon), int.Parse(lat)));
                                Resource.fenceFanbidOutInfo[sim] = new ValueTuple<string, string, string, string, string, List<Point>>(name, company, vType, vid, driver, temp2);
                            }
                            else
                            {
                                Resource.fenceFanbidOutInfo.TryAdd(sim, new ValueTuple<string, string, string, string, string, List<Point>>(name, company, vType, vid, driver, new List<Point>() { new Point(int.Parse(lon), int.Parse(lat)) }));
                            }
                            break;
                    }
                }
            }
            else {
                Resource.fenceFanbidOutInfo.Clear();
                Resource.fenceFanbidInInfo.Clear();
            }
        }
    }
}
