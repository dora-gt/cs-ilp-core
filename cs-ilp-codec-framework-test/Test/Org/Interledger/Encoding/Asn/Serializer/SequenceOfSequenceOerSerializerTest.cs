using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;
using Org.Interledger.Encoding.Asn.Serializers.Oer;

namespace Test.Org.Interledger.Encoding.Asn.Serializer
{
    public class SequenceOfSequenceOerSerializerTest
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> SequenceOfSequenceData = new[] {
            new object[] {
                new byte[][]
                {
                    new byte[]{0,0,0}
                },
                TestUtils.GetBytesFromHexString("0101000000", 16)
            },
            new object[] {
                new byte[][]
                {
                    new byte[]{1,2,3},
                    new byte[]{1,2,3}
                },
                TestUtils.GetBytesFromHexString("0102010203010203", 16)
            },
            new object[] {
                new byte[][]
                {
                    new byte[]{0,1,255},
                    new byte[]{0,1,255},
                    new byte[]{0,1,255},
                    new byte[]{0,1,255},
                    new byte[]{255,0,1}
                },
                TestUtils.GetBytesFromHexString("01050001FF0001FF0001FF0001FFFF0001", 16)
            }
        };

        public SequenceOfSequenceOerSerializerTest(ITestOutputHelper output)
        {
            this._output = output;
        }

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
            ssos.Add(new SampleSequence(0, 0, 0));

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                context.Write<SampleSequenceOfSequence>(ssos, stream);
                stream.Position = 0;
                SampleSequenceOfSequence writtenSsos = context.Read<SampleSequenceOfSequence>(stream);

                Assert.Equal(ssos, writtenSsos);
            }
        }

        [Theory]
        [MemberData(nameof(SequenceOfSequenceData))]
        public void TestSequenceOfSequenceBytes(byte[][] data, byte[] expectedBytes)
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);
            context.Register<SampleSequenceCodec, SampleSequence>(new SampleSequenceCodecSupplier());
            context.Register<SampleSequenceOfSequenceCodec, SampleSequenceOfSequence>(new SampleSequenceOfSequenceCodecSupplier());

            SampleSequenceOfSequence ssos = new SampleSequenceOfSequence();
            foreach (byte[] ints in data)
            {
                ssos.Add(new SampleSequence(ints));
            }

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                context.Write<SampleSequenceOfSequence>(ssos, stream);
                byte[] bytes = stream.ToArray();
                this._output.WriteLine(string.Format("TestSequenceOfSequenceBytes,\n\t{0} :expected bytes\n\t{1} :bytes read from stream", BitConverter.ToString(expectedBytes), BitConverter.ToString(bytes)));
                Assert.True(TestUtils.IsListEqual<byte>(expectedBytes, bytes));
            }
        }
    }

    public class SampleSequence
    {
        public byte[] Numbers { get; private set; }

        public SampleSequence(params byte[] numbers)
        {
            this.Numbers = numbers;
            if (this.Numbers.Length != 3)
            {
                throw new Exception("length of numbers should be 3!");
            }
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

            byte[] thisNumbers = this.Numbers;
            byte[] otherNumbers = other.Numbers;
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
            return this.Numbers.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("SampleSequence [{0}]", string.Join(", ", this.Numbers));
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

            for (int index = 0; index < this.Count; index++)
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
        public SampleSequenceOfSequenceCodec() : base(new SampleSequenceOfSequenceSupplier(), new SampleSequenceCodecSupplier())
        {
        }
    }

    public class SampleSequenceCodec : AsnSequenceCodecBase<SampleSequence>
    {
        public SampleSequenceCodec() : base(
            new AsnUint8Codec(),
            new AsnUint8Codec(),
            new AsnUint8Codec())
        {
        }

        public override SampleSequence Decode()
        {
            return new SampleSequence(GetValueAt<byte>(0), GetValueAt<byte>(1), GetValueAt<byte>(2));
        }

        public override void Encode(SampleSequence value)
        {
            SetValueAt(0, value.Numbers[0]);
            SetValueAt(1, value.Numbers[1]);
            SetValueAt(2, value.Numbers[2]);
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
