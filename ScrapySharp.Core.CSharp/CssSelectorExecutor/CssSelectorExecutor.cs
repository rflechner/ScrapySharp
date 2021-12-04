using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ScrapySharp.Core.CSharp
{
    public class CssSelectorExecutor<n>
    {
        internal INavigationProvider<n> navigator;
        internal FSharpList<n> nodes;
        internal FSharpList<Token> tokens;
        internal FilterLevel level;
        internal bool matchAncestors;
        public CssSelectorExecutor(List<n> nodes, List<Token> tokens, INavigationProvider<n> navigator)
        {
            this.navigator = navigator;
            this.nodes = ArrayModule.ToList<n>(nodes.ToArray());
            this.tokens = ArrayModule.ToList<Token>(tokens.ToArray());
            this.level = FilterLevel.Descendants;
            this.matchAncestors = false;
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
            return CssSelectorExecutor.selectElements(this, whiteSpaces, this.nodes, this.tokens);
        }
    }
    internal static class CssSelectorExecutor
    {
        internal static FSharpList<n> getTargets<n>(CssSelectorExecutor<n> x, FSharpList<n> acc)
        {
            FilterLevel level = x.level;
            FilterLevel obj = FilterLevel.Children;
            if (level.Equals(obj, LanguagePrimitives.GenericEqualityComparer))
            {
                return ArrayModule.ToList<n>(x.navigator.ChildNodes(new List<n>(acc)).ToArray());
            }
            level = x.level;
            obj = FilterLevel.Descendants;
            if (level.Equals(obj, LanguagePrimitives.GenericEqualityComparer))
            {
                return ArrayModule.ToList<n>(x.navigator.Descendants(new List<n>(acc)).ToArray());
            }
            level = x.level;
            obj = FilterLevel.Parents;
            if (level.Equals(obj, LanguagePrimitives.GenericEqualityComparer))
            {
                return ArrayModule.ToList<n>(x.navigator.ParentNodes(new List<n>(acc)).ToArray());
            }
            level = x.level;
            obj = FilterLevel.Ancestors;
            if (level.Equals(obj, LanguagePrimitives.GenericEqualityComparer))
            {
                return ArrayModule.ToList<n>(x.navigator.AncestorsAndSelf(new List<n>(acc)).ToArray());
            }
            return acc;
        }
        internal static FSharpList<n> selectElements<n>(CssSelectorExecutor<n> x, char[] whiteSpaces, FSharpList<n> acc, FSharpList<Token> source)
        {
            while (source.TailOrNull != null)
            {
                FSharpList<Token> fsharpList = source;
                switch (fsharpList.HeadOrDefault.Tag)
                {
                    case 0:
                        {
                            Token.ClassPrefix classPrefix = (Token.ClassPrefix)fsharpList.HeadOrDefault;
                            if (fsharpList.TailOrNull.TailOrNull != null)
                            {
                                FSharpList<Token> t = fsharpList.TailOrNull;
                                if (t.HeadOrDefault.Tag == 3)
                                {
                                    Token.CssClass cssClass = (Token.CssClass)t.HeadOrDefault;
                                    FSharpList<Token> tailOrNull = t.TailOrNull;
                                    int o4 = cssClass.item1;
                                    int num = classPrefix.item;
                                    string item = cssClass.item2;
                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes611<n>(x, whiteSpaces, item), selectedNodes));
                                    x.level = FilterLevel.Root;
                                    CssSelectorExecutor<n> cssSelectorExecutor = x;
                                    char[] array = whiteSpaces;
                                    FSharpList<n> fsharpList2 = selectedNodes2;
                                    source = tailOrNull;
                                    acc = fsharpList2;
                                    whiteSpaces = array;
                                    x = cssSelectorExecutor;
                                    continue;
                                }
                                t = fsharpList.TailOrNull;
                            }
                            else
                            {
                                FSharpList<Token> t = fsharpList.TailOrNull;
                            }
                            break;
                        }
                    case 1:
                        {
                            Token.IdPrefix idPrefix = (Token.IdPrefix)fsharpList.HeadOrDefault;
                            if (fsharpList.TailOrNull.TailOrNull != null)
                            {
                                FSharpList<Token> t = fsharpList.TailOrNull;
                                if (t.HeadOrDefault.Tag == 4)
                                {
                                    Token.CssId cssId = (Token.CssId)t.HeadOrDefault;
                                    FSharpList<Token> tailOrNull = t.TailOrNull;
                                    int o4 = cssId.item1;
                                    int num = idPrefix.item;
                                    string item = cssId.item2;
                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes682<n>(x, item), selectedNodes));
                                    x.level = FilterLevel.Root;
                                    CssSelectorExecutor<n> cssSelectorExecutor2 = x;
                                    char[] array2 = whiteSpaces;
                                    FSharpList<n> fsharpList3 = selectedNodes2;
                                    source = tailOrNull;
                                    acc = fsharpList3;
                                    whiteSpaces = array2;
                                    x = cssSelectorExecutor2;
                                    continue;
                                }
                                t = fsharpList.TailOrNull;
                            }
                            else
                            {
                                FSharpList<Token> t = fsharpList.TailOrNull;
                            }
                            break;
                        }
                    case 2:
                        {
                            Token.TagName tagName = (Token.TagName)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = tagName.item1;
                            string item = tagName.item2;
                            FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                            FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(selectedNodes);
                            selectedNodes = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes55<n>(x, item), selectedNodes2));
                            x.level = FilterLevel.Root;
                            CssSelectorExecutor<n> cssSelectorExecutor3 = x;
                            char[] array3 = whiteSpaces;
                            FSharpList<n> fsharpList4 = selectedNodes;
                            source = t;
                            acc = fsharpList4;
                            whiteSpaces = array3;
                            x = cssSelectorExecutor3;
                            continue;
                        }
                    default:
                        {
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            break;
                        }
                    case 5:
                        {
                            Token.AllChildren allChildren = (Token.AllChildren)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = allChildren.item;
                            x.level = ((!x.matchAncestors) ? FilterLevel.Descendants : FilterLevel.Ancestors);
                            CssSelectorExecutor<n> cssSelectorExecutor4 = x;
                            char[] array4 = whiteSpaces;
                            FSharpList<n> fsharpList5 = acc;
                            source = t;
                            acc = fsharpList5;
                            whiteSpaces = array4;
                            x = cssSelectorExecutor4;
                            continue;
                        }
                    case 6:
                        {
                            Token.OpenAttribute openAttribute = (Token.OpenAttribute)fsharpList.HeadOrDefault;
                            if (fsharpList.TailOrNull.TailOrNull != null)
                            {
                                FSharpList<Token> t = fsharpList.TailOrNull;
                                if (t.HeadOrDefault.Tag == 8)
                                {
                                    Token.AttributeName attributeName = (Token.AttributeName)t.HeadOrDefault;
                                    if (t.TailOrNull.TailOrNull != null)
                                    {
                                        FSharpList<Token> tailOrNull = t.TailOrNull;
                                        switch (tailOrNull.HeadOrDefault.Tag)
                                        {
                                            default:
                                                t = fsharpList.TailOrNull;
                                                break;
                                            case 10:
                                                {
                                                    Token.Assign assign = (Token.Assign)tailOrNull.HeadOrDefault;
                                                    if (tailOrNull.TailOrNull.TailOrNull != null)
                                                    {
                                                        FSharpList<Token> tailOrNull2 = tailOrNull.TailOrNull;
                                                        if (tailOrNull2.HeadOrDefault.Tag == 9)
                                                        {
                                                            Token.AttributeValue attributeValue = (Token.AttributeValue)tailOrNull2.HeadOrDefault;
                                                            if (tailOrNull2.TailOrNull.TailOrNull != null)
                                                            {
                                                                FSharpList<Token> tailOrNull3 = tailOrNull2.TailOrNull;
                                                                if (tailOrNull3.HeadOrDefault.Tag == 7)
                                                                {
                                                                    Token.CloseAttribute closeAttribute = (Token.CloseAttribute)tailOrNull3.HeadOrDefault;
                                                                    string item = attributeValue.item2;
                                                                    FSharpList<Token> tailOrNull4 = tailOrNull3.TailOrNull;
                                                                    int o4 = closeAttribute.item;
                                                                    int num = attributeValue.item1;
                                                                    int item2 = assign.item;
                                                                    int item3 = attributeName.item1;
                                                                    int item4 = openAttribute.item;
                                                                    string item5 = attributeName.item2;
                                                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes753<n>(x, item, item5), selectedNodes));
                                                                    x.level = FilterLevel.Root;
                                                                    CssSelectorExecutor<n> cssSelectorExecutor5 = x;
                                                                    char[] array5 = whiteSpaces;
                                                                    FSharpList<n> fsharpList6 = selectedNodes2;
                                                                    source = tailOrNull4;
                                                                    acc = fsharpList6;
                                                                    whiteSpaces = array5;
                                                                    x = cssSelectorExecutor5;
                                                                    continue;
                                                                }
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                            else
                                                            {
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            t = fsharpList.TailOrNull;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        t = fsharpList.TailOrNull;
                                                    }
                                                    break;
                                                }
                                            case 11:
                                                {
                                                    Token.EndWith endWith = (Token.EndWith)tailOrNull.HeadOrDefault;
                                                    if (tailOrNull.TailOrNull.TailOrNull != null)
                                                    {
                                                        FSharpList<Token> tailOrNull2 = tailOrNull.TailOrNull;
                                                        if (tailOrNull2.HeadOrDefault.Tag == 9)
                                                        {
                                                            Token.AttributeValue attributeValue = (Token.AttributeValue)tailOrNull2.HeadOrDefault;
                                                            if (tailOrNull2.TailOrNull.TailOrNull != null)
                                                            {
                                                                FSharpList<Token> tailOrNull3 = tailOrNull2.TailOrNull;
                                                                if (tailOrNull3.HeadOrDefault.Tag == 7)
                                                                {
                                                                    Token.CloseAttribute closeAttribute = (Token.CloseAttribute)tailOrNull3.HeadOrDefault;
                                                                    string item = attributeValue.item2;
                                                                    FSharpList<Token> tailOrNull4 = tailOrNull3.TailOrNull;
                                                                    int o4 = closeAttribute.item;
                                                                    int num = attributeValue.item1;
                                                                    int item2 = endWith.item;
                                                                    int item3 = attributeName.item1;
                                                                    int item4 = openAttribute.item;
                                                                    string item5 = attributeName.item2;
                                                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes824<n>(x, item, item5), selectedNodes));
                                                                    x.level = FilterLevel.Root;
                                                                    CssSelectorExecutor<n> cssSelectorExecutor6 = x;
                                                                    char[] array6 = whiteSpaces;
                                                                    FSharpList<n> fsharpList7 = selectedNodes2;
                                                                    source = tailOrNull4;
                                                                    acc = fsharpList7;
                                                                    whiteSpaces = array6;
                                                                    x = cssSelectorExecutor6;
                                                                    continue;
                                                                }
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                            else
                                                            {
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            t = fsharpList.TailOrNull;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        t = fsharpList.TailOrNull;
                                                    }
                                                    break;
                                                }
                                            case 12:
                                                {
                                                    Token.StartWith startWith = (Token.StartWith)tailOrNull.HeadOrDefault;
                                                    if (tailOrNull.TailOrNull.TailOrNull != null)
                                                    {
                                                        FSharpList<Token> tailOrNull2 = tailOrNull.TailOrNull;
                                                        if (tailOrNull2.HeadOrDefault.Tag == 9)
                                                        {
                                                            Token.AttributeValue attributeValue = (Token.AttributeValue)tailOrNull2.HeadOrDefault;
                                                            if (tailOrNull2.TailOrNull.TailOrNull != null)
                                                            {
                                                                FSharpList<Token> tailOrNull3 = tailOrNull2.TailOrNull;
                                                                if (tailOrNull3.HeadOrDefault.Tag == 7)
                                                                {
                                                                    Token.CloseAttribute closeAttribute = (Token.CloseAttribute)tailOrNull3.HeadOrDefault;
                                                                    string item = attributeValue.item2;
                                                                    FSharpList<Token> tailOrNull4 = tailOrNull3.TailOrNull;
                                                                    int o4 = closeAttribute.item;
                                                                    int num = attributeValue.item1;
                                                                    int item2 = startWith.item;
                                                                    int item3 = attributeName.item1;
                                                                    int item4 = openAttribute.item;
                                                                    string item5 = attributeName.item2;
                                                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes895<n>(x, item, item5), selectedNodes));
                                                                    x.level = FilterLevel.Root;
                                                                    CssSelectorExecutor<n> cssSelectorExecutor7 = x;
                                                                    char[] array7 = whiteSpaces;
                                                                    FSharpList<n> fsharpList8 = selectedNodes2;
                                                                    source = tailOrNull4;
                                                                    acc = fsharpList8;
                                                                    whiteSpaces = array7;
                                                                    x = cssSelectorExecutor7;
                                                                    continue;
                                                                }
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                            else
                                                            {
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            t = fsharpList.TailOrNull;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        t = fsharpList.TailOrNull;
                                                    }
                                                    break;
                                                }
                                            case 15:
                                                {
                                                    Token.AttributeContainsPrefix attributeContainsPrefix = (Token.AttributeContainsPrefix)tailOrNull.HeadOrDefault;
                                                    if (tailOrNull.TailOrNull.TailOrNull != null)
                                                    {
                                                        FSharpList<Token> tailOrNull2 = tailOrNull.TailOrNull;
                                                        if (tailOrNull2.HeadOrDefault.Tag == 9)
                                                        {
                                                            Token.AttributeValue attributeValue = (Token.AttributeValue)tailOrNull2.HeadOrDefault;
                                                            if (tailOrNull2.TailOrNull.TailOrNull != null)
                                                            {
                                                                FSharpList<Token> tailOrNull3 = tailOrNull2.TailOrNull;
                                                                if (tailOrNull3.HeadOrDefault.Tag == 7)
                                                                {
                                                                    Token.CloseAttribute closeAttribute = (Token.CloseAttribute)tailOrNull3.HeadOrDefault;
                                                                    string item = attributeValue.item2;
                                                                    FSharpList<Token> tailOrNull4 = tailOrNull3.TailOrNull;
                                                                    int o4 = closeAttribute.item;
                                                                    int num = attributeValue.item1;
                                                                    int item2 = attributeContainsPrefix.item;
                                                                    int item3 = attributeName.item1;
                                                                    int item4 = openAttribute.item;
                                                                    string item5 = attributeName.item2;
                                                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes966<n>(x, item, item5), selectedNodes));
                                                                    x.level = FilterLevel.Root;
                                                                    CssSelectorExecutor<n> cssSelectorExecutor8 = x;
                                                                    char[] array8 = whiteSpaces;
                                                                    FSharpList<n> fsharpList9 = selectedNodes2;
                                                                    source = tailOrNull4;
                                                                    acc = fsharpList9;
                                                                    whiteSpaces = array8;
                                                                    x = cssSelectorExecutor8;
                                                                    continue;
                                                                }
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                            else
                                                            {
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            t = fsharpList.TailOrNull;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        t = fsharpList.TailOrNull;
                                                    }
                                                    break;
                                                }
                                            case 16:
                                                {
                                                    Token.AttributeContains attributeContains = (Token.AttributeContains)tailOrNull.HeadOrDefault;
                                                    if (tailOrNull.TailOrNull.TailOrNull != null)
                                                    {
                                                        FSharpList<Token> tailOrNull2 = tailOrNull.TailOrNull;
                                                        if (tailOrNull2.HeadOrDefault.Tag == 9)
                                                        {
                                                            Token.AttributeValue attributeValue = (Token.AttributeValue)tailOrNull2.HeadOrDefault;
                                                            if (tailOrNull2.TailOrNull.TailOrNull != null)
                                                            {
                                                                FSharpList<Token> tailOrNull3 = tailOrNull2.TailOrNull;
                                                                if (tailOrNull3.HeadOrDefault.Tag == 7)
                                                                {
                                                                    Token.CloseAttribute closeAttribute = (Token.CloseAttribute)tailOrNull3.HeadOrDefault;
                                                                    string item = attributeValue.item2;
                                                                    FSharpList<Token> tailOrNull4 = tailOrNull3.TailOrNull;
                                                                    int o4 = closeAttribute.item;
                                                                    int num = attributeValue.item1;
                                                                    int item2 = attributeContains.item;
                                                                    int item3 = attributeName.item1;
                                                                    int item4 = openAttribute.item;
                                                                    string item5 = attributeName.item2;
                                                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes1037<n>(x, item, item5), selectedNodes));
                                                                    x.level = FilterLevel.Root;
                                                                    CssSelectorExecutor<n> cssSelectorExecutor9 = x;
                                                                    char[] array9 = whiteSpaces;
                                                                    FSharpList<n> fsharpList10 = selectedNodes2;
                                                                    source = tailOrNull4;
                                                                    acc = fsharpList10;
                                                                    whiteSpaces = array9;
                                                                    x = cssSelectorExecutor9;
                                                                    continue;
                                                                }
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                            else
                                                            {
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            t = fsharpList.TailOrNull;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        t = fsharpList.TailOrNull;
                                                    }
                                                    break;
                                                }
                                            case 17:
                                                {
                                                    Token.AttributeContainsWord attributeContainsWord = (Token.AttributeContainsWord)tailOrNull.HeadOrDefault;
                                                    if (tailOrNull.TailOrNull.TailOrNull != null)
                                                    {
                                                        FSharpList<Token> tailOrNull2 = tailOrNull.TailOrNull;
                                                        if (tailOrNull2.HeadOrDefault.Tag == 9)
                                                        {
                                                            Token.AttributeValue attributeValue = (Token.AttributeValue)tailOrNull2.HeadOrDefault;
                                                            if (tailOrNull2.TailOrNull.TailOrNull != null)
                                                            {
                                                                FSharpList<Token> tailOrNull3 = tailOrNull2.TailOrNull;
                                                                if (tailOrNull3.HeadOrDefault.Tag == 7)
                                                                {
                                                                    Token.CloseAttribute closeAttribute = (Token.CloseAttribute)tailOrNull3.HeadOrDefault;
                                                                    string item = attributeValue.item2;
                                                                    FSharpList<Token> tailOrNull4 = tailOrNull3.TailOrNull;
                                                                    int o4 = closeAttribute.item;
                                                                    int num = attributeValue.item1;
                                                                    int item2 = attributeContainsWord.item;
                                                                    int item3 = attributeName.item1;
                                                                    int item4 = openAttribute.item;
                                                                    string item5 = attributeName.item2;
                                                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes1108<n>(x, whiteSpaces, item, item5), selectedNodes));
                                                                    x.level = FilterLevel.Root;
                                                                    CssSelectorExecutor<n> cssSelectorExecutor10 = x;
                                                                    char[] array10 = whiteSpaces;
                                                                    FSharpList<n> fsharpList11 = selectedNodes2;
                                                                    source = tailOrNull4;
                                                                    acc = fsharpList11;
                                                                    whiteSpaces = array10;
                                                                    x = cssSelectorExecutor10;
                                                                    continue;
                                                                }
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                            else
                                                            {
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            t = fsharpList.TailOrNull;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        t = fsharpList.TailOrNull;
                                                    }
                                                    break;
                                                }
                                            case 18:
                                                {
                                                    Token.AttributeNotEqual attributeNotEqual = (Token.AttributeNotEqual)tailOrNull.HeadOrDefault;
                                                    if (tailOrNull.TailOrNull.TailOrNull != null)
                                                    {
                                                        FSharpList<Token> tailOrNull2 = tailOrNull.TailOrNull;
                                                        if (tailOrNull2.HeadOrDefault.Tag == 9)
                                                        {
                                                            Token.AttributeValue attributeValue = (Token.AttributeValue)tailOrNull2.HeadOrDefault;
                                                            if (tailOrNull2.TailOrNull.TailOrNull != null)
                                                            {
                                                                FSharpList<Token> tailOrNull3 = tailOrNull2.TailOrNull;
                                                                if (tailOrNull3.HeadOrDefault.Tag == 7)
                                                                {
                                                                    Token.CloseAttribute closeAttribute = (Token.CloseAttribute)tailOrNull3.HeadOrDefault;
                                                                    string item = attributeValue.item2;
                                                                    FSharpList<Token> tailOrNull4 = tailOrNull3.TailOrNull;
                                                                    int o4 = closeAttribute.item;
                                                                    int num = attributeValue.item1;
                                                                    int item2 = attributeNotEqual.item;
                                                                    int item3 = attributeName.item1;
                                                                    int item4 = openAttribute.item;
                                                                    string item5 = attributeName.item2;
                                                                    FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                                                                    FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes11910<n>(x, item, item5), selectedNodes));
                                                                    x.level = FilterLevel.Root;
                                                                    CssSelectorExecutor<n> cssSelectorExecutor11 = x;
                                                                    char[] array11 = whiteSpaces;
                                                                    FSharpList<n> fsharpList12 = selectedNodes2;
                                                                    source = tailOrNull4;
                                                                    acc = fsharpList12;
                                                                    whiteSpaces = array11;
                                                                    x = cssSelectorExecutor11;
                                                                    continue;
                                                                }
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                            else
                                                            {
                                                                t = fsharpList.TailOrNull;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            t = fsharpList.TailOrNull;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        t = fsharpList.TailOrNull;
                                                    }
                                                    break;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        t = fsharpList.TailOrNull;
                                    }
                                }
                                else
                                {
                                    t = fsharpList.TailOrNull;
                                }
                            }
                            else
                            {
                                FSharpList<Token> t = fsharpList.TailOrNull;
                            }
                            break;
                        }
                    case 13:
                        {
                            Token.DirectChildren directChildren = (Token.DirectChildren)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = directChildren.item;
                            x.level = ((!x.matchAncestors) ? FilterLevel.Children : FilterLevel.Parents);
                            CssSelectorExecutor<n> cssSelectorExecutor12 = x;
                            char[] array12 = whiteSpaces;
                            FSharpList<n> fsharpList13 = acc;
                            source = t;
                            acc = fsharpList13;
                            whiteSpaces = array12;
                            x = cssSelectorExecutor12;
                            continue;
                        }
                    case 14:
                        {
                            Token.Ancestor ancestor = (Token.Ancestor)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = ancestor.item;
                            x.level = FilterLevel.Ancestors;
                            CssSelectorExecutor<n> cssSelectorExecutor13 = x;
                            char[] array13 = whiteSpaces;
                            FSharpList<n> fsharpList14 = acc;
                            source = t;
                            acc = fsharpList14;
                            whiteSpaces = array13;
                            x = cssSelectorExecutor13;
                            continue;
                        }
                    case 19:
                        {
                            Token.Checkbox checkbox = (Token.Checkbox)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = checkbox.item;
                            FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                            FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes12611<n>(x), selectedNodes));
                            x.level = FilterLevel.Root;
                            CssSelectorExecutor<n> cssSelectorExecutor14 = x;
                            char[] array14 = whiteSpaces;
                            FSharpList<n> fsharpList15 = selectedNodes2;
                            source = t;
                            acc = fsharpList15;
                            whiteSpaces = array14;
                            x = cssSelectorExecutor14;
                            continue;
                        }
                    case 20:
                        {
                            Token.Checked checkeds = (Token.Checked)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = checkeds.item;
                            FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                            FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes13312<n>(x), selectedNodes));
                            x.level = FilterLevel.Root;
                            CssSelectorExecutor<n> cssSelectorExecutor15 = x;
                            char[] array15 = whiteSpaces;
                            FSharpList<n> fsharpList16 = selectedNodes2;
                            source = t;
                            acc = fsharpList16;
                            whiteSpaces = array15;
                            x = cssSelectorExecutor15;
                            continue;
                        }
                    case 21:
                        {
                            Token.Disabled disabled = (Token.Disabled)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = disabled.item;
                            FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                            FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes14714<n>(x), selectedNodes));
                            x.level = FilterLevel.Root;
                            CssSelectorExecutor<n> cssSelectorExecutor16 = x;
                            char[] array16 = whiteSpaces;
                            FSharpList<n> fsharpList17 = selectedNodes2;
                            source = t;
                            acc = fsharpList17;
                            whiteSpaces = array16;
                            x = cssSelectorExecutor16;
                            continue;
                        }
                    case 22:
                        {
                            Token.Enabled enabled = (Token.Enabled)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = enabled.item;
                            FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                            FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes15415<n>(x), selectedNodes));
                            x.level = FilterLevel.Root;
                            CssSelectorExecutor<n> cssSelectorExecutor17 = x;
                            char[] array17 = whiteSpaces;
                            FSharpList<n> fsharpList18 = selectedNodes2;
                            source = t;
                            acc = fsharpList18;
                            whiteSpaces = array17;
                            x = cssSelectorExecutor17;
                            continue;
                        }
                    case 23:
                        {
                            Token.Selected selected = (Token.Selected)fsharpList.HeadOrDefault;
                            FSharpList<Token> t = fsharpList.TailOrNull;
                            int o4 = selected.item;
                            FSharpList<n> selectedNodes = CssSelectorExecutor.getTargets<n>(x, acc);
                            FSharpList<n> selectedNodes2 = SeqModule.ToList<n>(SeqModule.Filter<n>(new CssSelectorExecutor.selectedNodes14013<n>(x), selectedNodes));
                            x.level = FilterLevel.Root;
                            CssSelectorExecutor<n> cssSelectorExecutor18 = x;
                            char[] array18 = whiteSpaces;
                            FSharpList<n> fsharpList19 = selectedNodes2;
                            source = t;
                            acc = fsharpList19;
                            whiteSpaces = array18;
                            x = cssSelectorExecutor18;
                            continue;
                        }
                }
                throw new Exception("Invalid token");
            }
            return acc;
        }
        internal sealed class selectedNodes55<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string name;
            internal selectedNodes55(CssSelectorExecutor<n> x0, string name)
            {
                this.x0 = x0;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.GetName(x).Equals(this.name, StringComparison.InvariantCultureIgnoreCase);
            }
        }
        internal sealed class selectedNodes611<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public char[] whiteSpaces;
            public string className;
            internal selectedNodes611(CssSelectorExecutor<n> x0, char[] whiteSpaces, string className)
            {
                this.x0 = x0;
                this.whiteSpaces = whiteSpaces;
                this.className = className;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.GetAttributeValue(x, "class", string.Empty).Split(this.whiteSpaces).Contains(this.className);
            }
        }
        internal sealed class selectedNodes682<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string id;
            internal selectedNodes682(CssSelectorExecutor<n> x0, string id)
            {
                this.x0 = x0;
                this.id = id;
            }
            public override bool Invoke(n x)
            {
                return string.Equals(this.x0.navigator.GetId(x), this.id);
            }
        }
        internal sealed class selectedNodes753<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string value;
            public string name;
            internal selectedNodes753(CssSelectorExecutor<n> x0, string value, string name)
            {
                this.x0 = x0;
                this.value = value;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return string.Equals(this.x0.navigator.GetAttributeValue(x, this.name, string.Empty), this.value);
            }
        }
        internal sealed class selectedNodes824<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string value;
            public string name;
            internal selectedNodes824(CssSelectorExecutor<n> x0, string value, string name)
            {
                this.x0 = x0;
                this.value = value;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.GetAttributeValue(x, this.name, string.Empty).EndsWith(this.value);
            }
        }
        internal sealed class selectedNodes895<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string value;
            public string name;
            internal selectedNodes895(CssSelectorExecutor<n> x0, string value, string name)
            {
                this.x0 = x0;
                this.value = value;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.GetAttributeValue(x, this.name, string.Empty).StartsWith(this.value);
            }
        }
        internal sealed class selectedNodes966<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string value;
            public string name;
            internal selectedNodes966(CssSelectorExecutor<n> x0, string value, string name)
            {
                this.x0 = x0;
                this.value = value;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.GetAttributeValue(x, this.name, string.Empty).StartsWith(this.value);
            }
        }
        internal sealed class selectedNodes1037<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string value;
            public string name;
            internal selectedNodes1037(CssSelectorExecutor<n> x0, string value, string name)
            {
                this.x0 = x0;
                this.value = value;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.GetAttributeValue(x, this.name, string.Empty).ToLowerInvariant().Contains(this.value.ToLowerInvariant());
            }
        }
        internal sealed class selectedNodes1129
        {
            public string value;
            public selectedNodes1129(string value)
            {
                this.value = value;
            }
            internal bool Invoke(string s)
            {
                return s.Equals(this.value, StringComparison.InvariantCultureIgnoreCase);
            }
        }
        internal sealed class selectedNodes1108<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public char[] whiteSpaces;
            public string value;
            public string name;
            internal selectedNodes1108(CssSelectorExecutor<n> x0, char[] whiteSpaces, string value, string name)
            {
                this.x0 = x0;
                this.whiteSpaces = whiteSpaces;
                this.value = value;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.GetAttributeValue(x, this.name, string.Empty).Split(this.whiteSpaces).Any(new Func<string, bool>(new CssSelectorExecutor.selectedNodes1129(this.value).Invoke));
            }
        }
        internal sealed class selectedNodes11910<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            public string value;
            public string name;
            internal selectedNodes11910(CssSelectorExecutor<n> x0, string value, string name)
            {
                this.x0 = x0;
                this.value = value;
                this.name = name;
            }
            public override bool Invoke(n x)
            {
                return !string.Equals(this.x0.navigator.GetAttributeValue(x, this.name, string.Empty), this.value);
            }
        }
        internal sealed class selectedNodes12611<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            internal selectedNodes12611(CssSelectorExecutor<n> x0)
            {
                this.x0 = x0;
            }
            public override bool Invoke(n x)
            {
                return string.Equals(this.x0.navigator.GetAttributeValue(x, "type", string.Empty), "checkbox");
            }
        }
        internal sealed class selectedNodes13312<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            internal selectedNodes13312(CssSelectorExecutor<n> x0)
            {
                this.x0 = x0;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.Attributes(x).AllKeys.Contains("checked");
            }
        }
        internal sealed class selectedNodes14013<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            internal selectedNodes14013(CssSelectorExecutor<n> x0)
            {
                this.x0 = x0;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.Attributes(x).AllKeys.Contains("selected");
            }
        }
        internal sealed class selectedNodes14714<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            internal selectedNodes14714(CssSelectorExecutor<n> x0)
            {
                this.x0 = x0;
            }
            public override bool Invoke(n x)
            {
                return this.x0.navigator.Attributes(x).AllKeys.Contains("disabled");
            }
        }
        internal sealed class selectedNodes15415<n> : FSharpFunc<n, bool>
        {
            public CssSelectorExecutor<n> x0;
            internal selectedNodes15415(CssSelectorExecutor<n> x0)
            {
                this.x0 = x0;
            }
            public override bool Invoke(n x)
            {
                return !this.x0.navigator.Attributes(x).AllKeys.Contains("disabled");
            }
        }
    }
}