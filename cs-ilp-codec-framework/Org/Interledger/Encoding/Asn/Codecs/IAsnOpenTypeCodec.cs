using System;
using Org.Interledger.Encoding.Asn.Framework;

namespace Org.Interledger.Encoding.Asn.Codecs
{
    public interface IAsnOpenTypeCodec
    {
        dynamic GetInnerCodec();
    }
}
