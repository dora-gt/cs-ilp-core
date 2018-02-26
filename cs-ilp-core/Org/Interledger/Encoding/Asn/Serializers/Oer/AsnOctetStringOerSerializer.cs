using System;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnOctetStringOerSerializer<T> : IAsnObjectSerializer<AsnOctetStringBasedObjectCodecBase<T>, T>
    {
        private static class OerLengthSerializer
        {
            public static int readLength(Stream stream)
            {
                throw new NotImplementedException();
            }

            public static void writeLength(int length, Stream stream)
            {
                throw new NotImplementedException();
            }
        }

        public void Read(AsnObjectSerializationContext context, AsnOctetStringBasedObjectCodecBase<T> instance, Stream inputStream)
        {
            Objects.RequireNonNull(context);
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);

            int length;
            AsnSizeConstraint sizeConstraint = instance.SizeConstraint;
            if (sizeConstraint.IsFixedSize)
            {
                length = sizeConstraint.Max;
            }
            else
            {
                // Read the length of the encoded OctetString...
                length = OerLengthSerializer.readLength(inputStream);
            }

            byte[] returnable = new byte[length];

            if (length == 0)
            {
                instance.Bytes = returnable;
            }
            else
            {
                int bytesRead = inputStream.Read(returnable, 0, length);
                if (bytesRead < length)
                {
                    throw new CodecException(
                        string.Format("Unexpected end of stream. Expected {0} bytes but only read {1}.", length, bytesRead)
                    );
                }
                instance.Bytes = returnable;
            }
        }

        public void Write(AsnObjectSerializationContext context, AsnOctetStringBasedObjectCodecBase<T> instance, Stream outputStream)
        {
            Objects.RequireNonNull(context);
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(outputStream);

            byte[] bytes = instance.Bytes;
            AsnSizeConstraint sizeConstraint = instance.SizeConstraint;
            if (!sizeConstraint.IsFixedSize)
            {
                // Write the length of the encoded OctetString...
                OerLengthSerializer.writeLength(bytes.Length, outputStream);
            }

            // Write the OctetString bytes to the buffer.
            if (0 < bytes.Length)
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
