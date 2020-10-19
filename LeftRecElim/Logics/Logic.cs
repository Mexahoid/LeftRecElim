using System;
using System.Collections.Generic;

namespace LeftRecElim.Logics
{
    class Logic
    {
        private RuleDictionary _grammar;

        private List<ISymbol> _terminals;
        private List<ISymbol> _nonTerminals;

        public Logic()
        {
            _grammar = new RuleDictionary();
            _terminals = new List<ISymbol>();
            _nonTerminals = new List<ISymbol>();

            //_terminals.Add(new Terminal("ϵ"));
        }

        public void AddSymbols(string symbols, bool terminals)
        {
            var sp = symbols.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in sp)
            {
                if (terminals)
                {
                    _terminals.Add(new Terminal(s));
                }
                else
                {
                    _nonTerminals.Add(new NonTerminal(s));
                }
            }
        }

        public void AddRules(string rules)
        {
            var sp = rules.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in sp)
            {
                AddRule(s);
            }
        }

        private ISymbol GetSymbol(string name, bool terminal)
        {
            var t = terminal ? _terminals : _nonTerminals;

            foreach (var symbol in t)
            {
                if (symbol.Name == name.Trim())
                    return symbol;
            }
            return null;
        }

        private void AddRule(string rule)
        {
            if (rule.Length < 1)
                return;

            var sp = rule.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
            if (sp.Length < 2)
                return;

            ISymbol sym = GetSymbol(sp[0], false);
            if (sym == null)
                return;


            var right = sp[1].Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

            List<Chain> syms = new List<Chain>();

            foreach (var chain in right)
            {
                Chain ch = new Chain();
                foreach (char symb in chain)
                {
                    ISymbol csm = GetSymbol(symb.ToString(), true);
                    if (csm == null)
                        csm = GetSymbol(symb.ToString(), false);

                    if (csm == null)
                        continue;

                    ch.Symbols.Add(csm);
                }

                syms.Add(ch);
            }

            _grammar[sym] = syms;
        }

        public List<string> GetRules()
        {
            return _grammar.GetRules();
        }

        public void ElimTest()
        {
            List<Rule> tmp = new List<Rule>();
            List<ISymbol> nts = new List<ISymbol>();

            foreach (var nonTerminal in _nonTerminals)
            {
                nts.Add(nonTerminal);
            }

            for (int i = 0; i < nts.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    List<Chain> test = new List<Chain>();

                    for (int k = 0; k < _grammar[_nonTerminals[i]].Count; k++)
                    {
                        if (_grammar[_nonTerminals[i]][k].Count > 0 && _grammar[_nonTerminals[i]][k].Symbols[0] == _nonTerminals[j])
                        {
                            


                            for (int l = 0; l < _grammar[_nonTerminals[j]].Count; l++)
                            {
                                Chain ch = new Chain();
                                for (int m = 0; m < _grammar[_nonTerminals[j]][l].Symbols.Count; m++)
                                {
                                    ch.Symbols.Add(_grammar[_nonTerminals[j]][l].Symbols[m]);
                                }

                                for (int m = 1; m < _grammar[_nonTerminals[i]][k].Symbols.Count; m++)
                                {
                                    ch.Symbols.Add(_grammar[_nonTerminals[i]][k].Symbols[m]);
                                }
                                test.Add(ch);
                            }

                        }
                        else
                        {
                            if (_grammar[_nonTerminals[i]][k].Count > 0)
                            {
                                Chain ch = new Chain();
                                for (int l = 0; l < _grammar[_nonTerminals[i]][k].Count; l++)
                                {
                                    ch.Symbols.Add(_grammar[_nonTerminals[i]][k].Symbols[l]);
                                }
                                test.Add(ch);
                            }
                        }
                    }

                    _grammar[_nonTerminals[i]] = test;
                }


                bool hasDirect = false;


                for (int k = 0; k < _grammar[_nonTerminals[i]].Count; k++)
                {
                    if (_grammar[_nonTerminals[i]][k].Count > 0 && _grammar[_nonTerminals[i]][k].Symbols[0] == _nonTerminals[i])
                    {
                        hasDirect = true;
                        break;
                    }
                }

                if (hasDirect)
                {
                    string helperName = _nonTerminals[i].Name + "'";

                    while (_grammar.ContainsKey(helperName))
                        helperName += "'";

                    ISymbol helper = new NonTerminal(helperName);
                    _nonTerminals.Add(helper);

                    List<Chain> newChains = new List<Chain>();
                    int j = 0;
                    for (int k = 0; k < _grammar[_nonTerminals[i]].Count; k++)
                    {
                        Chain chh = new Chain();
                        if (_grammar[_nonTerminals[i]][k].Count > 0)
                        {
                            if (_grammar[_nonTerminals[i]][k].Symbols[0] == _nonTerminals[i])
                            {
                                for (int l = 1; l < _grammar[_nonTerminals[i]][k].Symbols.Count; l++)
                                {
                                    chh.Symbols.Add(_grammar[_nonTerminals[i]][k].Symbols[l]);
                                }
                                chh.Symbols.Add(helper);

                                if (_grammar[helper] == null)
                                {
                                    newChains.Add(chh);
                                    _grammar[helper] = newChains;
                                }
                                else
                                {
                                    _grammar[helper].Add(chh);
                                }

                            }
                            else
                            {
                                if (_grammar[_nonTerminals[i]][k].Count == 1 &&
                                    _grammar[_nonTerminals[i]][k].Symbols[0] == _terminals[0])
                                {
                                    _grammar[_nonTerminals[i]][k].Symbols.Clear();
                                    _grammar[_nonTerminals[i]][k].Symbols.Add(helper);
                                }
                                else
                                {
                                    _grammar[_nonTerminals[i]][k].Symbols.Add(helper);
                                }

                                _grammar[_nonTerminals[i]][j] = _grammar[_nonTerminals[i]][k];
                                j++;
                            }

                        }
                    }

                    List<Chain> kek = new List<Chain>();
                    for (int z = 0; z < j; z++)
                    {
                        kek.Add(_grammar[_nonTerminals[i]][z]);
                    }
                    _grammar[_nonTerminals[i]] = kek;

                    Chain eps = new Chain();
                    eps.Symbols.Add(_terminals[0]);
                    var a = _grammar[helper];
                    if (a == null)
                    {
                        kek = new List<Chain>();
                        kek.Add(eps);
                        _grammar[helper] = kek;
                    }
                    else
                        _grammar[helper].Add(eps);
                }
            }
        }
        private bool IsRecursive(List<Chain> chains, ISymbol nonTerm)
        {
            foreach (var chain in chains)
            {
                if (chain.Contains(nonTerm))
                    return true;
            }
            return false;
        }

        private bool IsRecursive(Chain chain, ISymbol nonTerm)
        {
            return chain.Contains(nonTerm);
        }

    }
}