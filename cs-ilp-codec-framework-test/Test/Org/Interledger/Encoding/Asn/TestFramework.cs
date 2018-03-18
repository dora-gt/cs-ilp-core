using System;
using System.IO;
using Xunit;

using Org.Interledger.Encoding.Asn.Framework;
using Org.Interledger.Encoding.Asn.Codecs;
using Org.Interledger.Encoding.Asn.Serializers.Oer;

namespace Test.Org.Interledger.Encoding.Asn
{
    public class TestFramework
    {
        [Fact]
        public void TestByte()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);

            using (MemoryStream stream = new MemoryStream())
            {
                byte writtenByte = 0;
                for (byte byteValue = 0; byteValue <= byte.MaxValue;)
                {
                    stream.Position = 0;
                    context.Write<byte>(byteValue, stream);
                    stream.Position = 0;
                    writtenByte = context.Read<byte>(stream);

                    System.Console.WriteLine(string.Format("original byte: {0}, read byte: {1}", byteValue, BitConverter.ToString(BitConverter.GetBytes(writtenByte))));
                    Assert.Equal(byteValue, writtenByte);

                    if (byteValue == byte.MaxValue)
                    {
                        break;
                    }

                    byteValue <<= 1;
                    byteValue |= 1;
                    Array.Clear(stream.GetBuffer(), 0, (int)stream.Length);
                }
            }
        }

        [Fact]
        public void TestUint()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);

            using (MemoryStream stream = new MemoryStream())
            {
                uint writtenUint = 0;
                for (uint uintValue = 0; uintValue <= uint.MaxValue;)
                {
                    stream.Position = 0;
                    context.Write<uint>(uintValue, stream);
                    stream.Position = 0;
                    writtenUint = context.Read<uint>(stream);

                    System.Console.WriteLine(string.Format("original uint: {0}, read uint: {1}", uintValue, BitConverter.ToString(BitConverter.GetBytes(writtenUint))));
                    Assert.Equal(uintValue, writtenUint);

                    if (uintValue == uint.MaxValue)
                    {
                        break;
                    }

                    uintValue <<= 1;
                    uintValue |= 1;
                    Array.Clear(stream.GetBuffer(), 0, (int)stream.Length);
                }
            }
        }

        [Fact]
        public void TestUlong()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);

            using (MemoryStream stream = new MemoryStream())
            {
                ulong writtenUlong = 0;
                for (ulong ulongValue = 0; ulongValue <= ulong.MaxValue;)
                {
                    stream.Position = 0;
                    context.Write<ulong>(ulongValue, stream);
                    stream.Position = 0;
                    writtenUlong = context.Read<ulong>(stream);

                    System.Console.WriteLine(string.Format("original ulong: {0}, read ulong: {1}", ulongValue, BitConverter.ToString(BitConverter.GetBytes(writtenUlong))));
                    Assert.Equal(ulongValue, writtenUlong);

                    if (ulongValue == ulong.MaxValue)
                    {
                        break;
                    }

                    ulongValue <<= 1;
                    ulongValue |= 1;
                    Array.Clear(stream.GetBuffer(), 0, (int)stream.Length);
                }
            }
        }

        [Fact]
        public void TestBytes()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);

            using (MemoryStream stream = new MemoryStream())
            {
                Random rand = new Random();
                byte[] testData = new Byte[1024 * 1024];
                rand.NextBytes(testData);

                stream.Position = 0;
                context.Write<byte[]>(testData, stream);
                stream.Position = 0;
                byte[] writtenBytes = context.Read<byte[]>(stream);

                Assert.Equal(testData.Length, writtenBytes.Length);
                Assert.Equal<byte[]>(testData, writtenBytes);
            }
        }

        [Fact]
        public void TestStringUtf8()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);
            string strToWrite = "aiueo";

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                context.Write<string>(strToWrite, stream);
                stream.Position = 0;
                string writtenString = context.Read<string>(stream);

                Assert.Equal(strToWrite, writtenString);
            }
        }

        [Fact]
        public void TestStringUtf8MultiBytes()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);
            string strToWrite = "これはマルチバイトの日本語のテストです。";

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Position = 0;
                context.Write<string>(strToWrite, stream);
                stream.Position = 0;
                string writtenString = context.Read<string>(stream);

                Assert.Equal(strToWrite, writtenString);
            }
        }

        [Fact]
        public void TestRawSerialization()
        {
            AsnOctetStringCodec asnOctetStringCodec = new AsnOctetStringCodec(AsnSizeConstraint.UNCONSTRAINED);
            asnOctetStringCodec.Encode(new byte[] { 0, 1, 2, 3, 4, 5});

            AsnOctetStringOerSerializer serializer = new AsnOctetStringOerSerializer();

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Write(asnOctetStringCodec, stream);
            }
        }

        [Fact]
        public void TestMyCustomObjectEquality()
        {
            MyCustomObject o1 = new MyCustomObject()
            {
                Utf8StringProperty = "aiueo",
                FixedLengthUtf8StringProperty = "kakikukeko",
                Uint8Property = 100,
                Uint32Property = 1000,
                Uint64Property = 10000,
                OctetStringProperty = new byte[] { 1, 2, 3, 4, 5 },
                FixedLengthOctetStringProperty = new byte[] { 1, 2, 3, 4, 5 },
            };

            MyCustomObject o2 = new MyCustomObject()
            {
                Utf8StringProperty = "aiueo",
                FixedLengthUtf8StringProperty = "kakikukeko",
                Uint8Property = 100,
                Uint32Property = 1000,
                Uint64Property = 10000,
                OctetStringProperty = new byte[] { 1, 2, 3, 4, 5 },
                FixedLengthOctetStringProperty = new byte[] { 1, 2, 3, 4, 5 },
            };

            Assert.Equal(true, o1.Equals(o2));
        }

        [Fact]
        public void TestSequence()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);
            context.Register<AsnSequenceCodecBase<MyCustomObject>, MyCustomObject>(new AsnMyCustomObjectCodecSupplier());

            using (MemoryStream stream = new MemoryStream())
            {
                MyCustomObject o1 = new MyCustomObject()
                {
                    Utf8StringProperty = "aiueo",
                    FixedLengthUtf8StringProperty = "kaki",
                    Uint8Property = 100,
                    Uint32Property = 1000,
                    Uint64Property = 10000,
                    OctetStringProperty = new byte[] { 1, 2, 3, 4, 5 },
                    FixedLengthOctetStringProperty = new byte[] {
                        1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                        11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                        21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                        31, 32
                    },
                };

                stream.Position = 0;
                context.Write<MyCustomObject>(o1, stream);
                stream.Position = 0;
                MyCustomObject writtenMyCustomObject = context.Read<MyCustomObject>(stream);
                Assert.Equal(o1, writtenMyCustomObject);
            }
        }
    }
}
