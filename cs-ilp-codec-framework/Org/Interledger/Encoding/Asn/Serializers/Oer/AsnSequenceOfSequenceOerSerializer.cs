using System;
using System.IO;
using System.Numerics;
using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnSequenceOfSequenceOerSerializer : IAsnObjectSerializer<IAsnSequenceOfSequenceCodec>
    {
        private AsnObjectSerializationContext AsnObjectSerializationContext { get; set; }

        public AsnSequenceOfSequenceOerSerializer(AsnObjectSerializationContext asnObjectSerializationContext)
        {
            this.AsnObjectSerializationContext = asnObjectSerializationContext;
        }

        public void Read(IAsnSequenceOfSequenceCodec instance, Stream inputStream)
        {
            AsnUintCodec quantityCodec = new AsnUintCodec();
            this.AsnObjectSerializationContext.Read(quantityCodec, inputStream);

            BigInteger quantityBigInt = quantityCodec.Decode();
            if (quantityBigInt.CompareTo(new BigInteger(int.MaxValue)) > 0)
            {
                throw new CodecException("SEQUENCE_OF quantities > Integer.MAX_VALUE ar not supported");
            }

            int quantity = (int)quantityBigInt;
            instance.Size = quantity;

            for (int i = 0; i < quantity; i++)
            {
                this.AsnObjectSerializationContext.Read(instance.GetCodecAt(i), inputStream);
            }
        }

        public void Write(IAsnSequenceOfSequenceCodec instance, Stream outputStream)
        {
            AsnUintCodec quantityCodec = new AsnUintCodec();
            quantityCodec.Encode(new BigInteger(instance.Size));
            this.AsnObjectSerializationContext.Write(quantityCodec, outputStream);

            for (int i = 0; i < instance.Size; i++)
            {
                this.AsnObjectSerializationContext.Write(instance.GetCodecAt(i), outputStream);
            }
        }
    }
}
