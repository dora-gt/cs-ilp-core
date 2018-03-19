using System;
namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface ISupplier<T>
    {
        T Get();
    }
}
