using System;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectSerializer<T>
    {
        void Read(AsnObjectSerializationContext context, T instance, Stream inputStream);

        void Write(AsnObjectSerializationContext context, T instance, Stream outputStream);
    }
}
