using System;

namespace ScrapySharp.Extensions
{
    internal class NodeValueComparer
    {
        private readonly NodeValueComparison comparison;

        public NodeValueComparer(NodeValueComparison comparison)
        {
            this.comparison = comparison;
        }

        public bool Compare(string value1, string value2) =>
            comparison switch
            {
                NodeValueComparison.Equals => value1.Equals(value2, StringComparison.InvariantCultureIgnoreCase),
                NodeValueComparison.StartsWith => value1.StartsWith(value2, StringComparison.InvariantCultureIgnoreCase),
                NodeValueComparison.EndsWith => value1.EndsWith(value2, StringComparison.InvariantCultureIgnoreCase),
                NodeValueComparison.Contains => value1.ToLowerInvariant().Contains(value2.ToLowerInvariant()),
                _ => value1 == value2
            };
    }
}