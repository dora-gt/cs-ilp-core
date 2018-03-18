using System;

using Org.Interledger.Encoding.Asn.Codecs;

namespace Test.Org.Interledger.Encoding.Asn
{
    public class AsnMyCustomObjectCodec : AsnSequenceCodecBase<MyCustomObject>
    {
        public AsnMyCustomObjectCodec() : base(
            new AsnUtf8StringCodec(AsnSizeConstraint.UNCONSTRAINED),
            new AsnUtf8StringCodec(4),
            new AsnUint8Codec(),
            new AsnUint32Codec(),
            new AsnUint64Codec(),
            new AsnOctetStringCodec(AsnSizeConstraint.UNCONSTRAINED),
            new AsnOctetStringCodec(32)
        )
        {
        }

        public override MyCustomObject Decode()
        {
            return new MyCustomObject()
            {
                Utf8StringProperty = GetValueAt<string>(0),
                FixedLengthUtf8StringProperty = GetValueAt<string>(1),
                Uint8Property = GetValueAt<byte>(2),
                Uint32Property = GetValueAt<uint>(3),
                Uint64Property = GetValueAt<ulong>(4),
                OctetStringProperty = GetValueAt<byte[]>(5),
                FixedLengthOctetStringProperty = GetValueAt<byte[]>(6),
            };
        }

        public override void Encode(MyCustomObject value)
        {
            this.SetValueAt(0, value.Utf8StringProperty);
            this.SetValueAt(1, value.FixedLengthUtf8StringProperty);
            this.SetValueAt(2, value.Uint8Property);
            this.SetValueAt(3, value.Uint32Property);
            this.SetValueAt(4, value.Uint64Property);
            this.SetValueAt(5, value.OctetStringProperty);
            this.SetValueAt(6, value.FixedLengthOctetStringProperty);
        }
    }
}
