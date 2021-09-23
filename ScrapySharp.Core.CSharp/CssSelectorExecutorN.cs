using Microsoft.FSharp.Collections;
using System.Collections.Generic;
using System;

namespace ScrapySharp.Core.CSharp
{
    public class CssSelectorExecutor<n>
    {
        public CssSelectorExecutor(List<n> nodes, List<Token> tokens, INavigationProvider<n> navigator) : this()
        {
            this.navigator18 = navigator;
            this.nodes19 = ArrayModule.ToList<n>(nodes.ToArray());
            this.tokens20 = ArrayModule.ToList<Token>(tokens.ToArray());
            this.level = FilterLevel.Descendants;
            this.matchAncestors = false;
        }

        public CssSelectorExecutor()
        {
        }

        public bool MatchAncestors
        {
            get
            {
                return this.matchAncestors;
            }
            set
            {
                this.matchAncestors = value;
                this.level = ((!this.matchAncestors) ? FilterLevel.Root : FilterLevel.Ancestors);
            }
        }
        public n[] GetElements()
        {
            return ListModule.ToArray<n>(this.selectElements());
        }
        internal FSharpList<n> selectElements()
        {
            char[] whiteSpaces = new char[]
            {
                ' ',
                '\t',
                '\r',
                '\n'
            };
            return CssSelectorExecutor.selectElements51(this, whiteSpaces, this.nodes19, this.tokens20);
        }
        internal INavigationProvider<n> navigator18;
        internal FSharpList<n> nodes19;
        internal FSharpList<Token> tokens20;
        internal FilterLevel level;
        internal bool matchAncestors;
    }
}