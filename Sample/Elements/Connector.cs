using System;

namespace Sample.Elements
{
    public class Connector : IConnector
    {
        private ILedger LedgerA { get; set; }

        private ILedger LedgerB { get; set; }

        public string Name { get; private set; }

        public Connector(string name, ILedger ledgerA, ILedger ledgerB)
        {
            this.Name = name;
            this.LedgerA = ledgerA;
            this.LedgerB = ledgerB;
        }
    }
}
