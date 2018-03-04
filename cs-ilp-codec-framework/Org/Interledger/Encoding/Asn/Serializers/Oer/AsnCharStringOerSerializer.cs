using System;
using System.IO;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;


namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnCharStringOerSerializer : IAsnObjectSerializer<AsnCharStringBasedObjectCodecBase<string>, string>
    {
        public void Read(AsnCharStringBasedObjectCodecBase<string> instance, Stream inputStream)
        {
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
                length = (int)OerLengthSerializer.ReadLength(inputStream);
            }

            /* beware the 0-length string */
            string result = (length == 0 ? "" : this.GetString(inputStream, length, instance.Encoding));
            instance.CharString = result;
        }

        public void Write(AsnCharStringBasedObjectCodecBase<string> instance, Stream outputStream)
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(outputStream);

            byte[] data = instance.Encoding.GetBytes(instance.CharString);
            AsnSizeConstraint sizeConstraint = instance.SizeConstraint;

            if (!sizeConstraint.IsFixedSize)
            {
                // Write the octet length of the string...
                OerLengthSerializer.WriteLength((ulong)data.Length, outputStream);
            }

            // Write the String bytes to the buffer.
            outputStream.Write(data, 0, data.Length);
        }

        private string GetString(Stream inputStream, int lengthToRead, System.Text.Encoding charset)
        {
            Objects.RequireNonNull(inputStream);

            // Read lengthToRead bytes from the inputStream into the buffer...
            byte[] buffer = new byte[lengthToRead];
            int read = inputStream.Read(buffer, 0, lengthToRead);

            if (read != lengthToRead)
            {
                throw new IOException(string.Format("error reading {0} bytes from stream, only read {1}", lengthToRead, read));
            }

            return charset.GetString(buffer, 0, lengthToRead);
        }
    }
}
