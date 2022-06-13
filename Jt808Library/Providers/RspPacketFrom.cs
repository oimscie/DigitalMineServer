using JtLibrary.PacketBody;
using JtLibrary.PacketBody.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Providers
{
    using Structures;

    public interface IRspPacketFrom
    {
        T Decode<T>(byte[] buffer);
        T Decode<T>(byte[] buffer, int offset, int size);
    }

    public class RspPacketFrom : IRspPacketFrom
    {
        private IPacketProvider provider = null;

        public RspPacketFrom(IPacketProvider provider)
        {
           this.provider = provider;
        }

        public T Decode<T>(byte[] buffer, int offset, int size)
        {
            PacketMessage msg = provider.Decode(buffer, offset, size);
            if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0102)
            {
                var val = new REP_0102().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0102));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0100)
            {
                var val = new REP_0100().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0100));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0001)
            {
                var val = new REP_0001().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0001));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0104)
            {
                var val = new REP_0104().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0104));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0107)
            {
                var val = new REP_0107().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0107));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0108)
            {
                var val = new REP_0108().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0108));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0200)
            {
                var val = new REP_0200().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0200));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0201)
            {
                var val = new REP_0201().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0201));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0301)
            {
                var val = new REP_0301().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0301));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0302)
            {
                var val = new REP_0302().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0302));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0303)
            {
                var val = new REP_0303().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0303));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0500)
            {
                var val = new REP_0500().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0500));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0700)
            {
                var val = new REP_0700().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0700));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0701)
            {
                var val = new REP_0701().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0701));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0702)
            {
                var val = new REP_0702().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0702));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0704)
            {
                var val = new REP_0704().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0704));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0705)
            {
                var val = new REP_0705().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0705));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0800)
            {
                var val = new REP_0800().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0800));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0801)
            {
                var val = new REP_0801().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0801));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0802)
            {
                var val = new REP_0802().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0802));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0805)
            {
                var val = new REP_0805().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0805));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0900)
            {
                var val = new REP_0900().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0900));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0901)
            {
                var val = new REP_0901().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0901));
            }
            else if (msg.pmPacketHead.phMessageId == JT808Cmd.RSP_0A00)
            {
                var val = new REP_0A00().Decode(msg.pmMessageBody);
                return (T)Convert.ChangeType(val, typeof(PB0A00));
            }

            throw new Exception(msg.pmPacketHead.phMessageId + ",该消息Id找不到对应解析实例");
        }


        public T Decode<T>(byte[] buffer)
        {
            return Decode<T>(buffer, 0, buffer.Length);
        }
    }
}
