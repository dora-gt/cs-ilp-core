using System;
using System.IO;
using System.Security.Cryptography;

using Org.Interledger.Core.PSK2;
using Org.Interledger.Encoding.Asn.Framework;

using Sample.Elements;
using static Sample.Logger;

namespace Sample
{
    public class Sample
    {
        /// <summary>
        /// This implementation is just a pseudo code that shows how ILPv4 works,
        /// and doesn't use socket actually. This is because the object of this project is
        /// to show ILP structure simply.
        /// </summary>
        public static void Main(string[] args)
        {
            // sender <BTCLedger> connectorA <XRPLedger> connectorB <YenLedger> receiver
            ILedger btcLedger = new Ledger("BTC ledger");
            ILedger xrpLedger = new Ledger("XRP ledger");
            ILedger yenLedger = new Ledger("YEN ledger");

            IConnector connectorA = new Connector("BTC/XRP", btcLedger, xrpLedger);
            IConnector connectorB = new Connector("XRP/YEN", xrpLedger, yenLedger);

            // Application layer work
            // Sender and receiver agree on a shared key that is shared WITHOUT ILP packet.
            // See also SPSP(https://github.com/interledger/rfcs/blob/master/0009-simple-payment-setup-protocol/0009-simple-payment-setup-protocol.md).
            // In this code, it is just done by setting variable.

            // Shared secret is just a random bytes.
            byte[] sharedSecret = new byte[PSK2Const.SharedSecretLength];
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(sharedSecret);
            Log("sharedSecret: {0}", BitConverter.ToString(sharedSecret));

            ISender sender = new Sender(sharedSecret);
            IReceiver receiver = new Receiver(sharedSecret);

            Codec();
        }

        private static void Codec()
        {
            CodecContext context = CodecContextFactory.GetContext(CodecContextFactory.OCTET_ENCODING_RULES);

            using (MemoryStream stream = new MemoryStream())
            {
                context.Write<int>(100, stream);
                Console.WriteLine(BitConverter.ToString(stream.GetBuffer()));
                stream.Position = 0;
                int written = context.Read<int>(stream);
                Console.WriteLine(string.Format("{0}", written));
            }
        }
    }
}
