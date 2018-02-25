using System;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectSerializer<T> where T:IAsnObjectCodec<T>
    {
        void Read(AsnObjectSerializationContext context, T instance, Stream stream);

        void Write(AsnObjectSerializationContext context, T instance, Stream stream);
    }
}
