using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace LeftRecElim.Logics
{
    class Chain
    {
        public List<ISymbol> Symbols { get; set; }

        public int Count
        {
            get => Symbols.Count; 
        }

        public Chain()
        {
            Symbols = new List<ISymbol>();
        }

        public Chain(List<ISymbol> chain)
        {
            Symbols = chain;
        }

        public bool Contains(ISymbol symbol)
        {
            return Symbols.Contains(symbol);
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var symbol in Symbols)
            {
                str.Append(symbol);
            }

            return str.ToString();
        }
    }
}
