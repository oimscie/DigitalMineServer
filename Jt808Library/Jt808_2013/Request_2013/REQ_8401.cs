
using JtLibrary.PacketBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Jt808_2013.Request_2013
{
   public class REQ_8401_2013
    {
        private Encoding encoding = null;
        public REQ_8401_2013()
        {
            encoding= Encoding.GetEncoding("GBK");
        }
        /// <summary>
        /// 消息体打包
        /// </summary>
        /// <param name="info">联系人项</param>
        /// <returns></returns>
        public byte[] Encode(PB8401 info)
        {
            List<byte> list = new List<byte>((info.ContactList.Count << 4) + 1);

            //设置类型
            list.Add(info.sType);
            if (info.sType != 0)
            {
                //设置联系人总数
                byte cCount = (byte)info.ContactList.Count;
                list.Add(cCount);

                byte[] temp = null;
                for (int i = 0; i < cCount; i++)
                {
                    list.Add(info.ContactList[i].flag);

                    temp = encoding.GetBytes(info.ContactList[i].mobile);
                    list.Add((byte)temp.Length);
                    list.AddRange(temp);

                    temp = encoding.GetBytes(info.ContactList[i].contact);
                    list.Add((byte)temp.Length);
                    list.AddRange(temp);
                }
            }
            return list.ToArray();
        }
    }
}
