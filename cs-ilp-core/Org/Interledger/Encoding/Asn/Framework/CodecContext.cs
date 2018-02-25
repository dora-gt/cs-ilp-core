using System;
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

        public CodecContext Register<T>(Type type, IAsnObjectCodecSupplier<T> supplier)
        {
            Objects.RequireNonNull(type);
            Objects.RequireNonNull(supplier);

            //Verify we have a serializer for this object (throws if not)
            serializers.GetSerializer<T>(supplier.Get());

            //Register the mapping
            mappings.Register(supplier);

            return this;
        }


        public CodecContext Register<T, U>(Type t, IAsnObjectCodecSupplier<T> supplier, IAsnObjectSerializer<U> serializer)
        {
            Objects.requireNonNull(type);
            Objects.requireNonNull(supplier);
            Objects.requireNonNull(serializer);

            //Register the serializer
            serializers.register((Class<U>)((U)supplier.get()).getClass(), serializer);

            //Register the mapping
            mappings.register(type, supplier);

            return this;
        }


        public T read<T>(Typ type, InputStream inputStream)
        {
            AsnObjectCodec<T> asnObjectCodec = mappings.getAsnObjectForType(type);
            serializers.read(asnObjectCodec, inputStream);
            return asnObjectCodec.decode();
        }

        public <T> void write(T instance, OutputStream outputStream)
        {
            AsnObjectCodec<T> asnObjectCodec = mappings.getAsnObjectForType((Class<T>)instance.getClass());
            asnObjectCodec.encode(instance);
            serializers.write(asnObjectCodec, outputStream);
        }
    }
}

