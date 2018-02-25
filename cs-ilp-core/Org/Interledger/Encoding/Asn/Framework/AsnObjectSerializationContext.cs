using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class AsnObjectSerializationContext
    {
        private readonly IDictionary<Type, object> serializers;

        public AsnObjectSerializationContext()
        {
            this.serializers = new ConcurrentDictionary<Type, object>();
        }

        public AsnObjectSerializationContext Register<T>(Type type, IAsnObjectSerializer<T> serializer) where T : IAsnObjectCodec<T>
        {
            Objects.RequireNonNull(type);
            Objects.RequireNonNull(serializer);

            this.serializers.Add(type, serializer);

            return this;
        }

        public AsnObjectSerializationContext Read<T>(T instance, Stream inputStream) where T:class, IAsnObjectCodec<T>
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);
            this.GetSerializer<T>(instance).Read(this, instance, inputStream);
            return this;
        }

        public AsnObjectSerializationContext Read<T>(T instance, byte[] data) where T: class, IAsnObjectCodec<T>
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(data);

            try
            {
                using (MemoryStream stream = new MemoryStream(data))
                {
                    this.Read(instance, stream);
                }
            }
            catch (IOException e)
            {
                throw new CodecException(string.Format("Unable to decode " + instance.GetType().FullName), e);
            }

            return this;
        }

        public AsnObjectSerializationContext Write<T>(T instance, Stream outputStream) where T: class, IAsnObjectCodec<T>
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(outputStream);
            this.GetSerializer<T>(instance).Write(this, instance, outputStream);
            return this;
        }


        public byte[] Write<T>(T instance) where T: class, IAsnObjectCodec<T>
        {
            Objects.RequireNonNull(instance);

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    this.GetSerializer<T>(instance).Write(this, instance, stream);
                    return stream.GetBuffer();
                }
            }
            catch (IOException e)
            {
                throw new CodecException(string.Format("Error encoding " + instance.GetType().FullName), e);
            }
        }

        internal IAsnObjectSerializer<T> GetSerializer<T>(T instance) where T:IAsnObjectCodec<T>
        {
            Type type = instance.GetType();
            IAsnObjectSerializer<T> serializer = TryGetSerializerForCodec<T>(type);

            if (serializer == null)
            {
                throw new CodecException(
                    string.Format("No serializer registered for {0} or its super classes!",type)
                );
            }
            return serializer;
        }

        private IAsnObjectSerializer<T> TryGetSerializerForCodec<T>(Type type) where T : IAsnObjectCodec<T>
        {
            Objects.RequireNonNull(type);

            IAsnObjectSerializer<T> serializer;

            if (serializers.ContainsKey(type))
            {
                serializer = (IAsnObjectSerializer<T>)this.serializers[type];
                if (serializer != null)
                {
                    return serializer;
                }
            }

            if (type.BaseType != null)
            {
                serializer = this.TryGetSerializerForCodec<T>(type.BaseType);
                if (serializer != null)
                {
                    return serializer;
                }
            }

            foreach (Type interfaceType in type.GetInterfaces())
            {
                serializer = this.TryGetSerializerForCodec<T>(interfaceType);
                if (serializer != null)
                {
                    return serializer;
                }
            }

            return null;
        }
    }
}
