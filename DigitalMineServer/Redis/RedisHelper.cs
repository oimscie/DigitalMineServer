using JtLibrary.Utils;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.Redis
{
    public class RedisHelper
    {
        private readonly RedisClient client = new RedisClient("127.0.0.1", 6379, "admin");

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
        /// <param name="expiresIn">过期时间：分钟，默认30分钟</param>
        /// <returns></returns>
        public bool Set(string key, string value, int expiresIn = 0)
        {
            if (key == null)
            {
                return false;
            }
            if (expiresIn != 0)
            {
                return client.Set(key, value, new TimeSpan(expiresIn * 600000000L));
            }
            return client.Set(key, value, new TimeSpan(18000000000));
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
            client.Delete(key);
        }
    }
}