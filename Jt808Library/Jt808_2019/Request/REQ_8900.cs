/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:52:25
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: 2af9e375-5a74-4794-8a97-6be6c535537b
---------------------------------------------------------------*/
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2019.Request_2019
{
   public class REQ_8900_2019
    {

        /// <summary>
        /// 数据体打包
        /// </summary>
        /// <returns></returns>
        public byte[] Encode(PB8900 info)
        {
            byte[] data = new byte[info.content.Length + 1];
            data[0] = info.mType;
            info.content.CopyTo(data, 1);
            return data;
        }
    }
}
