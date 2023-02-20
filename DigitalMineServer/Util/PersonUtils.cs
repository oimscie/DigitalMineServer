using DigitalMineServer.Mysql;
using DigitalMineServer.Redis;
using JtLibrary.Structures;
using JtLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DigitalMineServer.Structures.Comprehensive;
using static ServiceStack.Script.Lisp;

namespace DigitalMineServer.Util
{
    public class PersonUtils
    {
        private readonly MySqlHelper PersonMysql;

        private readonly RedisHelper PersonRedis;

        public PersonUtils()
        {
            PersonMysql = new MySqlHelper();
            PersonRedis = new RedisHelper();
        }

        /// <summary>
        /// 打卡位置判别
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xy"></param>
        /// <param name="company"></param>
        public void ClockInCheck(string name, List<double> xy, string company)
        {
            //判断打卡围栏
            List<Point> points = PersonRedis.GetClockInFench(company + Redis_key_ext.clock_in);
            if (points == null || !Polygon.IsInPolygon(new Point(xy[0], xy[1]), points))
            {
                return;
            }
            string sql = "select count(ID) as Count from rec_clockin_temp where USERNAME='" + name + "' and add_time='" + DateTime.Now.ToShortDateString() + "'and company='" + company + "'";
            if (PersonMysql.GetCount(sql) == 0)
            {
                //插入永久表
                sql = "INSERT INTO `rec_clockin`( `USERNAME`, `FIRSTCLOCK`, `LASTCLOCK`, `COMPANY`, `ADD_TIME`, `temp1`, `temp2`, `temp3`, `temp4`) VALUES ( '" + name + "', '" + DateTime.Now + "', '" + DateTime.Now + "', '" + company + "', '" + DateTime.Now.ToShortDateString() + "', NULL, NULL, NULL, NULL);";
                PersonMysql.UpdOrInsOrdel(sql);
                //插入临时表
                sql = "INSERT INTO `rec_clockin_temp`( `USERNAME`, `FIRSTCLOCK`, `LASTCLOCK`, `COMPANY`, `ADD_TIME`, `temp1`, `temp2`, `temp3`, `temp4`) VALUES ( '" + name + "', '" + DateTime.Now + "', '" + DateTime.Now + "', '" + company + "', '" + DateTime.Now.ToShortDateString() + "', NULL, NULL, NULL, NULL)";
                PersonMysql.UpdOrInsOrdel(sql);
            }
            else
            {
                sql = "select ID as id from rec_clockin_temp where company='" + company + "' and USERNAME='" + name + "' and add_time='" + DateTime.Now.ToShortDateString() + "'";
                string id = PersonMysql.SingleSelect_Str(sql, "id");
                if (id != null)
                {
                    sql = "update rec_clockin_temp set LASTCLOCK='" + DateTime.Now + "' where ID='" + id + "'";
                    PersonMysql.UpdOrInsOrdel(sql);
                }
                sql = "select ID as id from rec_clockin where company='" + company + "' and USERNAME='" + name + "' and add_time='" + DateTime.Now.ToShortDateString() + "'";
                id = PersonMysql.SingleSelect_Str(sql, "id");
                if (id != null)
                {
                    sql = "update rec_clockin set LASTCLOCK='" + DateTime.Now + "' where ID='" + id + "'";
                    PersonMysql.UpdOrInsOrdel(sql);
                }
            }
        }

        /// <summary>
        /// 判断人员围栏
        /// </summary>
        /// <param name="sim">SIM号</param>
        /// <param name="xy">2000坐标</param>
        public void CheckPersonFence(string sim, List<double> xy)
        {
            //判断禁入围栏

            Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>> dic = PersonRedis.GetFench(sim, Redis_key_ext.fench_in);
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    if (Polygon.IsInPolygon(new Point(xy[0], xy[1]), item.Value.Item6))
                    {
                        string sql = "select COUNT(ID) as Count from rec_unu_info where COMPANY='" + item.Value.Item2 + "' and WARN_USER_ID='" + item.Value.Item4 + "' and  USERTYPE='人员' and  WARNTYPE='" + WarnType.Forbid_In + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 2 MINUTE)";
                        if (PersonMysql.GetCount(sql) == 0)
                        {
                            sql = "INSERT INTO `product`.`rec_unu_info`( `WARN_USER_ID`, `WARN_USER_TYPE`,` USERTYPE`, `WARNTYPE`, `INFO`, `DRIVER`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + item.Value.Item4 + "','" + item.Value.Item3 + "','人员', '" + WarnType.Forbid_In + "', '围栏名称：" + item.Value.Item1 + "', '', '" + item.Value.Item2 + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL)";
                            PersonMysql.UpdOrInsOrdel(sql);
                        }
                    }
                }
            }

            //判断禁出围栏
            dic = PersonRedis.GetFench(sim, Redis_key_ext.fench_out);
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    if (!Polygon.IsInPolygon(new Point(xy[0], xy[1]), item.Value.Item6))
                    {
                        string sql = "select COUNT(ID) as Count from rec_unu_info where COMPANY='" + item.Value.Item2 + "' and WARN_USER_ID='" + item.Value.Item4 + "' and USERTYPE='人员' and  WARNTYPE='" + WarnType.Forbid_Out + "' and ADD_TIME>=DATE_SUB(NOW(),INTERVAL 2 MINUTE)";
                        if (PersonMysql.GetCount(sql) == 0)
                        {
                            sql = "INSERT INTO `product`.`rec_unu_info`( `WARN_USER_ID`, `WARN_USER_TYPE`,` USERTYPE`, `WARNTYPE`, `INFO`, `DRIVER`, `COMPANY`, `ADD_TIME`, `TEMP1`, `TEMP2`, `TEMP3`, `TEMP4`) VALUES ('" + item.Value.Item4 + "','" + item.Value.Item3 + "','人员','" + WarnType.Forbid_Out + "', '围栏名称：" + item.Value.Item1 + "', '', '" + item.Value.Item2 + "', '" + DateTime.Now + "', NULL, NULL, NULL, NULL)";
                            PersonMysql.UpdOrInsOrdel(sql);
                        }
                    }
                }
            }
        }
    }
}