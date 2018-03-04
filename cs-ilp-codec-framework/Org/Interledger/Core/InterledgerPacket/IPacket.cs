using System;

namespace Org.Interledger.Core.InterledgerPacket
{
    public interface IPacket
    {
        PacketTypes PacketType { get; }
    }
}
