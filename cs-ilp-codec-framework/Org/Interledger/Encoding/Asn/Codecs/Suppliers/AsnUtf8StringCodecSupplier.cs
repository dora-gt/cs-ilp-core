using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnUtf8StringCodecSupplier : IAsnObjectCodecSupplier<string>
    {
        public IAsnObjectCodec<string> Get()
        {
            return new AsnUtf8StringCodec(AsnSizeConstraint.UNCONSTRAINED);
        }
    }
}
