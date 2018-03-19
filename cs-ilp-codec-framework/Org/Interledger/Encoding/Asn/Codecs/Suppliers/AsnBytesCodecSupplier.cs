using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnBytesCodecSupplier : IAsnObjectCodecSupplier<AsnOctetStringCodec, byte[]>
    {
        public AsnOctetStringCodec Get()
        {
            return new AsnOctetStringCodec(AsnSizeConstraint.UNCONSTRAINED);
        }
    }
}
