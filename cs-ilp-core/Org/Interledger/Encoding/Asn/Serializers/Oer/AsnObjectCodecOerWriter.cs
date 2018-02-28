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

        public Stream Stream { get; set; }

        public AsnObjectCodecOerWriter()
        {
            this.AsnUint8OerSerializer = new AsnUint8OerSerializer();
            this.AsnUint32OerSerializer = new AsnOctetStringOerSerializer<uint>();
            this.AsnUint64OerSerializer = new AsnOctetStringOerSerializer<ulong>();
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
    }
}
