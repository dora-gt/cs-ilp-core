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
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
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
