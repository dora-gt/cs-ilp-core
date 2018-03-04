using System;

using Org.Interledger.Core;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnUint32Codec : AsnOctetStringBasedObjectCodecBase<uint>
    {
        public AsnUint32Codec() : base(new AsnSizeConstraint(4))
        {
        }

        public override uint Decode()
        {
            byte[] bytes = this.Bytes;
            uint value = 0;
            for (int i = 0; i < 4; i++)
            {
                value <<= InterledgerConst.BitsOfByte;
                value |= (byte)(bytes[i] & 0xFF);
            }
            return value;
        }

        public override void Encode(uint value)
        {
            if (4294967295L < value || value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Uint32 only supports values from 0 to 4294967295, value {0} is out of range.", value)
                );
            }

            byte[] bytes = new byte[4];
            for (int i = 0; i <= 3; i++)
            {
                bytes[i] = ((byte)((value >> (InterledgerConst.BitsOfByte * (3 - i))) & 0xFF));
            }
            this.Bytes = bytes;
        }

        public override void Accept(IAsnObjectCodecVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return string.Format("AsnUint32Codec{{value={0}}}", this.Decode());
        }
    }
}
