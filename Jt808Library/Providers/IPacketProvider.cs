namespace JtLibrary
{
    using Structures;

    public interface IPacketProvider
    {
        byte[] Encode(PacketFrom item);

        PacketMessage Decode(byte[] buffer, int offset, int count);
    }
}
