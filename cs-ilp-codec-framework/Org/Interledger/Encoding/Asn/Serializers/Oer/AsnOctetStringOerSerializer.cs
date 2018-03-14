using System;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnOctetStringOerSerializer<T> : IAsnObjectSerializer<AsnOctetStringBasedObjectCodecBase<T>>
    {
        public void Read(AsnOctetStringBasedObjectCodecBase<T> instance, Stream inputStream)
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);

            ulong length;
            AsnSizeConstraint sizeConstraint = instance.SizeConstraint;
            if (sizeConstraint.IsFixedSize)
            {
                length = (ulong)sizeConstraint.Max;
            }
            else
            {
                // Read the length of the encoded OctetString...
                length = OerLengthSerializer.ReadLength(inputStream);
            }

            if (int.MaxValue < length)
            {
                throw new IOException(string.Format("Can't handle more than {0} bytes! length={1}", int.MaxValue, length));
            }

            int handlableLength = (int)length;
            byte[] returnable = new byte[handlableLength];

            if (length == 0)
            {
                instance.Bytes = returnable;
            }
            else
            {
                int bytesRead = inputStream.Read(returnable, 0, handlableLength);
                if (bytesRead < handlableLength)
                {
                    throw new CodecException(
                        string.Format("Unexpected end of stream. Expected {0} bytes but only read {1}.", handlableLength, bytesRead)
                    );
                }
                instance.Bytes = returnable;
            }
        }

        public void Write(AsnOctetStringBasedObjectCodecBase<T> instance, Stream outputStream)
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(outputStream);

            byte[] bytes = instance.Bytes;
            AsnSizeConstraint sizeConstraint = instance.SizeConstraint;
            if (!sizeConstraint.IsFixedSize)
            {
                // Write the length of the encoded OctetString...
                OerLengthSerializer.WriteLength((ulong)bytes.Length, outputStream);
            }

            // Write the OctetString bytes to the buffer.
            if (0 < bytes.Length)
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
