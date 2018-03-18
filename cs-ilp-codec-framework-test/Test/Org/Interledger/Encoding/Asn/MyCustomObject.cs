using System;
namespace Test.Org.Interledger.Encoding.Asn
{
    public class MyCustomObject
    {
        public string Utf8StringProperty { get; set; }

        public string FixedLengthUtf8StringProperty { get; set; }

        public byte Uint8Property { get; set; }

        public uint Uint32Property { get; set; }

        public ulong Uint64Property { get; set; }

        public byte[] OctetStringProperty { get; set; }

        public byte[] FixedLengthOctetStringProperty { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            MyCustomObject other = (MyCustomObject)obj;

            return this.Utf8StringProperty == other.Utf8StringProperty &&
                       this.FixedLengthUtf8StringProperty == other.FixedLengthUtf8StringProperty &&
                       this.Uint8Property == other.Uint8Property &&
                       this.Uint32Property == other.Uint32Property &&
                       this.Uint64Property == other.Uint64Property &&
                       IsByteArrayEqual(this.OctetStringProperty, other.OctetStringProperty) &&
                       IsByteArrayEqual(this.FixedLengthOctetStringProperty, other.FixedLengthOctetStringProperty);
        }

        public override int GetHashCode()
        {
            return this.Utf8StringProperty.GetHashCode() ^
                       this.FixedLengthUtf8StringProperty.GetHashCode() ^
                       this.Uint8Property.GetHashCode() ^
                       this.Uint32Property.GetHashCode() ^
                       this.Uint64Property.GetHashCode() ^
                       this.OctetStringProperty.GetHashCode() ^
                       this.FixedLengthOctetStringProperty.GetHashCode();
        }

        private bool IsByteArrayEqual(byte[] a, byte[] b)
        {
            if (a != null && b != null && a == b)
            {
                return true;
            }
            if (a == null || b == null)
            {
                return false;
            }
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int index = 0; index < a.Length; index++)
            {
                if (a[index] != b[index])
                {
                    return false;
                }
            }
            return true;
        }
    }
}