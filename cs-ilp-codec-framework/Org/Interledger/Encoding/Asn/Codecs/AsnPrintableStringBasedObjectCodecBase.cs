using System;
using System.Text.RegularExpressions;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnPrintableStringBasedObjectCodecBase<T> : AsnCharStringBasedObjectCodecBase<T>
    {
        public AsnPrintableStringBasedObjectCodecBase(AsnSizeConstraint sizeConstraint) : base(sizeConstraint, System.Text.Encoding.GetEncoding("US_ASCII"))
        {
            Regex regex = new Regex(this.GetRegex());
            this.Validator = (string charString) => regex.IsMatch(charString);
        }

        private string GetRegex()
        {
            return "[\\p{Alnum}'()+,-.?:/= ]+";
        }
    }
}
