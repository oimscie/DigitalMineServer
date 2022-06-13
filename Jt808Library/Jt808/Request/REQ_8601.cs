/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 19:19:47
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *   guid: f9f3830d-6165-48be-b416-750567336d83
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace JtLibrary.PacketBody.Request
{
   public class REQ_8601
    {
        /// <summary>
        /// 删除区域消息体打包,区域数0-125，0为删除指定消息指令的所有区域
        /// </summary>
        /// <param name="areaId">区域ID列表</param>
        /// <returns></returns>
        public byte[] Encode(List<UInt32> areaId)
        {
            byte[] data = new byte[(areaId.Count << 2) + 1];
            data[0] = (byte)areaId.Count;

            if (areaId.Count > 0)
            {
                byte[] temp = null;
                for (int i = 0; i < data[0]; ++i)
                {
                    temp = areaId[i].ToBytes();
                    temp.CopyTo(data, 1 + (i << 2));
                }
            }
            return data;
        }
    }
}
