using System;

namespace Sample.Elements
{
    public class Sender : ISender
    {
        private byte[] SharedSecret { get; set; }

        public Sender(byte[] sharedSecret)
        {
            this.SharedSecret = sharedSecret;
        }
    }
}
