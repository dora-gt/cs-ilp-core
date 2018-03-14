using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnTimestampCodec : AsnPrintableStringBasedObjectCodecBase<DateTime>
    {
        private string DateTimeFormatter { get { return "yyyyMMddHHmmssfff"; } }

        public AsnTimestampCodec() : base(new AsnSizeConstraint(17))
        {
            Regex regex = new Regex(this.GetRegex());
            this.Validator = (string charString) => regex.IsMatch(charString);
        }
            
        public override DateTime Decode()
        {
            DateTime dateTime;
            DateTime.TryParseExact(this.CharString, this.DateTimeFormatter, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out dateTime);
            if (dateTime != null)
            {
                return dateTime;
            }
            else
            {
                throw new FormatException(string.Format(
                    "Interledger timestamps only support values in the format '{0}', value {1} is invalid.", this.DateTimeFormatter, this.CharString
                ));
            }
        }

        public override void Encode(DateTime value)
        {
            this.CharString = value.ToString(this.DateTimeFormatter);
        }

        private string GetRegex()
        {
            return "[0-9]{17}";
        }
    }
}
