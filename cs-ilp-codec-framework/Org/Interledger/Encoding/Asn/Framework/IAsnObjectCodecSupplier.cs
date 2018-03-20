using System;
namespace Org.Interledger.Encoding.Asn.Framework
{
    /// <summary>
    /// U を生成するための T 型の Codec を返すための Supplier
    /// </summary>
    public interface IAsnObjectCodecSupplier<T, U> where T:IAsnObjectCodec<U>
    {
        T Get();
    }
}
