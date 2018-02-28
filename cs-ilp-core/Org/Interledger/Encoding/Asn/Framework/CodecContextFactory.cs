using System;

using Org.Interledger.Encoding.Asn.Codecs;
using Org.Interledger.Encoding.Asn.Serializers.Oer;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class CodecContextFactory
    {
        private class AnsUint8CodecSupplier : IAsnObjectCodecSupplier<byte>
        {
            public IAsnObjectCodec<byte> Get()
            {
                return new AsnUint8Codec();
            }
        }

        private class AsnUint32CodecSupplier : IAsnObjectCodecSupplier<uint>
        {
            public IAsnObjectCodec<uint> Get()
            {
                return new AsnUint32Codec();
            }
        }

        public static readonly String OCTET_ENCODING_RULES = "OER";

        public static CodecContext GetContext(String encodingRules)
        {
            AsnObjectCodecRegistry mappings = new AsnObjectCodecRegistry();
            mappings.Register(new AnsUint8CodecSupplier());
            mappings.Register(new AsnUint32CodecSupplier());

            AsnObjectSerializationContext serializers = null;
            if (OCTET_ENCODING_RULES.Equals(encodingRules))
            {
                serializers = new AsnObjectSerializationContext(new AsnObjectCodecOerReader(), new AsnObjectCodecOerWriter());
            }
            else
            {
                throw new CodecException(string.Format("Unknown encoding rules '{0}'", encodingRules));
            }

            return new CodecContext(mappings, serializers);
        }
    }
}
