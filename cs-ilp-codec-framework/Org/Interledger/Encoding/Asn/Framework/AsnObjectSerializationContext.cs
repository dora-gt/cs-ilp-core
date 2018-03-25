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
        private readonly IDictionary<Type, object> _serializers;

        public AsnObjectSerializationContext()
        {
            this._serializers = new ConcurrentDictionary<Type, object>();
        }

        public AsnObjectSerializationContext Register<T>(Type type, IAsnObjectSerializer<T> serializer)
        {
            Objects.RequireNonNull(type);
            Objects.RequireNonNull(serializer);

            Type serializerInterface = serializer.GetType().GetInterface(typeof(IAsnObjectSerializer<>));
            if (serializerInterface == null)
            {
                throw new Exception(string.Format("serializer must implement IAsnObjectSerializer! serializer: {0}", serializer));
            }

            this._serializers.Add(type, serializer);

            return this;
        }

        public AsnObjectSerializationContext Read(dynamic instance, Stream inputStream)
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(inputStream);
            this.GetSerializer(instance).Read(this, instance, inputStream);
            return this;
        }

        public AsnObjectSerializationContext Read(dynamic instance, byte[] data)
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

        public AsnObjectSerializationContext Write(dynamic instance, Stream outputStream)
        {
            Objects.RequireNonNull(instance);
            Objects.RequireNonNull(outputStream);
            this.GetSerializer(instance).Write(this, instance, outputStream);
            return this;
        }

        public byte[] Write(dynamic instance)
        {
            Objects.RequireNonNull(instance);

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    this.GetSerializer(instance).Write(this, instance, stream);
                    return stream.GetBuffer();
                }
            }
            catch (IOException e)
            {
                throw new CodecException(string.Format("Error encoding " + instance.GetType().FullName), e);
            }
        }

        /// <summary>
        /// returns IAsnObjectSerializer&lt;T, U&gt;
        /// </summary>
        /// <param name="instance">IAsnObjectCodec&lt;T&gt;</param>
        internal dynamic GetSerializer(dynamic instance)
        {
            Type type = instance.GetType();
            dynamic serializer = TryGetSerializerForCodec(type);
            if (serializer == null)
            {
                throw new CodecException(
                    string.Format("No serializer registered for {0} or its super classes!", type)
                );
            }
            return serializer;
        }


        /// <summary>
        /// returns IAsnObjectSerializer&lt;T, U&gt;
        /// </summary>
        private dynamic TryGetSerializerForCodec(Type type)
        {
            Objects.RequireNonNull(type);

            dynamic serializer = null;

            if (this._serializers.ContainsKey(type))
            {
                Console.WriteLine(string.Format("type: {0}", this._serializers[type].GetType()));
                return this._serializers[type];
            }

            if (type.BaseType != null)
            {
                serializer = this.TryGetSerializerForCodec(type.BaseType);
                if (serializer != null)
                {
                    return serializer;
                }

                if (type.BaseType.IsGenericType)
                {
                    serializer = this.TryGetSerializerForCodec(type.BaseType.GetGenericTypeDefinition());
                    if (serializer != null)
                    {
                        return serializer;
                    }
                }
            }

            foreach (Type interfaceType in type.GetInterfaces())
            {
                serializer = this.TryGetSerializerForCodec(interfaceType);
                if (serializer != null)
                {
                    return serializer;
                }

                if (interfaceType.IsGenericType)
                {
                    serializer = this.TryGetSerializerForCodec(interfaceType.GetGenericTypeDefinition());
                    if (serializer != null)
                    {
                        return serializer;
                    }
                }
            }

            return null;
        }
    }
}


