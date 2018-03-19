using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnUtf8StringCodecSupplier : IAsnObjectCodecSupplier<AsnUtf8StringCodec, string>
    {
        public AsnUtf8StringCodec Get()
        {
            return new AsnUtf8StringCodec(AsnSizeConstraint.UNCONSTRAINED);
        }
    }
}
