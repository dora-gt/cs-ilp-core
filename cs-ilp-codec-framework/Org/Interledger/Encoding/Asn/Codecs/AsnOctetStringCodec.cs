using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnOctetStringCodec : AsnOctetStringBasedObjectCodecBase<byte[]>
    {

        public AsnOctetStringCodec(AsnSizeConstraint sizeConstraint) : base (sizeConstraint)
        {
        }

        public AsnOctetStringCodec(int fixedSizeConstraint) : base (fixedSizeConstraint)
        {
        }

        public AsnOctetStringCodec(int minSize, int maxSize) : base (minSize, maxSize)
        {
        }

        public override byte[] Decode()
        {
            return this.Bytes;
        }

        public override void Encode(byte[] value)
        {
            this.Bytes = value;
        }
    }
}
