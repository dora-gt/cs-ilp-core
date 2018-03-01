using System;
using System.IO;
using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers.Oer
{
    public class AsnObjectCodecOerReader : IAsnObjectCodecReader
    {
        private AsnUint8OerSerializer AsnUint8OerSerializer { get; set; }

        private AsnOctetStringOerSerializer<uint> AsnUint32OerSerializer { get; set; }

        private AsnOctetStringOerSerializer<ulong> AsnUint64OerSerializer { get; set; }

        public Stream Stream { get; set; }

        public AsnObjectCodecOerReader()
        {
            this.AsnUint8OerSerializer = new AsnUint8OerSerializer();
            this.AsnUint32OerSerializer = new AsnOctetStringOerSerializer<uint>();
            this.AsnUint64OerSerializer = new AsnOctetStringOerSerializer<ulong>();
        }

        public void Visit(AsnUint8Codec asnUint8Codec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnUint8OerSerializer.Read(asnUint8Codec, this.Stream);
        }

        public void Visit(AsnUint32Codec asnUint32Codec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnUint32OerSerializer.Read(asnUint32Codec, this.Stream);
        }

        public void Visit(AsnUint64Codec asnUint64Codec)
        {
            Objects.RequireNonNull(this.Stream);
            this.AsnUint64OerSerializer.Read(asnUint64Codec, this.Stream);
        }

        public void Visit(AsnOctetStringCodec asnOctetStringCodec)
        {
            // FIXME
            throw new NotImplementedException();
        }

        public void Visit(AsnPrintableStringCodec asnPrintableStringCodec)
        {
            // FIXME
            throw new NotImplementedException();
        }

        public void Visit(AsnUtf8StringCodec asnUtf8StringCodec)
        {
            // FIXME
            throw new NotImplementedException();
        }

        public void Visit(AsnIA5StringCodec asnIA5StringCodec)
        {
            // FIXME
            throw new NotImplementedException();
        }
    }
}
