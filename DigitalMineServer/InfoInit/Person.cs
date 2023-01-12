using DigitalMineServer.Mysql;
using DigitalMineServer.Redis;
using DigitalMineServer.Static;
using DigitalMineServer.Util;
using System;
using System.Collections.Generic;
using static DigitalMineServer.Structures.Comprehensive;

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
                Dictionary<string, ValueTuple<string, string, string>> temp = new Dictionary<string, (string, string, string)>();
                //判断服务器返回的车辆信息是否为空
                if (result == null)
                {
                    return;
                }
                foreach (var item in result)
                {
                    item.TryGetValue("PERSON_SIM", out string sim);
                    item.TryGetValue("ID", out string id);
                    item.TryGetValue("PERSON_TYPE", out string type);
                    item.TryGetValue("COMPANY", out string company);
                    item.TryGetValue("PERSON_ID", out string vid);
                    temp.Add(sim, new ValueTuple<string, string, string>(type, vid, "未记录"));
                    Redis.Set(sim + Redis_key_ext.person, Utils.Util.ObjectSerializ(new ValueTuple<string, string, string, string>(id, type, company, vid)));
                }

                //围栏信息字段List（终端SIM，经度，纬度，围栏名称，围栏类型，归属公司）
                fileName = new List<string> { "SIM", "XY", "NAME", "TYPES", "COMPANY" };
                sql = "select SIM,XY,NAME,TYPES,COMPANY from list_fence inner join (select Person_sim from list_person ) a on a.Person_sim=SIM";
                //服务器围栏写入redis
                new Fence().UpdateFence(mySql.MultipleSelect(sql, fileName), temp, Redis);
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