using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs.Suppliers
{
    public class AnsUint8CodecSupplier : IAsnObjectCodecSupplier<byte>
    {
        public IAsnObjectCodec<byte> Get()
        {
            return new AsnUint8Codec();
        }
    }
}
