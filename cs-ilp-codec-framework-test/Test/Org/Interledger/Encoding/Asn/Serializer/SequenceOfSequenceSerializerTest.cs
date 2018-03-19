using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Xunit;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;
using Org.Interledger.Encoding.Asn.Serializers.Oer;

namespace Test.Org.Interledger.Encoding.Asn.Serializer
{
    public class SequenceOfSequenceSerializerTest
    {
        [Fact]
        public void TestSequence()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);
            context.Register<SampleSequenceCodec, SampleSequence>(new SampleSequenceCodecSupplier());

            SampleSequence sampleSequence = new SampleSequence(0, 1, 2);
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                context.Write<SampleSequence>(sampleSequence, stream);
                stream.Position = 0;
                SampleSequence writtenSequence = context.Read<SampleSequence>(stream);

                Assert.Equal(sampleSequence, writtenSequence);
            }
        }

        [Fact]
        public void TestSequenceOfSequence()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);
            context.Register<SampleSequenceCodec, SampleSequence>(new SampleSequenceCodecSupplier());
            context.Register<SampleSequenceOfSequenceCodec, SampleSequenceOfSequence>(new SampleSequenceOfSequenceCodecSupplier());

            SampleSequenceOfSequence ssos = new SampleSequenceOfSequence();
            ssos.Add(new SampleSequence(0, 1, 2));
            ssos.Add(new SampleSequence(3, 4, 5));
            ssos.Add(new SampleSequence(6, 7, 8));

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                context.Write<SampleSequenceOfSequence>(ssos, stream);
                stream.Position = 0;
                SampleSequenceOfSequence writtenSsos = context.Read<SampleSequenceOfSequence>(stream);

                Assert.Equal(ssos, writtenSsos);
            }
        }
    }

    public class SampleSequence
    {
        private uint[] numbers;

        public SampleSequence(params uint[] numbers)
        {
            this.numbers = numbers;
            if (this.numbers.Length != 3)
            {
                throw new Exception("length of numbers should be 3!");
            }
        }

        public uint[] GetNumbers()
        {
            return numbers;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            SampleSequence other = (SampleSequence)obj;
            if (obj == other)
            {
                return true;
            }

            uint[] thisNumbers = this.GetNumbers();
            uint[] otherNumbers = other.GetNumbers();
            if (otherNumbers.Length != thisNumbers.Length)
            {
                return false;
            }
            for (int index = 0; index < thisNumbers.Length; index++)
            {
                if (thisNumbers[index] != otherNumbers[index])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.GetNumbers().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("SampleSequence [{0}]", string.Join(", ", this.numbers));
        }
    }

    public class SampleSequenceOfSequence : List<SampleSequence>
    {
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            SampleSequenceOfSequence other = (SampleSequenceOfSequence)obj;
            if (obj == other)
            {
                return true;
            }

            if (this.Count != other.Count)
            {
                return false;
            }

            for (int index = 0; index < this.Count; index ++)
            {
                if (this[index].Equals(other[index]) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            foreach (SampleSequence sequence in this)
            {
                hashCode ^= sequence.GetHashCode();
            }
            return hashCode;
        }
    }

    public class SampleSequenceOfSequenceCodec : AsnSequenceOfSequenceCodec<SampleSequenceOfSequence, SampleSequence, SampleSequenceCodec>
    {
        public SampleSequenceOfSequenceCodec() : base (new SampleSequenceOfSequenceSupplier(), new SampleSequenceCodecSupplier())
        {
        }
    }

    public class SampleSequenceCodec : AsnSequenceCodecBase<SampleSequence>
    {
        public SampleSequenceCodec() : base(
            new AsnUint32Codec(),
            new AsnUint32Codec(),
            new AsnUint32Codec())
        {
        }

        public override SampleSequence Decode()
        {
            return new SampleSequence(GetValueAt<uint>(0), GetValueAt<uint>(1), GetValueAt<uint>(2));
        }

        public override void Encode(SampleSequence value)
        {
            SetValueAt(0, value.GetNumbers()[0]);
            SetValueAt(1, value.GetNumbers()[1]);
            SetValueAt(2, value.GetNumbers()[2]);
        }
    }

    public class SampleSequenceCodecSupplier : IAsnObjectCodecSupplier<SampleSequenceCodec, SampleSequence>
    {
        public SampleSequenceCodec Get()
        {
            return new SampleSequenceCodec();
        }
    }

    public class SampleSequenceOfSequenceCodecSupplier : IAsnObjectCodecSupplier<SampleSequenceOfSequenceCodec, SampleSequenceOfSequence>
    {
        public SampleSequenceOfSequenceCodec Get()
        {
            return new SampleSequenceOfSequenceCodec();
        }
    }

    public class SampleSequenceOfSequenceSupplier : ISupplier<SampleSequenceOfSequence>
    {
        public SampleSequenceOfSequence Get()
        {
            return new SampleSequenceOfSequence();
        }
    }
}
