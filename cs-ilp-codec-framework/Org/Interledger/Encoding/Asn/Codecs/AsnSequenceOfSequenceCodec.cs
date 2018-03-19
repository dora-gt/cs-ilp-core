using System;
using System.Collections;
using System.Collections.Generic;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    /// <summary>
    /// L = Sequence of Sequence POCO
    /// T = POCO which the sequence represents
    /// U = Sequence codec
    /// </summary>
    public class AsnSequenceOfSequenceCodec<L, T, U> : AsnObjectCodecBase<L>, IAsnSequenceOfSequenceCodec
        where L : IList<T>
        where U: AsnSequenceCodecBase<T>
    {
        private IAsnObjectCodecSupplier<U, T> sequenceCodecSupplier;
        private ISupplier<L> listSupplier;
        private List<AsnSequenceCodecBase<T>> codecs;

        public int Size
        {
            get
            {
                Objects.RequireNonNull(this.codecs);
                return this.codecs.Count;
            }
            set
            {
                this.codecs = new List<AsnSequenceCodecBase<T>>();
                for (int i = 0; i < value; i++)
                {
                    this.codecs.Add(this.sequenceCodecSupplier.Get());
                }
            }
        }

        public AsnSequenceOfSequenceCodec(ISupplier<L> listSupplier, IAsnObjectCodecSupplier<U, T> sequenceCodecSupplier)
        {
            this.listSupplier = listSupplier;
            this.sequenceCodecSupplier = sequenceCodecSupplier;
        }

        public dynamic GetCodecAt(int index)
        {
            Objects.RequireNonNull(this.codecs);
            return this.codecs[index];
        }

        public override L Decode()
        {
            Objects.RequireNonNull(this.codecs);
            L list = this.listSupplier.Get();
            foreach (AsnSequenceCodecBase<T> codec in this.codecs)
            {
                list.Add(codec.Decode());
            }
            return list;
        }

        public override void Encode(L value)
        {
            this.codecs = new List<AsnSequenceCodecBase<T>>(value.Count);
            foreach (T item in value)
            {
                AsnSequenceCodecBase<T> codec = this.sequenceCodecSupplier.Get();
                codec.Encode(item);
                this.codecs.Add(codec);
            }
        }
    }
}
