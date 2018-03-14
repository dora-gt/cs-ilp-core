using System;
namespace Org.Interledger.Encoding.Asn.Framework
{
    public class CodecException : Exception
    {
        public CodecException(String message) : base(message)
        {
        }

        public CodecException(String message, Exception e) : base(message, e)
        {
            
        }
    }
}
