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
using static DigitalMineServer.Structures.Comprehensive;
using DigitalMineServer.Redis;

namespace DigitalMineServer.InfoInit
{
    public class Person
    {
        private readonly MySqlHelper mySql;
        private readonly RedisHelper Redis;

        public Person()
        {
            mySql = new MySqlHelper();
            Redis = new RedisHelper();
        }

        public void PersonInfo(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Resource.isPersonUpdate = true;
                //人员信息字段List(数据库ID，终端Sim，人员类型，归属公司，人员姓名或编号)
                List<string> fileName = new List<string> { "ID", "PERSON_SIM", "PERSON_TYPE", "COMPANY", "PERSON_ID" };
                //更新人员信息
                string sql = "select ID,PERSON_ID,PERSON_SIM,PERSON_TYPE,COMPANY from list_person";
                List<Dictionary<string, string>> result = mySql.MultipleSelect(sql, fileName);
                //临时信息存储，供围栏信息使用
                Dictionary<string, ValueTuple<string, string>> temp = new Dictionary<string, (string, string)>();
                //判断服务器返回的车辆信息是否为空，为空就清除车辆信息List
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        item.TryGetValue("PERSON_SIM", out string sim);
                        item.TryGetValue("ID", out string id);
                        item.TryGetValue("PERSON_TYPE", out string type);
                        item.TryGetValue("COMPANY", out string company);
                        item.TryGetValue("PERSON_ID", out string vid);
                        temp.Add(sim, new ValueTuple<string, string>(type, vid));
                        Redis.Set(sim + Redis_key_ext.person, Utils.Util.ObjectSerializ(new ValueTuple<string, string, string, string>(id, type, company, vid)));
                    }
                }
                //围栏信息字段List（终端SIM，经度，纬度，围栏名称，围栏类型，归属公司）
                fileName = new List<string> { "SIM", "X", "Y", "NAME", "TYPES", "COMPANY" };
                sql = "select SIM,X,Y,NAME,TYPES,COMPANY from list_fence ORDER BY ID ";
                result = mySql.MultipleSelect(sql, fileName);
                //判断服务器返回的信息是否为空，为空就清除人员围栏List
                if (result != null)
                {
                    //禁止驶出围栏临时存储

                    Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>> tempDicOut = new Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>>();

                    //禁止驶入围栏临时存储

                    Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>> tempDicIn = new Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>>();
                    foreach (var item in result)
                    {
                        item.TryGetValue("SIM", out string sim);
                        item.TryGetValue("X", out string x);
                        item.TryGetValue("Y", out string y);
                        item.TryGetValue("NAME", out string name);
                        item.TryGetValue("COMPANY", out string company);
                        item.TryGetValue("TYPES", out string type);
                        string vid = "未记录";
                        string vType = "未记录";
                        if (temp.ContainsKey(sim))
                        {
                            vType = temp[sim].Item1;
                            vid = temp[sim].Item2;
                        }
                        //判断围栏类型
                        switch (type)
                        {
                            //禁止入围栏
                            case "forbid_in":
                                if (tempDicIn.ContainsKey(sim))
                                {
                                    foreach (var item_value in tempDicIn)
                                    {
                                        if (item_value.Value.ContainsKey(name))
                                        {
                                            item_value.Value[name].Item6.Add(new Point(double.Parse(x), double.Parse(y)));
                                        }
                                        else
                                        {
                                            item_value.Value.Add(name,
                                                new ValueTuple<string, string, string, string, string, List<Point>>(
                                                    name, company, vType, vid, null, new List<Point>() {
                                                        new Point(double.Parse(x), double.Parse(y)) }
                                                    )
                                                );
                                        }
                                    }
                                }
                                else
                                {
                                    tempDicIn.Add(sim,
                                        new Dictionary<string, (string, string, string, string, string, List<Point>)>
                                    {
                                        { name,new ValueTuple<string, string, string,string, string, List<Point>>(
                                            name, company, vType, vid,null, new List<Point>() {
                                                new Point(double.Parse(x), double.Parse(y)) })
                                        }
                                    });
                                }
                                break;
                            //禁止出围栏
                            case "forbid_out":
                                if (tempDicOut.ContainsKey(sim))
                                {
                                    foreach (var item_value in tempDicOut)
                                    {
                                        if (item_value.Value.ContainsKey(name))
                                        {
                                            item_value.Value[name].Item6.Add(new Point(double.Parse(x), double.Parse(y)));
                                        }
                                        else
                                        {
                                            item_value.Value.Add(
                                                name, new ValueTuple<string, string, string, string, string, List<Point>>(
                                                    name, company, vType, vid, null, new List<Point>() {
                                                        new Point(double.Parse(x), double.Parse(y)) })
                                                );
                                        }
                                    }
                                }
                                else
                                {
                                    tempDicOut.Add(sim, new Dictionary<string, (string, string, string, string, string, List<Point>)>
                                    {
                                        { name, new ValueTuple<string, string,string, string, string, List<Point>>(
                                            name, company, vType, vid, null,new List<Point>() {
                                                new Point(double.Parse(x), double.Parse(y)) }
                                            ) }
                                    });
                                }
                                break;
                        }
                    }
                    //更新人员禁止出围栏信息
                    foreach (var item in tempDicOut)
                    {
                        Redis.Set(item.Key + Redis_key_ext.fench_out, Utils.Util.ObjectSerializ(item.Value));
                    }
                    //更新人员禁止入围栏信息
                    foreach (var item in tempDicIn)
                    {
                        Redis.Set(item.Key + Redis_key_ext.fench_in, Utils.Util.ObjectSerializ(item.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("人员信息更新错误", ex);
            }
            finally
            {
                Resource.isPersonUpdate = false;
            }
        }
    }
}