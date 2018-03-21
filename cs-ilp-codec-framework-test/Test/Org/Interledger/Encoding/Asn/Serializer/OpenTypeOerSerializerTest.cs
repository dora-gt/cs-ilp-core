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
    public class OpenTypeOerSerializerTest
    {
        private readonly ITestOutputHelper _output;

        public static IEnumerable<object[]> OpenTypeData = new[] {

            // [asn1_bytes][octet_bytes]
            new object [] { TestUtils.GetBytesFromHexString("0100", 16), System.Text.Encoding.ASCII.GetBytes("") },
            new object [] { TestUtils.GetBytesFromHexString("020101", 16), TestUtils.GetBytesFromHexString("01", 16) },
            new object [] { TestUtils.GetBytesFromHexString("0403616263", 16), System.Text.Encoding.ASCII.GetBytes("abc") },
            new object [] { TestUtils.GetBytesFromHexString("0C0B68656C6C6F20776F726C64", 16), System.Text.Encoding.ASCII.GetBytes("hello world") },
            new object [] { TestUtils.GetBytesFromHexString("0B0A672E746573742E666F6F", 16), System.Text.Encoding.ASCII.GetBytes("g.test.foo") },
            new object [] { TestUtils.GetBytesFromHexString(
                "8204028203FF672E746573742E313032342E4141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "4141414141414141414141414141414141414141414141414141414141414141414141414"
                + "1414141414141414141414141414141414141414141414141414141414141414141414141"
                + "41414141414141414141414141414141", 16),
                System.Text.Encoding.ASCII.GetBytes(
                "g.test.1024.AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA") },
        };

        public OpenTypeOerSerializerTest(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Theory]
        [MemberData(nameof(OpenTypeData))]
        public void TestOpenTypeRead(byte[] asn1Bytes, byte[] octetBytes)
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);
            context.Register<SampleOpenTypeCodec, SampleType>(new SampleOpenTypeSupplier());

            this._output.WriteLine(string.Format("TestOpenTypeRead with parameters,\n\t{0} :asn1Bytes bytes\n\t{1} :octetBytes", BitConverter.ToString(asn1Bytes), BitConverter.ToString(octetBytes)));

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                stream.Write(asn1Bytes, 0, asn1Bytes.Length);
                stream.Position = 0;
                SampleType actualValue = context.Read<SampleType>(stream);
                this._output.WriteLine(string.Format("TestOpenTypeRead result,\n\t{0} :expected bytes\n\t{1} :actual bytes", BitConverter.ToString(octetBytes), BitConverter.ToString(actualValue.Bytes)));
                Assert.Equal(octetBytes, actualValue.Bytes);

                stream.Position = 0;
                context.Write<SampleType>(actualValue, stream);
                stream.Position = 0;
                SampleType writtenValue = context.Read<SampleType>(stream);
                Assert.Equal(actualValue, writtenValue);
            }
        }
    }


    public class SampleType
    {
        public byte[] Bytes { get; private set; }

        public SampleType(byte[] bytes)
        {
            this.Bytes = bytes;
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

            SampleType other = (SampleType)obj;
            return TestUtils.IsListEqual<byte>(this.Bytes, other.Bytes);
        }

        public override int GetHashCode()
        {
            return this.Bytes.GetHashCode();
        }
    }

    public class SampleTypeCodec : AsnOctetStringBasedObjectCodecBase<SampleType>
    {
        public SampleTypeCodec() : base(AsnSizeConstraint.UNCONSTRAINED)
        {
        }

        public override SampleType Decode()
        {
            return new SampleType(this.Bytes);
        }

        public override void Encode(SampleType value)
        {
            this.Bytes = value.Bytes;
        }
    }

    public class SampleOpenTypeCodec : AsnOpenTypeCodec<SampleType>
    {
        public SampleOpenTypeCodec() : base(new SampleTypeCodec())
        {
        }
    }

    public class SampleOpenTypeSupplier : IAsnObjectCodecSupplier<SampleOpenTypeCodec, SampleType>
    {
        public SampleOpenTypeCodec Get()
        {
            return new SampleOpenTypeCodec();
        }
    }
}
