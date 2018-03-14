using System;

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
            mappings.Register(new AsnUtf8StringCodecSupplier());
            mappings.Register(new AsnBytesCodecSupplier());

            AsnObjectSerializationContext serializers = new AsnObjectSerializationContext();
            if (OCTET_ENCODING_RULES.Equals(encodingRules))
            {
                serializers.Register<IAsnObjectCodec<byte>, byte>(typeof(AsnUint8Codec), new AsnUint8OerSerializer());
                serializers.Register<AsnOctetStringBasedObjectCodecBase<uint>, uint>(typeof(AsnUint32Codec), new AsnOctetStringOerSerializer<uint>());
                serializers.Register<AsnOctetStringBasedObjectCodecBase<ulong>, ulong>(typeof(AsnUint64Codec), new AsnOctetStringOerSerializer<ulong>());
                serializers.Register<AsnCharStringBasedObjectCodecBase<string>, string>(typeof(AsnUtf8StringCodec), new AsnCharStringOerSerializer());
                // serializers.Register(typeof(AsnUtf8StringBasedObjectCodecBase), new());
                serializers.Register<AsnOctetStringBasedObjectCodecBase<byte[]>, byte[]>(typeof(AsnOctetStringCodec), new AsnOctetStringOerSerializer<byte[]>());
                // serializers.Register(typeof(AsnOctetStringBasedObjectCodecBase), new AsnOctetStringOerSerializer());
                serializers.Register<AsnCharStringBasedObjectCodecBase<string>, string>(typeof(AsnIA5StringCodec), new AsnCharStringOerSerializer());
                // serializers.Register(typeof(AsnIA5StringBasedObjectCodecBase), new AsnCharStringOerSerializer());
                // serializers.Register(typeof(AsnCharStringBasedObjectCodecBase), new AsnCharStringOerSerializer());
                // serializers.Register(typeof(AsnSequenceCodecBase), new AsnSequenceOerSerializer());
                // serializers.Register(typeof(AsnSequenceOfSequenceCodec), new AsnSequenceOfSequenceOerSerializer());
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
