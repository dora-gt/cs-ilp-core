using System;
using System.IO;
using Xunit;

using Org.Interledger.Encoding.Asn.Codecs;

namespace Test.Org.Interledger.Encoding.Asn
{
    public class TestInterledgerCodecs
    {
        [Fact]
        public void TestTimestamp()
        {
            DateTime dateTime = DateTime.Now;

            AsnTimestampCodec codec = new AsnTimestampCodec();
            codec.Encode(dateTime);
            Console.WriteLine(string.Format("encoded: {0}", codec.CharString));

            DateTime decodedDateTime = codec.Decode();
            Console.WriteLine(string.Format("decoded: {0}", decodedDateTime));
        }
    }
}