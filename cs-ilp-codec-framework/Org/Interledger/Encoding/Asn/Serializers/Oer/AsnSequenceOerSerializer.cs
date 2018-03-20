using System;
using System.IO;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnSequenceOerSerializer : IAsnObjectSerializer<IAsnSequenceCodec>
    {
        public void Read(AsnObjectSerializationContext context, IAsnSequenceCodec instance, Stream inputStream)
        {
            for (int index = 0; index < instance.Size; index++)
            {
                context.Read(instance.GetCodecAt(index), inputStream);
            }
        }

        public void Write(AsnObjectSerializationContext context, IAsnSequenceCodec instance, Stream outputStream)
        {
            for (int index = 0; index < instance.Size; index++)
            {
                context.Write(instance.GetCodecAt(index), outputStream);
            }
        }
    }
}
