using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AnsUint8CodecSupplier : IAsnObjectCodecSupplier<AsnUint8Codec, byte>
    {
        public AsnUint8Codec Get()
        {
            return new AsnUint8Codec();
        }
    }
}
