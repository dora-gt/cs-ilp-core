using System;
namespace Org.Interledger
{
    public enum Endianness
    {
        LittleEndian,
        BigEndian
    }

    public static class ByteUtils
    {
        public static Endianness Endianness
        {
            get
            {
                return BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
            }
        }

        public static ulong ToUlong(byte[] bytes, Endianness fromEndianness)
        {
            return BitConverter.ToUInt64(ToBytes(bytes, 8, fromEndianness, Endianness), 0);
        }

        public static ulong ToUint(byte[] bytes, Endianness fromEndianness)
        {
            return BitConverter.ToUInt32(ToBytes(bytes, 4, fromEndianness, Endianness), 0);
        }

        private static byte[] ToBytes(byte[] bytes, byte destLength, Endianness fromEndianness, Endianness destEndianness)
        {
            if (destLength < bytes.Length)
            {
                throw new ArgumentException("Length of bytes is more than destLength!");
            }
            byte[] destLengthBytes = new byte[destLength];
            if (bytes.Length < destLength)
            {
                if (fromEndianness == Endianness.BigEndian)
                {
                    Array.Copy(bytes, 0, destLengthBytes, destLength - bytes.Length, bytes.Length);
                }
                else if (fromEndianness == Endianness.LittleEndian)
                {
                    Array.Copy(bytes, 0, destLengthBytes, 0, bytes.Length);
                }
                else
                {
                    throw new Exception("Impossible byte order!");
                }
            }
            else
            {
                Array.Copy(bytes, 0, destLengthBytes, 0, bytes.Length);
            }
            if (fromEndianness != destEndianness)
            {
                Array.Reverse(destLengthBytes);
            }
            return destLengthBytes;
        }
    }
}
