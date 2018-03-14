using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnUint32CodecSupplier : IAsnObjectCodecSupplier<uint>
    {
        public IAsnObjectCodec<uint> Get()
        {
            return new AsnUint32Codec();
        }
    }
}
