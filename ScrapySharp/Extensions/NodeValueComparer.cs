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

        public bool Compare(string value1, string value2)
        {
            switch (comparison)
            {
                case NodeValueComparison.Equals:
                    return value1.Equals(value2, StringComparison.InvariantCultureIgnoreCase);
                case NodeValueComparison.StartsWith:
                    return value1.StartsWith(value2, StringComparison.InvariantCultureIgnoreCase);
                case NodeValueComparison.EndsWith:
                    return value1.EndsWith(value2, StringComparison.InvariantCultureIgnoreCase);
                case NodeValueComparison.Contains:
                    return value1.ToLowerInvariant().Contains(value2.ToLowerInvariant());
                default:
                    return value1 == value2;
            }
        }
    }
}