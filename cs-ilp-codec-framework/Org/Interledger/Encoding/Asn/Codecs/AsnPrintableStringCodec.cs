using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnPrintableStringCodec : AsnPrintableStringBasedObjectCodecBase<String>
    {
        public AsnPrintableStringCodec(AsnSizeConstraint sizeConstraint) : base(sizeConstraint)
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
            return string.Format("PrintableString{{value={0}}}", this.Decode());
        }
    }
}
