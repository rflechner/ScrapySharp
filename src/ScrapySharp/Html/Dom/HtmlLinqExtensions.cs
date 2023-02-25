using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrapySharp.Html.Dom
{
    public static class HtmlLinqExtensions
    {
        public static IEnumerable<HElement> Descendants(this HContainer container, string name)
        {
            foreach (var element in container.Elements(name))
            {
                yield return element;
            }

            foreach (var child in container.Children)
            {
                foreach (var element in child.Descendants(name))
                {
                    yield return element;
                }
            }
        }
        
        public static IEnumerable<HElement> Descendants(this HContainer container)
        {
            foreach (var element in container.Children)
            {
                yield return element;
            }

            foreach (var child in container.Children)
            {
                foreach (var element in child.Descendants())
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<HElement> Elements(this IEnumerable<HContainer> containers, string name)
        {
            return containers.SelectMany(c => c.Elements(name));
        }

        public static IEnumerable<HElement> Elements(this HContainer container, string name)
        {
            if (container.Children == null)
                return new HElement[0];

            return container.Children.Where(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}