using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnOpenTypeCodec<T> : AsnObjectCodecBase<T>, IAsnOpenTypeCodec
    {
        private IAsnObjectCodec<T> InnerCodec { get; set; }

        public AsnOpenTypeCodec(IAsnObjectCodec<T> innerCodec)
        {
            this.InnerCodec = innerCodec;
        }

        public override T Decode()
        {
            return InnerCodec.Decode();
        }

        public override void Encode(T value)
        {
            this.InnerCodec.Encode(value);
        }

        public dynamic GetInnerCodec()
        {
            return this.InnerCodec;
        }
    }
}
