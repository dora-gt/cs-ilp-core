using System;

using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class CodecContextFactory
    {

        private class AnsUint8CodecSupplier : IAsnObjectCodecSupplier<int>
        {
            public IAsnObjectCodec<int> Get()
            {
                return new AsnUint8Codec();
            }
        }

        public static readonly String OCTET_ENCODING_RULES = "OER";

        public static CodecContext GetContext(String encodingRules)
        {
            AsnObjectCodecRegistry mappings = new AsnObjectCodecRegistry();

                //.Register(new AnsUint8CodecSupplier());

            AsnObjectSerializationContext serializers = new AsnObjectSerializationContext();
            if (OCTET_ENCODING_RULES.Equals(encodingRules))
            {
                // serializers.Register
            }
            else
            {
                throw new CodecException(string.Format("Unknown encoding rules '{0}'", encodingRules));
            }

            return new CodecContext(mappings, serializers);
        }
    }
}
