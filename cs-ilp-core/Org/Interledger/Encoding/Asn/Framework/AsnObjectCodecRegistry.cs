using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class AsnObjectCodecRegistry
    {
        private readonly IDictionary<Type, object> mappersByObjectType;

        public AsnObjectCodecRegistry()
        {
            this.mappersByObjectType = new ConcurrentDictionary<Type, object>();
        }

        public AsnObjectCodecRegistry Register<T>(IAsnObjectCodecSupplier<T> supplier)
        {
            Objects.RequireNonNull<IAsnObjectCodecSupplier<T>>(supplier);

            this.mappersByObjectType.Add(typeof(T), supplier);

            return this;
        }

        public IAsnObjectCodec<T> GetAsnObjectForType<T>() where T : IAsnObjectCodecSupplier<T>
        {
            Type type = typeof(T);
            IAsnObjectCodec<T> codec = this.TryGetAsnObjectForType<T>(type);
            if (codec == null)
            {
                throw new CodecException(
                    string.Format("No codec registered for {0} or its super classes!", type)
                );
            }
            return codec;
        }

        private IAsnObjectCodec<T> TryGetAsnObjectForType<T>(Type type) where T : IAsnObjectCodecSupplier<T>
        {
            Objects.RequireNonNull<Type>(type);

            IAsnObjectCodec<T> codec;

            if (mappersByObjectType.ContainsKey(type))
            {
                codec = ((T)mappersByObjectType[type]).Get();
                if (codec != null)
                {
                    return codec;
                }
            }

            if (type.BaseType != null)
            {
                codec = (IAsnObjectCodec<T>)TryGetAsnObjectForType<T>(type.BaseType);
                if (codec != null)
                {
                    return codec;
                }
            }

            foreach (Type interfaceType in type.GetInterfaces())
            {
                codec = (IAsnObjectCodec<T>)TryGetAsnObjectForType<T>(interfaceType);
                if (codec != null)
                {
                    return codec;
                }
            }

            return null;
        }
    }
}
