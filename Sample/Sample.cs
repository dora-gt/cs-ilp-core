using System;
using System.IO;
using System.Security.Cryptography;

using Org.Interledger.Core;
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
                byte writtenByte = 0;
                for (byte byteValue = 0; byteValue <= byte.MaxValue; )
                {
                    stream.Position = 0;
                    context.Write<byte>(byteValue, stream);
                    stream.Position = 0;
                    writtenByte = context.Read<byte>(stream);
                    System.Diagnostics.Debug.Assert(byteValue == writtenByte, string.Format("byte value differs! wrote: {0} read: {1}", byteValue, writtenByte));
                    Console.WriteLine(string.Format("written byte: {0}, confirmed: {1}", writtenByte, byteValue == writtenByte));

                    if (byteValue == byte.MaxValue)
                    {
                        break;
                    }

                    byteValue <<= 1;
                    byteValue |= 1;
                }

                uint writtenUint = 0;
                for (uint uintValue = 0; uintValue <= uint.MaxValue; )
                {
                    stream.Position = 0;
                    context.Write<uint>(uintValue, stream);
                    stream.Position = 0;
                    writtenUint = context.Read<uint>(stream);
                    System.Diagnostics.Debug.Assert(uintValue == writtenUint, string.Format("uint value differs! wrote: {0} read: {1}", uintValue, writtenUint));
                    Console.WriteLine(string.Format("written uint: {0}, confirmed: {1}", writtenUint, uintValue == writtenUint));

                    if (uintValue == uint.MaxValue)
                    {
                        break;
                    }

                    uintValue <<= 1;
                    uintValue |= 1;
                }

                ulong writtenUlong = 0;
                for (ulong ulongValue = 0; ulongValue <= ulong.MaxValue; )
                {
                    stream.Position = 0;
                    context.Write<ulong>(ulongValue, stream);
                    stream.Position = 0;
                    writtenUlong = context.Read<ulong>(stream);
                    System.Diagnostics.Debug.Assert(ulongValue == writtenUlong, string.Format("ulong value differs! wrote: {0}, read: {1}", ulongValue, writtenUlong));
                    Console.WriteLine(string.Format("written ulong: {0}, confirmed: {1}", writtenUlong, ulongValue == writtenUlong));

                    if (ulongValue == ulong.MaxValue)
                    {
                        break;
                    }

                    ulongValue <<= 1;
                    ulongValue |= 1;
                }
            }
        }
    }
}
