using System;
using System.IO;
using System.Numerics;
using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnSequenceOfSequenceOerSerializer : IAsnObjectSerializer<IAsnSequenceOfSequenceCodec>
    {
        public void Read(AsnObjectSerializationContext context, IAsnSequenceOfSequenceCodec instance, Stream inputStream)
        {
            AsnUintCodec quantityCodec = new AsnUintCodec();
            context.Read(quantityCodec, inputStream);

            BigInteger quantityBigInt = quantityCodec.Decode();
            if (quantityBigInt.CompareTo(new BigInteger(int.MaxValue)) > 0)
            {
                throw new CodecException("SEQUENCE_OF quantities > Integer.MAX_VALUE ar not supported");
            }

            int quantity = (int)quantityBigInt;
            instance.Size = quantity;

            for (int i = 0; i < quantity; i++)
            {
                context.Read(instance.GetCodecAt(i), inputStream);
            }
        }

        public void Write(AsnObjectSerializationContext context, IAsnSequenceOfSequenceCodec instance, Stream outputStream)
        {
            AsnUintCodec quantityCodec = new AsnUintCodec();
            quantityCodec.Encode(new BigInteger(instance.Size));
            context.Write(quantityCodec, outputStream);

            for (int i = 0; i < instance.Size; i++)
            {
                context.Write(instance.GetCodecAt(i), outputStream);
            }
        }
    }
}
