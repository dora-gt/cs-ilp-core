using System;
namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectCodecSupplier<T>
    {
        IAsnObjectCodec<T> Get();
    }
}
