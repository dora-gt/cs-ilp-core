using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Test.Org.Interledger.Encoding.Asn
{
    public class AsnMyCustomObjectCodecSupplier : IAsnObjectCodecSupplier<MyCustomObject>
    {
        public IAsnObjectCodec<MyCustomObject> Get()
        {
            return new AsnMyCustomObjectCodec();
        }
    }
}
