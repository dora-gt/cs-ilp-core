using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Test.Org.Interledger.Encoding.Asn
{
    public class AsnMyCustomObjectCodecSupplier : IAsnObjectCodecSupplier<AsnMyCustomObjectCodec, MyCustomObject>
    {
        public AsnMyCustomObjectCodec Get()
        {
            return new AsnMyCustomObjectCodec();
        }
    }
}
