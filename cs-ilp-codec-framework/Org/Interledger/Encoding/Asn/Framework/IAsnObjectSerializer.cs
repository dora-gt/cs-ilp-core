using System;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public interface IAsnObjectSerializer<T>
    {
        void Read(T instance, Stream inputStream);

        void Write(T instance, Stream outputStream);
    }
}
