using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public interface IAsnOctetStringBasedObjectCodec
    {
        AsnSizeConstraint SizeConstraint { get; }

        byte[] Bytes { get; set; }
    }
}
