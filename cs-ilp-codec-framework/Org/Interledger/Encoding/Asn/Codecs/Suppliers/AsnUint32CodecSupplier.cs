using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnUint32CodecSupplier : IAsnObjectCodecSupplier<AsnUint32Codec, uint>
    {
        public AsnUint32Codec Get()
        {
            return new AsnUint32Codec();
        }
    }
}
