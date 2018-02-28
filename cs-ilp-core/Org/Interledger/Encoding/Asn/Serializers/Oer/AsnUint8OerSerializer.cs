using System;
using System.IO;

using Org.Interledger.Encoding.Asn.Codecs;
using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnUint8OerSerializer : IAsnObjectSerializer<IAsnObjectCodec<byte>, byte>
    {
        public void Read(IAsnObjectCodec<byte> instance, Stream inputStream)
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);

            int value = inputStream.ReadByte();
            if (value == -1)
            {
                throw new CodecException("Unexpected end of stream.");
            }

            instance.Encode((byte)value);
        }

        public void Write(IAsnObjectCodec<byte> instance, Stream outputStream)
        {
            if (255 < instance.Decode())
            {
                throw new FormatException("UInt8 values may only contain up to 8 bits!");
            }

            outputStream.WriteByte((byte)instance.Decode());
        }
    }
}
