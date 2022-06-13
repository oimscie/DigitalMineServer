using JtLibrary.PacketBody;
using JtLibrary.PacketBody.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Providers
{
    using Structures;

    public interface IReqPacketFrom
    {
        byte[] Encoding(PacketFrom from);
    }

    public class ReqPacketFrom :IReqPacketFrom
    {
        protected IPacketProvider provider = null;

        public PacketFrom packetFrom { get; private set; }

        public ReqPacketFrom(IPacketProvider provider)
        {
            this.provider = provider;
        }

        public byte[] Encoding(PacketFrom from)
        {
            this.packetFrom = from;

            return provider.Encode(from);
        }
    }

    public class ReqParam
    {
        public ushort msgSerialnumber { get; set; } = 0;
        public byte pEncryptFlag { get; set; } = 0;
        public ushort pSerialnumber { get; set; } = 1;
        public byte pSubFlag { get; set; } = 0;
        public ushort pTotal { get; set; } = 1;
        public string simNumber { get; set; } = string.Empty;
    }

    public class Req8001Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8001;

        public Req8001Packet(IPacketProvider provider) 
           : base(provider)
        {

        }

        public byte[] Encode(PB8001 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8001().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8003Packet:ReqPacketFrom
    {
        public const ushort Cmd = 0x8003;

        public Req8003Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8003 body, ReqParam reqParam)
        {
            var p= new PacketFrom()
            {
                msgBody = new  REQ_8003().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8100Packet:ReqPacketFrom
    {
        public const ushort Cmd = 0x8100;

        public Req8100Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8100 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8100().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8103Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8103;

        public Req8103Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8103 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8103().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8105Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8105;

        public Req8105Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8105 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8105().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8106Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8106;

        public Req8106Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8106 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8106().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8108Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8108;

        public Req8108Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8108 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8108().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8202Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8202;

        public Req8202Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8202 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8202().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8203Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8203;

        public Req8203Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8203 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8203().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8300Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8300;

        public Req8300Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8300 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8300().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8301Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8301;

        public Req8301Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8301 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8301().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8302Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8302;

        public Req8302Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8302 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8302().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8303Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8303;

        public Req8303Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8303 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8303().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8304Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8304;

        public Req8304Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8304 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8304().Encode(new ByteString() {
                     StringValue=body.StringValue,
                      Value=body.Value
                }),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8400Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8400;

        public Req8400Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8400 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8400().Encode(new ByteString()
                {
                    StringValue = body.StringValue,
                    Value = body.Value
                }),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8401Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8401;

        public Req8401Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8401 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8401().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8500Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8500;

        public Req8500Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(byte body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8500().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8600Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8600;

        public Req8600Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8600 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8600().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8601Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8601;

        public Req8601Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(List<uint> body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8601().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8602Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8602;

        public Req8602Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8602 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8602().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8603Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8603;

        public Req8603Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(List<uint> body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8603().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8604Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8604;

        public Req8604Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8604 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8604().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8605Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8605;

        public Req8605Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(List<uint> body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8605().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8606Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8606;

        public Req8606Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8606 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8606().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8607Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8607;

        public Req8607Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(List<uint> body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8607().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8700Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8700;

        public Req8700Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8701 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8700().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8701Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8701;

        public Req8701Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8701 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8701().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8800Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8800;

        public Req8800Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8800 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8800().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8801Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8801;

        public Req8801Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8801 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8801().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8802Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8802;

        public Req8802Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8802 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8802().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8803Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8803;

        public Req8803Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8803 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8803().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8804Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8804;

        public Req8804Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8804 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8804().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8805Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8805;

        public Req8805Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8805 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8805().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8900Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8900;

        public Req8900Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(PB8900 body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8900().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }

    public class Req8A00Packet : ReqPacketFrom
    {
        public const ushort Cmd = 0x8A00;

        public Req8A00Packet(IPacketProvider provider)
            : base(provider)
        {

        }

        public byte[] Encode(UInt32Bytes body, ReqParam reqParam)
        {
            var p = new PacketFrom()
            {
                msgBody = new REQ_8A00().Encode(body),
                msgId = Cmd,
                msgSerialnumber = reqParam.msgSerialnumber,
                pEncryptFlag = reqParam.pEncryptFlag,
                pSerialnumber = reqParam.pSerialnumber,
                pSubFlag = reqParam.pSubFlag,
                pTotal = reqParam.pTotal,
                simNumber = reqParam.simNumber.ToBCD(),
            };

            return Encoding(p);
        }
    }
}
