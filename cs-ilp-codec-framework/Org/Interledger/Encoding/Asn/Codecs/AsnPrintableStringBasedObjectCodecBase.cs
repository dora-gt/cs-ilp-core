using System;
using System.Text.RegularExpressions;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnPrintableStringBasedObjectCodecBase<T> : AsnCharStringBasedObjectCodecBase<T>
    {
        public AsnPrintableStringBasedObjectCodecBase(AsnSizeConstraint sizeConstraint) : base(sizeConstraint, System.Text.Encoding.GetEncoding("us-ascii"))
        {
            Regex regex = new Regex(this.GetRegex());
            this.Validator = (string charString) => regex.IsMatch(charString);
        }

        private string GetRegex()
        {
            return "[a-zA-Z0-9'()+,-.?:/= ]+";
        }
    }
}
