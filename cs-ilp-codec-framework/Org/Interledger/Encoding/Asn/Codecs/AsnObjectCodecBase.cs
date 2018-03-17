using System;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    // FIXME イベント系が未実装っちゃ未実装。いったん置きでより大きな問題に対処する
    public abstract class AsnObjectCodecBase<T> : IAsnObjectCodec<T>
    {
        public event Action<T> Encoded = delegate {};

        protected void OnEncoded(T value)
        {
            if (this.HasEncodeEventListener())
            {
                this.Encoded(value);
            }
        }

        public abstract T Decode();

        public abstract void Encode(T value);

        public bool HasEncodeEventListener()
        {
            return this.Encoded != null;
        }

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

            AsnObjectCodecBase<T> other = (AsnObjectCodecBase<T>)obj;

            return Decode().Equals(other.Decode());
        }

        public override int GetHashCode()
        {
            return this.Decode().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("AsnObjectCodec{{value={0}}}", this.Decode());
        }
    }
}
