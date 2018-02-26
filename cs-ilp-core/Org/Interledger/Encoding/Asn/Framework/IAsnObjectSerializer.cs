using System;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectSerializer<T, U> where T: IAsnObjectCodec<U>
    {
        void Read(AsnObjectSerializationContext context, T instance, Stream inputStream);

        void Write(AsnObjectSerializationContext context, T instance, Stream outputStream);
    }
}
