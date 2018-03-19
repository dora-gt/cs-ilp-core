using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnUint64CodecSupplier : IAsnObjectCodecSupplier<AsnUint64Codec, ulong>
    {
        public AsnUint64Codec Get()
        {
            return new AsnUint64Codec();
        }
    }
}
