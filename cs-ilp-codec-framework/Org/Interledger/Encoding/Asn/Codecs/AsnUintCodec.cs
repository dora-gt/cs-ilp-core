using System;
using System.Numerics;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnUintCodec : AsnOctetStringBasedObjectCodecBase<BigInteger>
    {
        public AsnUintCodec() : base(AsnSizeConstraint.UNCONSTRAINED)
        {
        }

        public override BigInteger Decode()
        {
            // convert big endians to little endians, adding leading 0x00 to define it's positive value.
            byte[] bytes = new byte[this.Bytes.Length + 1];
            Array.Copy(this.Bytes, 0, bytes, 1, this.Bytes.Length);
            bytes[0] = 0;
            Array.Reverse(bytes);

            return new BigInteger(bytes);
        }

        public override void Encode(BigInteger value)
        {
            if (value.CompareTo(BigInteger.Zero) < 0)
            {
                throw new ArgumentException("value must be positive or zero");
            }

            // bytes must be big endians while BigInteger in C# returns bytes in little endians format.
            byte[] bytes = value.ToByteArray();
            Array.Reverse(bytes);

            // BigInteger's toByteArray writes data in two's complement,
            // so positive values may have a leading 0x00 byte.
            if (bytes[0] == 0x00)
            {
                MemoryStream stream = new MemoryStream();
                stream.Write(bytes, 1, bytes.Length - 1);
                this.Bytes = stream.ToArray();
                return;
            }

            this.Bytes = bytes;
        }
    }
}
