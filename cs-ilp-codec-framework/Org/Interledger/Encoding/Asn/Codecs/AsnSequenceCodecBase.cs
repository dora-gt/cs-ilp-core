﻿using System;
using System.Collections;
using System.Collections.Generic;

using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public abstract class AsnSequenceCodecBase<T> : AsnObjectCodecBase<T>, IAsnSequenceCodec, IAsnObjectCodec<T>
    {
        // object = AsnObjectCodec
        private readonly List<object> sequence = new List<object>();

        public int Size { get { return this.sequence.Count; } }

        public AsnSequenceCodecBase(params object[] fields)
        {
            this.sequence.AddRange(fields);
        }

        protected void SetCodecAt(int index, object codec)
        {
            Objects.RequireNonNull(codec);
            this.sequence[index] = codec;
        }

        public U GetValueAt<U>(int index)
        {
            return (U)(GetCodecAt(index).Decode());
        }

        public void SetValueAt(int index, dynamic value)
        {
            GetCodecAt(index).Encode(value);
        }

        public dynamic GetCodecAt(int index)
        {
            return sequence[index];
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

            AsnSequenceCodecBase<T> that = (AsnSequenceCodecBase<T>)obj;

            return sequence.Equals(that.sequence);
        }

        public override int GetHashCode()
        {
            return this.sequence.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("AsnSequenceCodec{{sequence={0}}}", String.Concat(this.sequence, ", "));
        }
    }
}
