using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class AsnObjectSerializationContext
    {
        // object = IAsnObjectSerializer
        private readonly IDictionary<Type, object> serializers;

        public AsnObjectSerializationContext()
        {
            this.serializers = new ConcurrentDictionary<Type, object>();
        }

        public AsnObjectSerializationContext Register<T, U>(Type type, IAsnObjectSerializer<T, U> serializer) where T : IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(type);
            Objects.RequireNonNull(serializer);

            this.serializers.Add(type, serializer);

            return this;
        }

        public AsnObjectSerializationContext Read<T, U>(T instance, Stream inputStream) where T:class, IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);
            this.GetSerializer<T, U>(instance).Read(this, instance, inputStream);
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
            this.GetSerializer<T, U>(instance).Write(this, instance, outputStream);
            return this;
        }

        public byte[] Write<T, U>(T instance) where T: class, IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(instance);

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    this.GetSerializer<T, U>(instance).Write(this, instance, stream);
                    return stream.GetBuffer();
                }
            }
            catch (IOException e)
            {
                throw new CodecException(string.Format("Error encoding " + instance.GetType().FullName), e);
            }
        }

        internal IAsnObjectSerializer<T, U> GetSerializer<T, U>(T instance) where T:IAsnObjectCodec<U>
        {
            Type type = instance.GetType();
            IAsnObjectSerializer<T, U> serializer = TryGetSerializerForCodec<T, U>(type);

            if (serializer == null)
            {
                throw new CodecException(
                    string.Format("No serializer registered for {0} or its super classes!",type)
                );
            }
            return serializer;
        }

        private IAsnObjectSerializer<T, U> TryGetSerializerForCodec<T, U>(Type type) where T : IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(type);

            IAsnObjectSerializer<T, U> serializer;

            if (serializers.ContainsKey(type))
            {
                serializer = (IAsnObjectSerializer<T, U>)this.serializers[type];
                if (serializer != null)
                {
                    return serializer;
                }
            }

            if (type.BaseType != null)
            {
                serializer = this.TryGetSerializerForCodec<T, U>(type.BaseType);
                if (serializer != null)
                {
                    return serializer;
                }
            }

            foreach (Type interfaceType in type.GetInterfaces())
            {
                serializer = this.TryGetSerializerForCodec<T, U>(interfaceType);
                if (serializer != null)
                {
                    return serializer;
                }
            }

            return null;
        }
    }
}
