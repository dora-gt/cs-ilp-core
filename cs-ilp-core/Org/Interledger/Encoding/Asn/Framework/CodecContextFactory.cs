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

            AsnObjectSerializationContext serializers = new AsnObjectSerializationContext();
            if (OCTET_ENCODING_RULES.Equals(encodingRules))
            {
                serializers.Register(typeof(AsnUint8Codec), new AsnUint8OerSerializer());
                serializers.Register(typeof(AsnUint32Codec), new AsnOctetStringOerSerializer<uint>());
                serializers.Register(typeof(AsnUint64Codec), new AsnOctetStringOerSerializer<ulong>());
            }
            else
            {
                throw new CodecException(string.Format("Unknown encoding rules '{0}'", encodingRules));
            }

            return new CodecContext(mappings, serializers);
        }
    }
}
