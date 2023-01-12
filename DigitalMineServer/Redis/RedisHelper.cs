using JtLibrary.Utils;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DigitalMineServer.Structures.Comprehensive;

namespace DigitalMineServer.Redis
{
    public class RedisHelper
    {
        private readonly RedisClient client;

        /// <summary>
        /// 取消ServiceStack.Rides每小时6000条操作限制
        /// </summary>
            public static void Execute()
            {
                var field = typeof(ServiceStack.LicenseUtils).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                    .FirstOrDefault(f => f.Name.Equals("__activatedLicense")); ;
                var atype = typeof(ServiceStack.LicenseUtils).Assembly.GetTypes()
                    .FirstOrDefault(t => t.Name.Equals("__ActivatedLicense"));
                var __activatedLicense = field.GetValue(null);
                if (__activatedLicense == null)
                {
                    var licenseKey = new ServiceStack.LicenseKey { Type = ServiceStack.LicenseType.FreeIndividual };//注册为个人免费版
                    var ctr = atype.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)[0];
                    __activatedLicense = ctr.Invoke(new object[] { licenseKey });
                    field.SetValue(null, __activatedLicense);
                }
                else
                {
                    var lfield = __activatedLicense.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                        .FirstOrDefault(f => f.Name.Equals("LicenseKey"));
                    var licenseKey = lfield.GetValue(__activatedLicense) as ServiceStack.LicenseKey;
                    if (licenseKey == null)
                    {
                        licenseKey = new ServiceStack.LicenseKey { Type = ServiceStack.LicenseType.FreeIndividual };
                        lfield.SetValue(__activatedLicense, licenseKey);
                    }
                    licenseKey.Type = ServiceStack.LicenseType.FreeIndividual;
                }
            }
        public RedisHelper()
        {
            client = new RedisClient("127.0.0.1", 6379, "admin");
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期时间：分钟，默认30分钟</param>
        /// <returns></returns>
        public bool Set(string key, byte[] value, int expiresIn = 0)
        {
            if (key == null)
            {
                return false;
            }
            if (expiresIn != 0)
            {
                return client.Set(key, value, new TimeSpan(ticks: expiresIn * 600000000L));
            }
            return client.Set(key, value, new TimeSpan(18000000000));
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期时间：分钟，默认30分钟,-1为永久</param>
        /// <returns></returns>
        public bool Set(string key, string value, int expiresIn = 0)
        {
            if (key == null)
            {
                return false;
            }
            if (expiresIn == -1)
            {
                return client.Set(key, value);
            }
            if (expiresIn == 0)
            {
                return client.Set(key, value, new TimeSpan(18000000000));
            }
            return client.Set(key, value, new TimeSpan(expiresIn * 600000000L));
        }

        public byte[] ReadBytes(string key)
        {
            return client.Get<byte[]>(key);
        }

        public string ReadString(string key)
        {
            return client.Get<string>(key);
        }

        public void Delete(string key)
        {
            if (CheckKeyExist(key))
            {
                client.Delete(key);
            }
        }

        /// <summary>
        /// 获取指定车辆
        /// </summary>
        /// <param name="sim"></param>
        /// <returns>
        /// item1：车辆编号外键
        /// item2：车辆类型
        /// item3：所属公司
        /// item4：超速阈值
        /// item5：车辆编号
        /// item6：司机
        /// </returns>
        public ValueTuple<string, string, string, string, string, string> GetVehicleList(string sim)
        {
            ValueTuple<string, string, string, string, string, string> Info = new ValueTuple<string, string, string, string, string, string>();
            byte[] buffer = ReadBytes(sim + Redis_key_ext.vehicle);
            if (buffer is null)
            {
                return Info;
            }
            return (ValueTuple<string, string, string, string, string, string>)Utils.Util.Deserialization(buffer);
        }

        /// <summary>
        /// 获取指定人员
        /// </summary>
        /// <param name="sim"></param>
        /// <returns>
        /// item1：人员编号外键
        /// item2：人员类型
        /// item3：所属公司
        /// item4：人员姓名或编号
        /// </returns>
        public ValueTuple<string, string, string, string> GetPersonList(string sim)
        {
            ValueTuple<string, string, string, string> Info = new ValueTuple<string, string, string, string>();
            byte[] buffer = ReadBytes(sim + Redis_key_ext.person);
            if (buffer is null)
            {
                return Info;
            }
            return (ValueTuple<string, string, string, string>)Utils.Util.Deserialization(buffer);
        }

        /// <summary>
        /// 获取设备版本号
        /// </summary>
        /// <param name="sim"></param>
        /// <returns>
        /// item1:808版本
        /// item2:1078版本
        /// item3:主动安全版本
        /// Item4:协议版本号，19版开始每次关键修订递增，初始版本为1
        /// </returns>
        public ValueTuple<string, string, string, int> GetEquipVersion(string sim)
        {
            ValueTuple<string, string, string, int> Info = new ValueTuple<string, string, string, int>();
            byte[] buffer = ReadBytes(sim + Redis_key_ext.equipVersion);
            if (buffer is null)
            {
                return Info;
            }
            return (ValueTuple<string, string, string, int>)Utils.Util.Deserialization(buffer);
        }

        /// <summary>
        /// 获取指定SIM的围栏信息
        /// </summary>
        /// <param name="sim"></param>
        /// <returns>
        /// item1:围栏名称
        /// item2:所属公司
        /// item3:车辆类型
        /// item4:车辆编号
        /// item5:司机，人员围栏为null
        /// item6:点集
        /// </returns>
        public Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>> GetFench(string sim, string fenchType)
        {
            byte[] buffer = ReadBytes(sim + fenchType);
            if (buffer is null)
            {
                return null;
            }
            return (Dictionary<string, ValueTuple<string, string, string, string, string, List<Point>>>)Utils.Util.Deserialization(buffer);
        }

        /// <summary>
        /// 检测键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckKeyExist(string key)
        {
            if (key == null)
            {
                return false;
            }
            if (client.Get(key) == null)
            {
                return false;
            }
            return true;
        }
    }
}