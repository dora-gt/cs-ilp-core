using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class AsnObjectCodecRegistry
    {
        // object = IAsnObjectCodecSupplier<T>
        private readonly IDictionary<Type, object> _mappersByObjectType;

        public AsnObjectCodecRegistry()
        {
            this._mappersByObjectType = new ConcurrentDictionary<Type, object>();
        }

        public AsnObjectCodecRegistry Register<T>(IAsnObjectCodecSupplier<T> supplier)
        {
            Objects.RequireNonNull(supplier);

            this._mappersByObjectType.Add(typeof(T), supplier);

            return this;
        }

        public IAsnObjectCodec<T> GetAsnObjectForType<T>()
        {
            IAsnObjectCodec<T> codec = this.TryGetAsnObjectForType<T>(typeof(T));
            if (codec == null)
            {
                throw new CodecException(
                    string.Format("No codec registered for {0} or its super classes!", typeof(T))
                );
            }
            return codec;
        }

        private IAsnObjectCodec<T> TryGetAsnObjectForType<T>(Type type)
        {
            Objects.RequireNonNull(type);
            IAsnObjectCodec<T> codec;

            if (this._mappersByObjectType.ContainsKey(type))
            {
                codec = ((IAsnObjectCodecSupplier<T>)this._mappersByObjectType[type]).Get();
                if (codec != null)
                {
                    return codec;
                }
            }

            if (type.BaseType != null)
            {
                codec = TryGetAsnObjectForType<T>(type.BaseType);
                if (codec != null)
                {
                    return codec;
                }
            }

            foreach (Type interfaceType in type.GetInterfaces())
            {
                codec = TryGetAsnObjectForType<T>(interfaceType);
                if (codec != null)
                {
                    return codec;
                }
            }

            return null;
        }
    }
}
