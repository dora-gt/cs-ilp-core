using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public interface IAsnObjectCodecVisitor
    {
        void Visit(AsnUint8Codec asnUint8Codec);

        void Visit(AsnUint32Codec asnUint32Codec);

        void Visit(AsnUint64Codec asnUint64Codec);

        void Visit(AsnOctetStringCodec asnOctetStringCodec);

        void Visit(AsnPrintableStringCodec asnPrintableStringCodec);
    }
}
