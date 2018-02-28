using System;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectSerializer<T, U> where T: IAsnObjectCodec<U>
    {
        void Read(T instance, Stream inputStream);

        void Write(T instance, Stream outputStream);
    }
}
