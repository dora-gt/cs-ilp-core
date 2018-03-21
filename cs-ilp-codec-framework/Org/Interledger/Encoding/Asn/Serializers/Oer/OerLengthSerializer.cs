using System;
using System.IO;
using System.Collections.Generic;

using Org.Interledger.Encoding.Asn.Framework;

using static Org.Interledger.Core.InterledgerConst;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class OerLengthSerializer
    {
        // in [byte]
        public static readonly int LengthOfLength = 8;

        /// <summary>
        /// xx bytes -> max value
        /// </summary>
        private static readonly IDictionary<int, ulong> bytesMaxMap = new Dictionary<int, ulong>() {
            { 1, 255 },
            { 2, 65535 },
            { 3, 16777215 },
            { 4, 4294967295 },
            { 5, 1099511627775 },
            { 6, 281474976710655 },
            { 7, 72057594037927935 },
            { 8, 18446744073709551615 },
        };

        public static ulong ReadLength(Stream inputStream)
        {
            Objects.RequireNonNull(inputStream);
            // The number of encoded octets that the encoded payload will be stored in.
            ulong length;
            int initialLengthPrefixOctet = inputStream.ReadByte();
            if (initialLengthPrefixOctet >= 0 && initialLengthPrefixOctet < 128)
            {
                length = (ulong)initialLengthPrefixOctet;
            }
            else
            {
                // Truncate the MSB and use the rest as a number...
                int lengthOfLength = initialLengthPrefixOctet & 0x7f;

                // Convert the bytes into an integer...
                byte[] ba = new byte[lengthOfLength];
                if (LengthOfLength < lengthOfLength)
                {
                    throw new IOException(
                        string.Format("length of length must be equal to or less than {0}! length of length={1}", LengthOfLength, lengthOfLength)
                    );
                }
                if (lengthOfLength == 0)
                {
                    throw new IOException(string.Format("length of length must be more than 0!"));
                }
                int read = inputStream.Read(ba, 0, lengthOfLength);
                if (read != lengthOfLength)
                {
                    throw new IOException(
                        string.Format("error reading {0} bytes from stream, only read {1}", lengthOfLength, read)
                    );
                }
                length = ToUlong(ba);
            }

            return length;
        }

        private static ulong ToUlong(byte[] bytes)
        {
            // because length of bytes is not always 8.
            // e.g. 3 bytes, it must be 8 bytes to be converted to ulong.
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
                if (bytes.Length < 8)
                {
                    byte[] enough = new byte[8];
                    Array.Copy(bytes, enough, bytes.Length);
                    bytes = enough;
                }
            }
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static void WriteLength(ulong length, Stream outputStream)
        {
            Objects.RequireNonNull(outputStream);

            if (length >= 0 && length < 128)
            {
                // Write a single byte that contains the length (it will start with a 0, and not exceed
                // 127 in Base10.
                outputStream.WriteByte((byte)length);
            }
            else
            {
                for (int bytes = 1; bytes <= LengthOfLength; bytes++)
                {
                    if (length <= bytesMaxMap[bytes])
                    {
                        // Write length of length
                        outputStream.WriteByte((byte)(128 + bytes));

                        // Write length bytes
                        for (int index = 0; index < bytes; index ++)
                        {
                            outputStream.WriteByte((byte)(length >> (BitsOfByte * (bytes - 1 - index))));
                        }
                        return;
                    }
                }
                throw new Exception("Impossible, more than uint max!");
            }
        }
    }
}
