using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnBytesCodecSupplier : IAsnObjectCodecSupplier<byte[]>
    {
        public IAsnObjectCodec<byte[]> Get()
        {
            return new AsnOctetStringCodec(AsnSizeConstraint.UNCONSTRAINED);
        }
    }
}
