using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeftRecElim.Logics
{
    class NonTerminal : ISymbol
    {
        public string Name { get; set; }

        public NonTerminal(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
