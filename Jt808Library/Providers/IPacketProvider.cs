namespace JtLibrary.Providers
{
    using Structures;

    public interface IPacketProvider
    {
        byte[] Encode_2013(PacketFrom item);
        byte[] Encode_2019(PacketFrom item);
        PacketMessage Decode(byte[] buffer, int offset, int count);
    }
}
