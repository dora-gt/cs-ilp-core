using System;
using System.IO;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnObjectCodecOerWriter : IAsnObjectCodecWriter
    {
        private AsnUint8OerSerializer AsnUint8OerSerializer { get; set; }

        private AsnOctetStringOerSerializer<uint> AsnUint32OerSerializer { get; set; }

        private AsnOctetStringOerSerializer<ulong> AsnUint64OerSerializer { get; set; }

        private AsnOctetStringOerSerializer<byte[]> AsnBytesOerSerializer { get; set; }

        private AsnCharStringOerSerializer AsnCharStringOerSerializer { get; set; }

        public Stream Stream { get; set; }

        public AsnObjectCodecOerWriter()
        {
            this.AsnUint8OerSerializer = new AsnUint8OerSerializer();
            this.AsnUint32OerSerializer = new AsnOctetStringOerSerializer<uint>();
            this.AsnUint64OerSerializer = new AsnOctetStringOerSerializer<ulong>();
            this.AsnBytesOerSerializer = new AsnOctetStringOerSerializer<byte[]>();
            this.AsnCharStringOerSerializer = new AsnCharStringOerSerializer();
        }

        public void Visit(AsnUint8Codec asnUint8Codec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnUint8OerSerializer.Write(asnUint8Codec, this.Stream);
        }

        public void Visit(AsnUint32Codec asnUint32Codec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnUint32OerSerializer.Write(asnUint32Codec, this.Stream);
        }

        public void Visit(AsnUint64Codec asnUint64Codec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnUint64OerSerializer.Write(asnUint64Codec, this.Stream);
        }

        public void Visit(AsnOctetStringCodec asnOctetStringCodec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnBytesOerSerializer.Write(asnOctetStringCodec, this.Stream);
        }

        public void Visit(AsnPrintableStringCodec asnPrintableStringCodec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnCharStringOerSerializer.Write(asnPrintableStringCodec, this.Stream);
        }

        public void Visit(AsnUtf8StringCodec asnUtf8StringCodec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnCharStringOerSerializer.Write(asnUtf8StringCodec, this.Stream);
        }

        public void Visit(AsnIA5StringCodec asnIA5StringCodec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnCharStringOerSerializer.Write(asnIA5StringCodec, this.Stream);
        }
    }
}
