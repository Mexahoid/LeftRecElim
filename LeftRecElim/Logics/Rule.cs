using System.Collections.Generic;
using System.Text;

namespace LeftRecElim.Logics
{
    internal class Rule
    {
        public ISymbol Key
        {
            get; set;
        }

        public List<Chain> Rules
        {
            get; set;
        }

        public Rule(ISymbol key, List<Chain> rules)
        {
            Key = key;
            Rules = new List<Chain>();
            foreach (var chain in rules)
            {
                Rules.Add(chain);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Rules.Count - 1; i++)
            {
                foreach (var sym in Rules[i].Symbols)
                {
                    sb.Append(sym.Name);
                }
                sb.Append("|");
            }
            foreach (var sym in Rules[Rules.Count - 1].Symbols)
            {
                sb.Append(sym.Name);
            }

            return $"{Key.Name}->{sb}";
        }
    }
}
