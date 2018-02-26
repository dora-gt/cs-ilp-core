using System;
namespace Org.Interledger.Encoding.Asn.Codecs
{
    public class AsnUint8Codec : AsnPrimitiveCodecBase<int>
    {
        private int Value { get; set; }

        public AsnUint8Codec() : base(new AsnSizeConstraint(0, 1))
        {
        }

        public override int Decode()
        {
            return this.Value;
        }

        public override void Encode(int value)
        {
            if (255 < value || value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Uint8 only supports values from 0 to 255, value {0} is out of range.", value)
                );
            }

            this.Value = value;
            this.OnEncoded(this.Value);
        }

        public override string ToString()
        {
            return string.Format("AsnUint8Codec{{value={0}}}", this.Decode());
        }
    }
}
