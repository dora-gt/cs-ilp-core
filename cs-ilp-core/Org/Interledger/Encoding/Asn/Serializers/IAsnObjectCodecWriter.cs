using System;
using System.IO;

using Org.Interledger.Encoding.Asn.Codecs;

namespace Org.Interledger.Encoding.Asn.Serializers
{
    public interface IAsnObjectCodecWriter : IAsnObjectCodecVisitor
    {
        Stream Stream { get; set; }
    }
}
