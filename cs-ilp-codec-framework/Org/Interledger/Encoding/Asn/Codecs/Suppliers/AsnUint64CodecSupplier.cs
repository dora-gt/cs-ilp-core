using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnUint64CodecSupplier : IAsnObjectCodecSupplier<ulong>
    {
        public IAsnObjectCodec<ulong> Get()
        {
            return new AsnUint64Codec();
        }
    }
}
