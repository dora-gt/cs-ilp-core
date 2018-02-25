using System;
namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectCodec<T>
    {
        T Decode();

        void Encode(T value);
    }
}
