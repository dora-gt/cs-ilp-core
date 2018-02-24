using System;
namespace Org.Interledger.Core.InterledgerPacket
{
    public class PreparePacket : IPacket
    {
        public PacketTypes PacketType { get { return PacketTypes.ILP_PAREPARE_TYPE; } }

        public ulong Amount { get; set; }

        public Timestamp ExpiresAt { get; set; }

        public byte[] ExecutionCondition { get; set; }

        public PreparePacket()
        {
        }
    }
}
