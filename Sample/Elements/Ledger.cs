using System;
namespace Sample.Elements
{
    public class Ledger : ILedger
    {
        public string Name { get; private set; }

        public Ledger(string name)
        {
            this.Name = name;
        }
    }
}
