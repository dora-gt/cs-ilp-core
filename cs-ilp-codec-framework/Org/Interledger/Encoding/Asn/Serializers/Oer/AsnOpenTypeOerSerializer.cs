using System;
using System.IO;
using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnOpenTypeOerSerializer : IAsnObjectSerializer<IAsnOpenTypeCodec>
    {
        public void Read(AsnObjectSerializationContext context, IAsnOpenTypeCodec instance, Stream inputStream)
        {
            Objects.RequireNonNull(context);
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);

            //Get length
            ulong length = OerLengthSerializer.ReadLength(inputStream);
            if (int.MaxValue < length)
            {
                throw new IOException(string.Format("Can't handle more than {0} bytes! length={1}", int.MaxValue, length));
            }

            int handlableLength = (int)length;

            //Read in correct number of bytes
            byte[] buffer = new byte[handlableLength];
            int bytesRead = inputStream.Read(buffer, 0, handlableLength);

            if (bytesRead != handlableLength)
            {
                throw new IOException(string.Format("Unexpected end of stream. Expected {0} bytes but got {1}.", handlableLength, bytesRead));
            }
            context.Read(instance.GetInnerCodec(), buffer);
        }

        public void Write(AsnObjectSerializationContext context, IAsnOpenTypeCodec instance, Stream outputStream)
        {
            Objects.RequireNonNull(context);
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(outputStream);

            //Serialize the inner Asn.1 object
            byte[] bytes = context.Write(instance.GetInnerCodec());

            //Write a length prefix
            OerLengthSerializer.WriteLength((ulong)bytes.Length, outputStream);

            //Write the object
            if (bytes.Length > 0)
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
