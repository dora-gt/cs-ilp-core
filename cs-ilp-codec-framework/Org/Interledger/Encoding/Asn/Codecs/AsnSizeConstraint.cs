using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnSizeConstraint
    {
        public static readonly AsnSizeConstraint UNCONSTRAINED = new AsnSizeConstraint(0, 0);

        public int Min { get; private set; }

        public int Max { get; private set; }

        public bool IsUnconstrained
        {
            get
            {
                return this.Max == 0 && this.Min == 0;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return this.Max != 0 && this.Max == this.Min;
            }
        }

        public AsnSizeConstraint(int fixedSize)
        {
            this.Min = fixedSize;
            this.Max = fixedSize;
        }

        public AsnSizeConstraint(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
