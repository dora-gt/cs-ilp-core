using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnIA5StringCodec : AsnIA5StringBasedObjectCodecBase<String>
    {
        public AsnIA5StringCodec(AsnSizeConstraint sizeConstraint) : base(sizeConstraint)
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
            return string.Format("IA5String{{value={0}}}", this.Decode());
        }
    }
}
