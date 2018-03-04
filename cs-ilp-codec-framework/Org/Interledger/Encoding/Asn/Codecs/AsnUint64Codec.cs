using System;

using Org.Interledger.Core;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnUint64Codec : AsnOctetStringBasedObjectCodecBase<ulong>
    {
        public AsnUint64Codec() : base(new AsnSizeConstraint(8))
        {
        }

        public override void Accept(IAsnObjectCodecVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override ulong Decode()
        {
            byte[] bytes = this.Bytes;
            ulong value = 0;
            for (int i = 0; i < 8; i++)
            {
                value <<= InterledgerConst.BitsOfByte;
                value |= (byte)(bytes[i] & 0xFF);
            }
            return value;
        }

        public override void Encode(ulong value)
        {
            byte[] bytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                bytes[i] = ((byte)((value >> (InterledgerConst.BitsOfByte * (7 - i))) & 0xFF));
            }
            this.Bytes = bytes;
        }

        public override string ToString()
        {
            return string.Format("AsnUint64Codec{{value={0}}}", this.Decode());
        }
    }
}
