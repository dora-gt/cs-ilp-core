using System;
using System.Collections.Generic;

using Org.Interledger.Encoding.Asn.Codecs.Suppliers;
using Org.Interledger.Encoding.Asn.Serializers.Oer;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class CodecContextFactory
    {
        public static readonly String OCTET_ENCODING_RULES = "OER";

        public static CodecContext GetContext(String encodingRules)
        {
            AsnObjectCodecRegistry mappings = new AsnObjectCodecRegistry();
            mappings.Register(new AnsUint8CodecSupplier());
            mappings.Register(new AsnUint32CodecSupplier());
            mappings.Register(new AsnUint64CodecSupplier());
            mappings.Register(new AsnUintCodecSupplier());
            mappings.Register(new AsnUtf8StringCodecSupplier());
            mappings.Register(new AsnBytesCodecSupplier());

            AsnObjectSerializationContext serializers = new AsnObjectSerializationContext();
            if (OCTET_ENCODING_RULES.Equals(encodingRules))
            {
                serializers.Register(typeof(AsnUint8Codec), new AsnUint8OerSerializer());
                serializers.Register(typeof(AsnUint32Codec), new AsnOctetStringOerSerializer());
                serializers.Register(typeof(AsnUint64Codec), new AsnOctetStringOerSerializer());
                serializers.Register(typeof(AsnUtf8StringCodec), new AsnCharStringOerSerializer());
                serializers.Register(typeof(AsnUtf8StringBasedObjectCodecBase<>), new AsnCharStringOerSerializer());
                serializers.Register(typeof(AsnOctetStringCodec), new AsnOctetStringOerSerializer());
                serializers.Register(typeof(AsnOctetStringBasedObjectCodecBase<>), new AsnOctetStringOerSerializer());
                serializers.Register(typeof(AsnIA5StringCodec), new AsnCharStringOerSerializer());
                serializers.Register(typeof(AsnIA5StringBasedObjectCodecBase<>), new AsnCharStringOerSerializer());
                serializers.Register(typeof(AsnCharStringBasedObjectCodecBase<>), new AsnCharStringOerSerializer());
                serializers.Register(typeof(AsnSequenceCodecBase<>), new AsnSequenceOerSerializer(serializers));
                serializers.Register(typeof(AsnSequenceOfSequenceCodec<,,>), new AsnSequenceOfSequenceOerSerializer(serializers));
                // serializers.Register(typeof(AsnOpenTypeCodec), new AsnOpenTypeOerSerializer());
            }
            else
            {
                throw new CodecException(string.Format("Unknown encoding rules '{0}'", encodingRules));
            }

            return new CodecContext(mappings, serializers);
        }
    }
}
