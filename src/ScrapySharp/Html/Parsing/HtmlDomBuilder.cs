using System;
using System.Collections.Generic;
using ScrapySharp.Html.Dom;
using System.Linq;

namespace ScrapySharp.Html.Parsing
{
    public class HtmlDomBuilder
    {
        private readonly List<TagDeclaration> tags;

        public HtmlDomBuilder(HtmlDeclarationReader reader)
        {
            tags = new List<TagDeclaration>();

            while (!reader.End)
            {
                var d = reader.ReadTagDeclaration();
                tags.Add(d);
            }
        }

        public IEnumerable<HElement> BuildDom(List<TagDeclaration> declarations, HElement parent)
        {
            for (var i = 0; i < declarations.Count; i++)
            {
                var declaration = declarations[i];

                if (declaration.Type == DeclarationType.Comment)
                {
                    yield return new HComment
                                     {
                                         Name = "!--",
                                         InnerText = declaration.InnerText
                                     };
                }

                if (declaration.Type == DeclarationType.OpenTag)
                {
                    var openning = 1;
                    var closing = 0;
                    var start = i;

                    while (closing < openning && i < declarations.Count)
                    {
                        if (i >= declarations.Count - 1)
                            break;
                        var current = declarations[++i];
                        if (current.Type == DeclarationType.CloseTag && current.Name == declaration.Name)
                            closing++;
                        if (current.Type == DeclarationType.OpenTag && current.Name == declaration.Name)
                            openning++;

                        if (openning == closing)
                        {
                            var childrenTags = declarations.Skip(start+1).Take(i - start - 1).ToList();

                            var element = new HElement
                            {
                                Name = declaration.Name,
                                Attributes = declaration.Attributes,
                                InnerText = declaration.InnerText,
                                ParentNode = parent
                            };
                            var children = declarations.Count > childrenTags.Count ? BuildDom(childrenTags, element).ToList() : new List<HElement>();

                            element.Children = children;

                            yield return element;
                            break;
                        }
                    }

                    if (openning != closing)
                    {
                        var childrenTags = declarations.Skip(start + 1).Take(i - start - 1).ToList();

                        yield return new HElement
                        {
                            Name = declaration.Name,
                            Attributes = declaration.Attributes,
                            InnerText = declaration.InnerText,
                            Children = declarations.Count > childrenTags.Count ? BuildDom(childrenTags, parent).ToList() : new List<HElement>(),
                            ParentNode = parent
                        };
                    }
                }

                if (declaration.Type == DeclarationType.TextElement || declaration.Type == DeclarationType.SelfClosedTag)
                    yield return new HElement
                    {
                        InnerText = declaration.InnerText,
                        Name = declaration.Name,
                        Attributes = declaration.Attributes,
                        ParentNode = parent
                    };
            }
        }

        public IEnumerable<HElement> BuildDom()
        {
            return BuildDom(tags, null);
        }
    }
}