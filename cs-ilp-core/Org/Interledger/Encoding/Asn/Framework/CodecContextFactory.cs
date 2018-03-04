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

        private class AsnUint64CodecSupplier : IAsnObjectCodecSupplier<ulong>
        {
            public IAsnObjectCodec<ulong> Get()
            {
                return new AsnUint64Codec();
            }
        }

        private class AsnUtf8StringCodecSupplier : IAsnObjectCodecSupplier<string>
        {
            public IAsnObjectCodec<string> Get()
            {
                return new AsnUtf8StringCodec(AsnSizeConstraint.UNCONSTRAINED);
            }
        }

        private class AsnBytesCodecSupplier : IAsnObjectCodecSupplier<byte[]>
        {
            public IAsnObjectCodec<byte[]> Get()
            {
                return new AsnOctetStringCodec(AsnSizeConstraint.UNCONSTRAINED);
            }
        }

        public static readonly String OCTET_ENCODING_RULES = "OER";

        public static CodecContext GetContext(String encodingRules)
        {
            AsnObjectCodecRegistry mappings = new AsnObjectCodecRegistry();
            mappings.Register(new AnsUint8CodecSupplier());
            mappings.Register(new AsnUint32CodecSupplier());
            mappings.Register(new AsnUint64CodecSupplier());
            mappings.Register(new AsnUtf8StringCodecSupplier());
            mappings.Register(new AsnBytesCodecSupplier());

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
