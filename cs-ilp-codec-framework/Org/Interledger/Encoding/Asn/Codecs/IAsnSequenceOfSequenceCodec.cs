using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public interface IAsnSequenceOfSequenceCodec
    {
        int Size { get; set; }

        dynamic GetCodecAt(int index);
    }
}
