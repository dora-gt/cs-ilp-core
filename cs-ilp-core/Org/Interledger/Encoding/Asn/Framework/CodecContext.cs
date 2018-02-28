using System;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class CodecContext
    {
        private readonly AsnObjectCodecRegistry _mappings;
        private readonly AsnObjectSerializationContext _serializers;

        public CodecContext(AsnObjectCodecRegistry mappings, AsnObjectSerializationContext serializers)
        {
            this._mappings = mappings;
            this._serializers = serializers;
        }

        public CodecContext Register<T, U>(IAsnObjectCodecSupplier<U> supplier) where T: IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(supplier);

            //Register the mapping
            this._mappings.Register(supplier);

            return this;
        }

        public CodecContext Register<T, U>(IAsnObjectCodecSupplier<U> supplier, IAsnObjectSerializer<T, U> serializer) where T : IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(supplier);
            Objects.RequireNonNull(serializer);

            //Register the mapping
            this._mappings.Register<U>(supplier);

            return this;
        }

        public T Read<T>(Stream inputStream)
        {
            IAsnObjectCodec<T> asnObjectCodec = this._mappings.GetAsnObjectForType<T>();
            this._serializers.Read<IAsnObjectCodec<T>, T>(asnObjectCodec, inputStream);
            return asnObjectCodec.Decode();
        }

        public void Write<T>(T instance, Stream outputStream)
        {
            IAsnObjectCodec<T> asnObjectCodec = this._mappings.GetAsnObjectForType<T>();
            asnObjectCodec.Encode(instance);
            this._serializers.Write<IAsnObjectCodec<T>, T>(asnObjectCodec, outputStream);
        }
    }
}

