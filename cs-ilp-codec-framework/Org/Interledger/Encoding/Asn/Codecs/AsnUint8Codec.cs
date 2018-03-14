using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnUint8Codec : AsnPrimitiveCodecBase<byte>
    {
        private byte Value { get; set; }

        public AsnUint8Codec() : base(new AsnSizeConstraint(0, 1))
        {
        }

        public override byte Decode()
        {
            return this.Value;
        }

        public override void Encode(byte value)
        {
            this.Value = value;
            this.OnEncoded(this.Value);
        }

        public override string ToString()
        {
            return string.Format("AsnUint8Codec{{value={0}}}", this.Decode());
        }
    }
}
