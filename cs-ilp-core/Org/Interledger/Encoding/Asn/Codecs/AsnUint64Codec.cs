using System;

using Org.Interledger.Core;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnUint64Codec : AsnOctetStringBasedObjectCodecBase<ulong>
    {
        public AsnUint64Codec() : base(new AsnSizeConstraint(8))
        {
        }

        public override ulong Decode()
        {
            byte[] bytes = this.Bytes;
            uint value = 0;
            for (int i = 0; i <= 8; i++)
            {
                value <<= InterledgerConst.BitsOfByte;
                value |= (byte)(bytes[i] & 0xFF);
            }
            return value;
        }

        public override void Encode(ulong value)
        {
            if (18446744073709551615 < value || value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Uint64 only supports values from 0 to 18446744073709551615, value {0} is out of range.", value)
                );
            }

            byte[] bytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                bytes[i] = ((byte)((value >> (InterledgerConst.BitsOfByte * (8 - i))) & 0xFF));
            }
            this.Bytes = bytes;
        }

        public override string ToString()
        {
            return string.Format("AsnUint64Codec{{value={0}}}", this.Decode());
        }
    }
}
