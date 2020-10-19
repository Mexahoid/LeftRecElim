using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeftRecElim.Logics
{
    class RuleDictionary
    {

        private List<Rule> _rules;
        
        public RuleDictionary()
        {
            _rules = new List<Rule>();
        }

        public List<string> GetRules()
        {
            List<string> r = new List<string>();

            foreach (var rule in _rules)
            {
                r.Add(rule.ToString());
            }

            if (r.Count < 1)
                r.Add("Отсутствуют введенные правила");

            return r;
        }
        
        public List<Chain> this[ISymbol key]
        {
            get
            {
                foreach (var rule in _rules)
                {
                    if (key == rule.Key)
                        return rule.Rules;
                }
                return null;
            }
            set
            {
                bool found = false;
                foreach (var rule in _rules)
                {
                    if (key == rule.Key)
                    {
                        rule.Rules = value;
                        found = true;
                    }
                }

                if (!found)
                {
                    _rules.Add(new Rule(key, value));
                }
            }
        }

        public bool ContainsKey(string name)
        {
            foreach (var rule in _rules)
            {
                if (rule.Key.Name == name)
                    return true;
            }

            return false;
        }
    }
}
