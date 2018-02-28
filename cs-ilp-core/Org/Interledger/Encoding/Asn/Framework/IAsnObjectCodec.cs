using System;

using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectCodec<T>
    {
        T Decode();

        void Encode(T value);

        void Accept(IAsnObjectCodecVisitor visitor);
    }
}
