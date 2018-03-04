using System;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnPrimitiveCodecBase<T> : AsnObjectCodecBase<T>
    {
        public AsnSizeConstraint SizeConstraint { get; private set; }

        public AsnPrimitiveCodecBase(AsnSizeConstraint sizeConstraint)
        {
            this.SizeConstraint = sizeConstraint;
        }

        public AsnPrimitiveCodecBase(int fixedSizeConstraint)
        {
            this.SizeConstraint = new AsnSizeConstraint(fixedSizeConstraint);
        }

        public AsnPrimitiveCodecBase(int minSize, int maxSize)
        {
            this.SizeConstraint = new AsnSizeConstraint(minSize, maxSize);
        }
    }
}
