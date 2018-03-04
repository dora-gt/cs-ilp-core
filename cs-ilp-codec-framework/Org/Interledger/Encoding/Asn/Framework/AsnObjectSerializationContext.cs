using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

using Org.Interledger.Encoding.Asn.Codecs;
using Org.Interledger.Encoding.Asn.Serializers;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class AsnObjectSerializationContext
    {
        public IAsnObjectCodecReader Reader { get; private set; }

        public IAsnObjectCodecWriter Writer { get; private set; }

        public AsnObjectSerializationContext(IAsnObjectCodecReader reader, IAsnObjectCodecWriter writer)
        {
            this.Reader = reader;
            this.Writer = writer;
        }

        public AsnObjectSerializationContext Read<T, U>(T instance, Stream inputStream) where T:class, IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);

            this.Reader.Stream = inputStream;
            instance.Accept(this.Reader);
            this.Reader.Stream = null;

            return this;
        }

        public AsnObjectSerializationContext Read<T, U>(T instance, byte[] data) where T: class, IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(data);

            try
            {
                using (MemoryStream stream = new MemoryStream(data))
                {
                    this.Read<T, U>(instance, stream);
                }
            }
            catch (IOException e)
            {
                throw new CodecException(string.Format("Unable to decode " + instance.GetType().FullName), e);
            }

            return this;
        }

        public AsnObjectSerializationContext Write<T, U>(T instance, Stream outputStream) where T: class, IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(outputStream);

            this.Writer.Stream = outputStream;
            instance.Accept(this.Writer);
            this.Writer.Stream = null;

            return this;
        }

        public byte[] Write<T, U>(T instance) where T: class, IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(instance);

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    this.Writer.Stream = stream;
                    instance.Accept(this.Writer);
                    this.Writer.Stream = null;
                    return stream.GetBuffer();
                }
            }
            catch (IOException e)
            {
                throw new CodecException(string.Format("Error encoding " + instance.GetType().FullName), e);
            }
        }
    }
}
