using System;
using System.Linq;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnOctetStringBasedObjectCodecBase<T> : AsnPrimitiveCodecBase<T>
    {
        private byte[] _bytes;

        public byte[] Bytes
        {
            get
            {
                return this._bytes;
            }
            set
            {
                Objects.RequireNonNull(value);
                this.ValidateSize(value);
                this._bytes = value;
            }
        }

        public AsnOctetStringBasedObjectCodecBase(AsnSizeConstraint sizeConstraintInOctets) : base (sizeConstraintInOctets)
        {
        }

        public AsnOctetStringBasedObjectCodecBase(int fixedSizeConstraint) : base(fixedSizeConstraint)
        {
        }

        public AsnOctetStringBasedObjectCodecBase(int minSize, int maxSize) : base(minSize, maxSize)
        {
        }

        private void ValidateSize(byte[] bytes)
        {
            if (this.SizeConstraint.IsUnconstrained)
            {
                return;
            }

            if (this.SizeConstraint.IsFixedSize)
            {
                if (bytes.Length != this.SizeConstraint.Max)
                {
                    throw new CodecException(
                        string.Format("Invalid octet string length. Expected {0}, got {1}", this.SizeConstraint.Max, bytes.Length)
                    );
                }
            }
            else
            {
                if (bytes.Length < this.SizeConstraint.Min)
                {
                    throw new CodecException(
                        string.Format("Invalid octet string length. Expected > {0}, got {1}", this.SizeConstraint.Min, bytes.Length)
                    );
                }
                if (this.SizeConstraint.Max < bytes.Length)
                {
                    throw new CodecException(
                        string.Format("Invalid octet string length. Expected < {0}, got {1}", this.SizeConstraint.Max, bytes.Length)
                    );
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            AsnOctetStringBasedObjectCodecBase<T> that = (AsnOctetStringBasedObjectCodecBase<T>)obj;

            return this.Bytes.SequenceEqual(that.Bytes);
        }

        public override int GetHashCode()
        {
            return this.Bytes.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("AsnOctetStringBasedObjectCodec{{bytes={0}}}", string.Concat(this.Bytes, ", "));
        }
    }
}
