using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public interface IAsnSequenceCodec
    {
        int Size { get; }

        dynamic GetCodecAt(int index);
    }
}
