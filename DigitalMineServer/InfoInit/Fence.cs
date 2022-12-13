using DigitalMineServer.Redis;
using JtLibrary.Utils;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DigitalMineServer.Structures.Comprehensive;

namespace DigitalMineServer.InfoInit
{
    public class Fence
    {
        /// <summary>
        /// 更新围栏信息
        /// </summary>
        /// <param name="result">数据库返回的围栏搜索结果</param>
        /// <param name="info">sim对应的信息</param>
        /// <param name="Redis">redis连接头</param>
        public void UpdateFence(List<Dictionary<string, string>> result, Dictionary<string, ValueTuple<string, string, string>> info, RedisHelper Redis)
        {
            //判断服务器返回的信息是否为空
            if (result == null)
            {
                return;
            }
            //禁止驶出围栏临时存储

            Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>> tempDicOut = new Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>>();

            //禁止驶入围栏临时存储

            Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>> tempDicIn = new Dictionary<string, Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>>();

            foreach (var item in result)
            {
                item.TryGetValue("SIM", out string sim);
                item.TryGetValue("XY", out string xy);
                item.TryGetValue("NAME", out string name);
                item.TryGetValue("COMPANY", out string company);
                item.TryGetValue("TYPES", out string type);
                string driver = "未记录";
                string vid = "未记录";
                string vType = "未记录";
                if (info.ContainsKey(sim))
                {
                    vType = info[sim].Item1;
                    vid = info[sim].Item2;
                    driver = info[sim].Item3;
                }
                //判断围栏类型
                switch (type)
                {
                    //禁止驶入围栏
                    case "禁入":
                        if (tempDicIn.ContainsKey(sim))
                        {
                            tempDicIn[sim].Add(name,
                                    new ValueTuple<string, string, string, string, string, List<Point>>(
                                        name, company, vType, vid, driver, Utils.Util.SerializationPoint(xy))
                                    );
                        }
                        else
                        {
                            tempDicIn.Add(sim,
                                new Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>
                            {
                                        { name,new ValueTuple<string, string, string, string, string, List<Point>>(
                                            name, company, vType, vid, driver,Utils.Util.SerializationPoint(xy))
                                    }
                                });
                        }
                        break;
                    //禁止驶出围栏
                    case "禁出":
                        if (tempDicOut.ContainsKey(sim))
                        {
                            tempDicOut[sim].Add(name,
                                   new ValueTuple<string, string, string, string, string, List<Point>>(
                                       name, company, vType, vid, driver, Utils.Util.SerializationPoint(xy))
                                   );
                        }
                        else
                        {
                            tempDicOut.Add(sim, new Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>
                                    {
                                        { name, new ValueTuple<string, string, string, string, string, List<Point>>(
                                            name, company, vType, vid, driver, Utils.Util.SerializationPoint(xy)
                                            )
                                    }});
                        }
                        break;
                }
            }
            //更新车辆禁止驶出围栏信息
            foreach (var item in tempDicOut)
            {
                Redis.Set(item.Key + Redis_key_ext.fench_out, Utils.Util.ObjectSerializ(item.Value));
            }
            //更新车辆禁止驶入围栏信息
            foreach (var item in tempDicIn)
            {
                Redis.Set(item.Key + Redis_key_ext.fench_in, Utils.Util.ObjectSerializ(item.Value));
            }
        }
    }
}