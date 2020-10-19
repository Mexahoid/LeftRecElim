using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeftRecElim.Logics
{
    interface ISymbol
    {
        string Name { get; set; }

        string ToString();
    }
}
