using System;
using System.Numerics;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AsnUintCodecSupplier : IAsnObjectCodecSupplier<AsnUintCodec, BigInteger>
    {
        public AsnUintCodec Get()
        {
            return new AsnUintCodec();
        }
    }
}
