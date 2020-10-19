using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeftRecElim.Logics
{
    class Terminal : ISymbol
    {
        public string Name { get; set; }

        public Terminal(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
