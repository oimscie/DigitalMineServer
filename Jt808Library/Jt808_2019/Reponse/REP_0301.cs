/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/22 16:34:43
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: f2283289-01fc-4b93-bc26-18406148d6cd
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
    /// 事件报告
    /// </summary>
    public class REP_0301_2019
    {
        public REP_0301_2019()
        {
        }
        public PB0301 Decode(byte[] msgBody)
        {
            return new PB0301()
            {
                EventId = msgBody[0]
            };
        }
    }
}
