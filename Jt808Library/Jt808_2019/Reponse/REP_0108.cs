/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:30:54
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 51463e00-e379-4a2e-a7e3-a2b49c9338a0
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Reponse_2019
{
    /// <summary>
    /// 终端升级数据返回结果
    /// </summary>
    public class REP_0108_2019
    {
        public REP_0108_2019()
        {
        }
        /// <summary>
        /// 终端升级结果回应数据解析
        /// </summary>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public PB0108 Decode(byte[] msgBody)
        {
            return new PB0108()
            {
                UpdateType = msgBody[0],
                UpdateResult = msgBody[1]
            };
        }
    }
}
