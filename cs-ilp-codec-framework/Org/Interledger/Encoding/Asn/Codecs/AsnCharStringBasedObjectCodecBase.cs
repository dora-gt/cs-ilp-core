using System;
using System.Text;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnCharStringBasedObjectCodecBase<T> : AsnPrimitiveCodecBase<T>
    {
        private string _charString;

        protected Predicate<String> Validator
        {
            get;
            set;
        }

        public String CharString
        {
            get
            {
                return this._charString;
            }
            set
            {
                Objects.RequireNonNull(value);
                this.ValidateSize(value);
                this.Validate(value);
                this._charString = value;
            }
        }

        public System.Text.Encoding Encoding
        {
            get;
            private set;
        }

        public AsnCharStringBasedObjectCodecBase(AsnSizeConstraint sizeConstraint, System.Text.Encoding encoding) : base(sizeConstraint)
        {
            this.Encoding = encoding;
        }

        public AsnCharStringBasedObjectCodecBase(int fixedSizeConstraint, System.Text.Encoding encoding) : this(new AsnSizeConstraint(fixedSizeConstraint), encoding)
        {
        }

        public AsnCharStringBasedObjectCodecBase(int minSize, int maxSize, System.Text.Encoding encoding) : this(new AsnSizeConstraint(minSize, maxSize), encoding)
        {
        }

        private void Validate(String value)
        {
            if (this.Validator != null)
            {
                if (!this.Validator(value))
                {
                    throw new CodecException(string.Format("Invalid format: {0}", value));
                }
            }
        }

        private void ValidateSize(String charString)
        {
            if (this.SizeConstraint.IsUnconstrained)
            {
                return;
            }

            if (this.SizeConstraint.IsFixedSize)
            {
                if (charString.Length != this.SizeConstraint.Max)
                {
                    throw new CodecException(
                        string.Format("Invalid character string length. Expected {0}, got {1}", this.SizeConstraint.Max, charString.Length)
                    );
                }
            }
            else
            {
                if (charString.Length < this.SizeConstraint.Min)
                {
                    throw new CodecException(
                        string.Format("Invalid character string length. Expected > {0}, got {1}", this.SizeConstraint.Min, charString.Length)
                    );
                }
                if (charString.Length > this.SizeConstraint.Max)
                {
                    throw new CodecException(
                        string.Format("Invalid character string length. Expected < {0}, got {1}", this.SizeConstraint.Max, charString.Length)
                    );
                }
            }
        }

        public override string ToString()
        {
            return string.Format("AsnCharStringBasedObjectCodec{{string={0}}}", this.CharString);
        }
    }
}
