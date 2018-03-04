using System;
using System.IO;
using Xunit;

using Org.Interledger.Encoding.Asn.Framework;

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
    }
}
