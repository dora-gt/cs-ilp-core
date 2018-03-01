using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnUtf8StringBasedObjectCodecBase<T> : AsnCharStringBasedObjectCodecBase<T>
    {
        public AsnUtf8StringBasedObjectCodecBase(AsnSizeConstraint sizeConstraint) : base(sizeConstraint, System.Text.Encoding.UTF8)
        {
        }

        public AsnUtf8StringBasedObjectCodecBase(int fixedSizeConstraint) : base(fixedSizeConstraint, System.Text.Encoding.UTF8)
        {
        }

        public AsnUtf8StringBasedObjectCodecBase(int minSize, int maxSize) : base(minSize, maxSize, System.Text.Encoding.UTF8)
        {
        }
    }
}
