using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnIA5StringBasedObjectCodecBase<T> : AsnCharStringBasedObjectCodecBase<T>
    {
        public AsnIA5StringBasedObjectCodecBase(AsnSizeConstraint sizeConstraint) : base(sizeConstraint, System.Text.Encoding.GetEncoding("us-ascii"))
        {
        }
    }
}
