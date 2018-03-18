using System;
using System.IO;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnSequenceOerSerializer : IAsnObjectSerializer<IAsnSequenceCodec>
    {
        private AsnObjectSerializationContext AsnObjectSerializationContext { get; set; }

        public AsnSequenceOerSerializer(AsnObjectSerializationContext asnObjectSerializationContext)
        {
            this.AsnObjectSerializationContext = asnObjectSerializationContext;
        }

        public void Read(IAsnSequenceCodec instance, Stream inputStream)
        {
            for (int index = 0; index < instance.Size; index++)
            {
                this.AsnObjectSerializationContext.Read(instance.GetCodecAt(index), inputStream);
            }
        }

        public void Write(IAsnSequenceCodec instance, Stream outputStream)
        {
            for (int index = 0; index < instance.Size; index++)
            {
                this.AsnObjectSerializationContext.Write(instance.GetCodecAt(index), outputStream);
            }
        }
    }
}
