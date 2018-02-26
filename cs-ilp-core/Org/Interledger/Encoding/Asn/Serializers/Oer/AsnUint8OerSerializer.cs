using System;
using System.IO;

using Org.Interledger.Encoding.Asn.Codecs;
using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnUint8OerSerializer : IAsnObjectSerializer<AsnUint8Codec, int>
    {
        public void Read(AsnObjectSerializationContext context, AsnUint8Codec instance, Stream inputStream)
        {
            Objects.RequireNonNull(context);
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);

            int value = inputStream.ReadByte();
            if (value == -1)
            {
                throw new CodecException("Unexpected end of stream.");
            }

            instance.Encode(value);
        }

        public void Write(AsnObjectSerializationContext context, AsnUint8Codec instance, Stream outputStream)
        {
            if (255 < instance.Decode())
            {
                throw new FormatException("UInt8 values may only contain up to 8 bits!");
            }

            outputStream.WriteByte((byte)instance.Decode());
        }
    }
}
