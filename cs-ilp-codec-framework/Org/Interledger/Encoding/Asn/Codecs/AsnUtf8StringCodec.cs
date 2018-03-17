using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnUtf8StringCodec : AsnUtf8StringBasedObjectCodecBase<string>
    {
        public AsnUtf8StringCodec(AsnSizeConstraint sizeConstraint) : base(sizeConstraint)
        {
        }

        public AsnUtf8StringCodec(int fixedSizeConstraint) : base(fixedSizeConstraint)
        {
        }

        public AsnUtf8StringCodec(int minSize, int maxSize) : base (minSize, maxSize)
        {
        }

        public override string Decode()
        {
            return this.CharString;
        }

        public override void Encode(string value)
        {
            this.CharString = value;
        }

        public override string ToString()
        {
            return string.Format("AsnUtf8StringCodec{{value={0}}}", this.Decode());
        }
    }
}
