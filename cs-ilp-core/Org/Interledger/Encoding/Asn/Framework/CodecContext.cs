using System;
using System.IO;

namespace Org.Interledger.Encoding.Asn.Framework
{
    public class CodecContext
    {
        private readonly AsnObjectCodecRegistry mappings;
        private readonly AsnObjectSerializationContext serializers;

        public CodecContext(AsnObjectCodecRegistry mappings, AsnObjectSerializationContext serializers)
        {
            this.mappings = mappings;
            this.serializers = serializers;
        }

        public CodecContext Register<T, U>(IAsnObjectCodecSupplier<U> supplier) where T: IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(supplier);

            //Verify we have a serializer for this object (throws if not)
            serializers.GetSerializer<IAsnObjectCodec<U>, U>(supplier.Get());

            //Register the mapping
            mappings.Register(supplier);

            return this;
        }

        public CodecContext Register<T, U>(IAsnObjectCodecSupplier<U> supplier, IAsnObjectSerializer<T, U> serializer) where T : IAsnObjectCodec<U>
        {
            Objects.RequireNonNull(supplier);
            Objects.RequireNonNull(serializer);

            //Register the serializer
            serializers.Register(typeof(U), serializer);

            //Register the mapping
            mappings.Register<U>(supplier);

            return this;
        }

        public T Read<T>(Stream inputStream)
        {
            IAsnObjectCodec<T> asnObjectCodec = mappings.GetAsnObjectForType<T>();
            serializers.Read<IAsnObjectCodec<T>, T>(asnObjectCodec, inputStream);
            return asnObjectCodec.Decode();
        }

        public void Write<T>(T instance, Stream outputStream)
        {
            IAsnObjectCodec<T> asnObjectCodec = mappings.GetAsnObjectForType<T>();
            asnObjectCodec.Encode(instance);
            serializers.Write<IAsnObjectCodec<T>, T>(asnObjectCodec, outputStream);
        }
    }
}

