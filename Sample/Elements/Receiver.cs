using System;
namespace Sample.Elements
{
    public class Receiver : IReceiver
    {
        private byte[] SharedSecret { get; set; }

        public Receiver(byte[] sharedSecret)
        {
            this.SharedSecret = sharedSecret;
        }
    }
}
