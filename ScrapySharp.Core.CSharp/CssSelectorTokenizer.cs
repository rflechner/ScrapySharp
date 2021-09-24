using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using System;

namespace ScrapySharp.Core.CSharp
{
    public class CssSelectorTokenizer
    {
        internal static Tuple<string, FSharpList<char>> readString26(CssSelectorTokenizer x, string acc, FSharpList<char> _arg1)
        {
            char c;
            FSharpList<char> t;
            FSharpList<char> t2;
            for (; ; )
            {
                if (_arg1.TailOrNull != null)
                {
                    FSharpList<char> fsharpList = _arg1;
                    c = fsharpList.HeadOrDefault;
                    if (char.IsLetterOrDigit(c))
                    {
                        goto IL_1C;
                    }
                    if (c.Equals('-'))
                    {
                        goto IL_1C;
                    }
                    if (c.Equals('_'))
                    {
                        goto IL_1C;
                    }
                    if (c.Equals('+'))
                    {
                        goto IL_1C;
                    }
                    bool flag = c.Equals('/');
                IL_51:
                    if (flag)
                    {
                        FSharpList<char> fsharpList2 = fsharpList.TailOrNull;
                        c = fsharpList.HeadOrDefault;
                        CssSelectorTokenizer cssSelectorTokenizer = x;
                        string text = acc + c.ToString();
                        _arg1 = fsharpList2;
                        acc = text;
                        x = cssSelectorTokenizer;
                        continue;
                    }
                    goto IL_81;
                IL_1C:
                    flag = true;
                    goto IL_51;
                }
            IL_81:
                if (_arg1.TailOrNull != null)
                {
                    FSharpList<char> fsharpList2 = _arg1;
                    switch (fsharpList2.HeadOrDefault)
                    {
                        case '\'':
                            {
                                t = fsharpList2.TailOrNull;
                                if (x.inQuotes)
                                {
                                    goto Block_13;
                                }
                                x.inQuotes = true;
                                CssSelectorTokenizer cssSelectorTokenizer2 = x;
                                string text2 = acc;
                                _arg1 = t;
                                acc = text2;
                                x = cssSelectorTokenizer2;
                                continue;
                            }
                        default:
                            switch (fsharpList2.HeadOrDefault)
                            {
                                case '\\':
                                    if (fsharpList2.TailOrNull.TailOrNull != null)
                                    {
                                        t = fsharpList2.TailOrNull;
                                        switch (t.HeadOrDefault)
                                        {
                                            case '\'':
                                                if (x.inQuotes)
                                                {
                                                    t2 = t.TailOrNull;
                                                    CssSelectorTokenizer cssSelectorTokenizer3 = x;
                                                    string text3 = acc + '\''.ToString();
                                                    _arg1 = t2;
                                                    acc = text3;
                                                    x = cssSelectorTokenizer3;
                                                    continue;
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
                if (_arg1.TailOrNull == null)
                {
                    break;
                }
                t = _arg1;
                if (!x.inQuotes)
                {
                    break;
                }
                t2 = t.TailOrNull;
                c = t.HeadOrDefault;
                CssSelectorTokenizer cssSelectorTokenizer4 = x;
                string text4 = acc + c.ToString();
                _arg1 = t2;
                acc = text4;
                x = cssSelectorTokenizer4;
            }
            if (_arg1.TailOrNull == null)
            {
                return new Tuple<string, FSharpList<char>>(acc, FSharpList<char>.Empty);
            }
            t2 = _arg1;
            FSharpList<char> t3 = t2.TailOrNull;
            c = t2.HeadOrDefault;
            return new Tuple<string, FSharpList<char>>(acc, FSharpList<char>.Cons(c, t3));
        Block_13:
            x.inQuotes = false;
            return new Tuple<string, FSharpList<char>>(acc, t);
        }
        internal static FSharpFunc<FSharpList<char>, FSharpOption<FSharpList<char>>> TokenStr49(string s)
        {
            return new CssSelectorTokenizer.TokenStr501(s);
        }
        internal static FSharpList<Token> tokenize60(CssSelectorTokenizer x, FSharpList<Token> acc, FSharpList<char> sourceChars)
        {
            char c;
            FSharpList<char> t2;
            for (; ; )
            {
                if (sourceChars.TailOrNull != null)
                {
                    FSharpList<char> fsharpList = sourceChars;
                    if (char.IsWhiteSpace(fsharpList.HeadOrDefault))
                    {
                        c = fsharpList.HeadOrDefault;
                        FSharpList<char> fsharpList2 = fsharpList.TailOrNull;
                        FSharpList<Token> seqtoken = SeqModule.ToList<Token>(SeqModule.Skip<Token>(1, acc));
                        switch (acc.Head.Tag)
                        {
                            default:
                                {
                                    CssSelectorTokenizer cssSelectorTokenizer = x;
                                    FSharpList<Token> fsharpList3 = FSharpList<Token>.Cons(Token.NewAllChildren(x.getOffset(fsharpList2)), acc);
                                    sourceChars = fsharpList2;
                                    acc = fsharpList3;
                                    x = cssSelectorTokenizer;
                                    continue;
                                }
                            case 5:
                                {
                                    CssSelectorTokenizer cssSelectorTokenizer2 = x;
                                    FSharpList<Token> fsharpList4 = FSharpList<Token>.Cons(Token.NewAllChildren(x.getOffset(fsharpList2)), seqtoken);
                                    sourceChars = fsharpList2;
                                    acc = fsharpList4;
                                    x = cssSelectorTokenizer2;
                                    continue;
                                }
                            case 13:
                                {
                                    CssSelectorTokenizer cssSelectorTokenizer3 = x;
                                    FSharpList<Token> fsharpList5 = FSharpList<Token>.Cons(Token.NewDirectChildren(x.getOffset(fsharpList2)), seqtoken);
                                    sourceChars = fsharpList2;
                                    acc = fsharpList5;
                                    x = cssSelectorTokenizer3;
                                    continue;
                                }
                            case 14:
                                {
                                    CssSelectorTokenizer cssSelectorTokenizer4 = x;
                                    FSharpList<Token> fsharpList6 = FSharpList<Token>.Cons(Token.NewAncestor(x.getOffset(fsharpList2)), seqtoken);
                                    sourceChars = fsharpList2;
                                    acc = fsharpList6;
                                    x = cssSelectorTokenizer4;
                                    continue;
                                }
                        }
                    }
                }
                FSharpList<char> t;
                Tuple<string, FSharpList<char>> tuple;
                if (sourceChars.TailOrNull != null)
                {
                    FSharpList<char> fsharpList2 = sourceChars;
                    switch (fsharpList2.HeadOrDefault)
                    {
                        case '!':
                            if (fsharpList2.TailOrNull.TailOrNull != null)
                            {
                                t = fsharpList2.TailOrNull;
                                switch (t.HeadOrDefault)
                                {
                                    case '=':
                                        {
                                            t2 = t.TailOrNull;
                                            tuple = CssSelectorTokenizer.readString26(x, "", t2);
                                            FSharpList<char> fsharpList7 = tuple.Item2;
                                            string s = tuple.Item1;
                                            CssSelectorTokenizer cssSelectorTokenizer5 = x;
                                            FSharpList<Token> fsharpList8 = FSharpList<Token>.Cons(Token.NewAttributeValue(x.getOffset(t2) + 1, s), FSharpList<Token>.Cons(Token.NewAttributeNotEqual(x.getOffset(t2)), acc));
                                            sourceChars = fsharpList7;
                                            acc = fsharpList8;
                                            x = cssSelectorTokenizer5;
                                            continue;
                                        }
                                    default:
                                        {
                                            FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                            if (fsharpOption == null)
                                            {
                                                goto IL_235;
                                            }
                                            t = fsharpOption.Value;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                if (fsharpOption == null)
                                {
                                    goto IL_235;
                                }
                                t = fsharpOption.Value;
                            }
                            break;
                        default:
                            switch (fsharpList2.HeadOrDefault)
                            {
                                case '#':
                                    {
                                        t = fsharpList2.TailOrNull;
                                        tuple = CssSelectorTokenizer.readString26(x, "", t);
                                        t2 = tuple.Item2;
                                        string s = tuple.Item1;
                                        CssSelectorTokenizer cssSelectorTokenizer6 = x;
                                        FSharpList<Token> fsharpList9 = FSharpList<Token>.Cons(Token.NewCssId(x.getOffset(t) + 1, s), FSharpList<Token>.Cons(Token.NewIdPrefix(x.getOffset(t)), acc));
                                        sourceChars = t2;
                                        acc = fsharpList9;
                                        x = cssSelectorTokenizer6;
                                        continue;
                                    }
                                case '$':
                                    if (fsharpList2.TailOrNull.TailOrNull != null)
                                    {
                                        t = fsharpList2.TailOrNull;
                                        switch (t.HeadOrDefault)
                                        {
                                            case '=':
                                                {
                                                    t2 = t.TailOrNull;
                                                    tuple = CssSelectorTokenizer.readString26(x, "", t2);
                                                    FSharpList<char> fsharpList7 = tuple.Item2;
                                                    string s = tuple.Item1;
                                                    CssSelectorTokenizer cssSelectorTokenizer7 = x;
                                                    FSharpList<Token> fsharpList10 = FSharpList<Token>.Cons(Token.NewAttributeValue(x.getOffset(t2) + 1, s), FSharpList<Token>.Cons(Token.NewEndWith(x.getOffset(t2)), acc));
                                                    sourceChars = fsharpList7;
                                                    acc = fsharpList10;
                                                    x = cssSelectorTokenizer7;
                                                    continue;
                                                }
                                            default:
                                                {
                                                    FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                    if (fsharpOption == null)
                                                    {
                                                        goto IL_235;
                                                    }
                                                    t = fsharpOption.Value;
                                                    break;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                        if (fsharpOption == null)
                                        {
                                            goto IL_235;
                                        }
                                        t = fsharpOption.Value;
                                    }
                                    break;
                                default:
                                    switch (fsharpList2.HeadOrDefault)
                                    {
                                        case '*':
                                            if (fsharpList2.TailOrNull.TailOrNull != null)
                                            {
                                                t = fsharpList2.TailOrNull;
                                                switch (t.HeadOrDefault)
                                                {
                                                    case '=':
                                                        {
                                                            t2 = t.TailOrNull;
                                                            tuple = CssSelectorTokenizer.readString26(x, "", t2);
                                                            FSharpList<char> fsharpList7 = tuple.Item2;
                                                            string s = tuple.Item1;
                                                            CssSelectorTokenizer cssSelectorTokenizer8 = x;
                                                            FSharpList<Token> fsharpList11 = FSharpList<Token>.Cons(Token.NewAttributeValue(x.getOffset(t2) + 1, s), FSharpList<Token>.Cons(Token.NewAttributeContains(x.getOffset(t2)), acc));
                                                            sourceChars = fsharpList7;
                                                            acc = fsharpList11;
                                                            x = cssSelectorTokenizer8;
                                                            continue;
                                                        }
                                                    default:
                                                        {
                                                            FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                            if (fsharpOption == null)
                                                            {
                                                                goto IL_235;
                                                            }
                                                            t = fsharpOption.Value;
                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                            {
                                                FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                if (fsharpOption == null)
                                                {
                                                    goto IL_235;
                                                }
                                                t = fsharpOption.Value;
                                            }
                                            break;
                                        default:
                                            switch (fsharpList2.HeadOrDefault)
                                            {
                                                case '.':
                                                    {
                                                        t = fsharpList2.TailOrNull;
                                                        tuple = CssSelectorTokenizer.readString26(x, "", t);
                                                        t2 = tuple.Item2;
                                                        string s = tuple.Item1;
                                                        CssSelectorTokenizer cssSelectorTokenizer9 = x;
                                                        FSharpList<Token> fsharpList12 = FSharpList<Token>.Cons(Token.NewCssClass(x.getOffset(t) + 1, s), FSharpList<Token>.Cons(Token.NewClassPrefix(x.getOffset(t)), acc));
                                                        sourceChars = t2;
                                                        acc = fsharpList12;
                                                        x = cssSelectorTokenizer9;
                                                        continue;
                                                    }
                                                default:
                                                    switch (fsharpList2.HeadOrDefault)
                                                    {
                                                        case '=':
                                                            {
                                                                t = fsharpList2.TailOrNull;
                                                                tuple = CssSelectorTokenizer.readString26(x, "", t);
                                                                t2 = tuple.Item2;
                                                                string s = tuple.Item1;
                                                                CssSelectorTokenizer cssSelectorTokenizer10 = x;
                                                                FSharpList<Token> fsharpList13 = FSharpList<Token>.Cons(Token.NewAttributeValue(x.getOffset(t) + 1, s), FSharpList<Token>.Cons(Token.NewAssign(x.getOffset(t)), acc));
                                                                sourceChars = t2;
                                                                acc = fsharpList13;
                                                                x = cssSelectorTokenizer10;
                                                                continue;
                                                            }
                                                        default:
                                                            switch (fsharpList2.HeadOrDefault)
                                                            {
                                                                case '[':
                                                                    {
                                                                        t = fsharpList2.TailOrNull;
                                                                        tuple = CssSelectorTokenizer.readString26(x, "", t);
                                                                        t2 = tuple.Item2;
                                                                        string s = tuple.Item1;
                                                                        CssSelectorTokenizer cssSelectorTokenizer11 = x;
                                                                        FSharpList<Token> fsharpList14 = FSharpList<Token>.Cons(Token.NewAttributeName(x.getOffset(t) + 1, s), FSharpList<Token>.Cons(Token.NewOpenAttribute(x.getOffset(t)), acc));
                                                                        sourceChars = t2;
                                                                        acc = fsharpList14;
                                                                        x = cssSelectorTokenizer11;
                                                                        continue;
                                                                    }
                                                                default:
                                                                    switch (fsharpList2.HeadOrDefault)
                                                                    {
                                                                        case ']':
                                                                            {
                                                                                t = fsharpList2.TailOrNull;
                                                                                CssSelectorTokenizer cssSelectorTokenizer12 = x;
                                                                                FSharpList<Token> fsharpList15 = FSharpList<Token>.Cons(Token.NewCloseAttribute(x.getOffset(t)), acc);
                                                                                sourceChars = t;
                                                                                acc = fsharpList15;
                                                                                x = cssSelectorTokenizer12;
                                                                                continue;
                                                                            }
                                                                        case '^':
                                                                            if (fsharpList2.TailOrNull.TailOrNull != null)
                                                                            {
                                                                                t = fsharpList2.TailOrNull;
                                                                                switch (t.HeadOrDefault)
                                                                                {
                                                                                    case '=':
                                                                                        {
                                                                                            t2 = t.TailOrNull;
                                                                                            tuple = CssSelectorTokenizer.readString26(x, "", t2);
                                                                                            FSharpList<char> fsharpList7 = tuple.Item2;
                                                                                            string s = tuple.Item1;
                                                                                            CssSelectorTokenizer cssSelectorTokenizer13 = x;
                                                                                            FSharpList<Token> fsharpList16 = FSharpList<Token>.Cons(Token.NewAttributeValue(x.getOffset(t2) + 1, s), FSharpList<Token>.Cons(Token.NewStartWith(x.getOffset(t2)), acc));
                                                                                            sourceChars = fsharpList7;
                                                                                            acc = fsharpList16;
                                                                                            x = cssSelectorTokenizer13;
                                                                                            continue;
                                                                                        }
                                                                                    default:
                                                                                        {
                                                                                            FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                                                            if (fsharpOption == null)
                                                                                            {
                                                                                                goto IL_235;
                                                                                            }
                                                                                            t = fsharpOption.Value;
                                                                                            break;
                                                                                        }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                                                if (fsharpOption == null)
                                                                                {
                                                                                    goto IL_235;
                                                                                }
                                                                                t = fsharpOption.Value;
                                                                            }
                                                                            break;
                                                                        default:
                                                                            switch (fsharpList2.HeadOrDefault)
                                                                            {
                                                                                case '|':
                                                                                    if (fsharpList2.TailOrNull.TailOrNull != null)
                                                                                    {
                                                                                        t = fsharpList2.TailOrNull;
                                                                                        switch (t.HeadOrDefault)
                                                                                        {
                                                                                            case '=':
                                                                                                {
                                                                                                    t2 = t.TailOrNull;
                                                                                                    tuple = CssSelectorTokenizer.readString26(x, "", t2);
                                                                                                    FSharpList<char> fsharpList7 = tuple.Item2;
                                                                                                    string s = tuple.Item1;
                                                                                                    CssSelectorTokenizer cssSelectorTokenizer14 = x;
                                                                                                    FSharpList<Token> fsharpList17 = FSharpList<Token>.Cons(Token.NewAttributeValue(x.getOffset(t2) + 1, s), FSharpList<Token>.Cons(Token.NewAttributeContainsPrefix(x.getOffset(t2)), acc));
                                                                                                    sourceChars = fsharpList7;
                                                                                                    acc = fsharpList17;
                                                                                                    x = cssSelectorTokenizer14;
                                                                                                    continue;
                                                                                                }
                                                                                            default:
                                                                                                {
                                                                                                    FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                                                                    if (fsharpOption == null)
                                                                                                    {
                                                                                                        goto IL_235;
                                                                                                    }
                                                                                                    t = fsharpOption.Value;
                                                                                                    break;
                                                                                                }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                                                        if (fsharpOption == null)
                                                                                        {
                                                                                            goto IL_235;
                                                                                        }
                                                                                        t = fsharpOption.Value;
                                                                                    }
                                                                                    break;
                                                                                default:
                                                                                    switch (fsharpList2.HeadOrDefault)
                                                                                    {
                                                                                        case '~':
                                                                                            if (fsharpList2.TailOrNull.TailOrNull != null)
                                                                                            {
                                                                                                t = fsharpList2.TailOrNull;
                                                                                                switch (t.HeadOrDefault)
                                                                                                {
                                                                                                    case '=':
                                                                                                        {
                                                                                                            t2 = t.TailOrNull;
                                                                                                            tuple = CssSelectorTokenizer.readString26(x, "", t2);
                                                                                                            FSharpList<char> fsharpList7 = tuple.Item2;
                                                                                                            string s = tuple.Item1;
                                                                                                            CssSelectorTokenizer cssSelectorTokenizer15 = x;
                                                                                                            FSharpList<Token> fsharpList18 = FSharpList<Token>.Cons(Token.NewAttributeValue(x.getOffset(t2) + 1, s), FSharpList<Token>.Cons(Token.NewAttributeContainsWord(x.getOffset(t2)), acc));
                                                                                                            sourceChars = fsharpList7;
                                                                                                            acc = fsharpList18;
                                                                                                            x = cssSelectorTokenizer15;
                                                                                                            continue;
                                                                                                        }
                                                                                                    default:
                                                                                                        {
                                                                                                            FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                                                                            if (fsharpOption == null)
                                                                                                            {
                                                                                                                goto IL_235;
                                                                                                            }
                                                                                                            t = fsharpOption.Value;
                                                                                                            break;
                                                                                                        }
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                                                                if (fsharpOption == null)
                                                                                                {
                                                                                                    goto IL_235;
                                                                                                }
                                                                                                t = fsharpOption.Value;
                                                                                            }
                                                                                            break;
                                                                                        default:
                                                                                            {
                                                                                                FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                                                                                                if (fsharpOption == null)
                                                                                                {
                                                                                                    goto IL_235;
                                                                                                }
                                                                                                t = fsharpOption.Value;
                                                                                                break;
                                                                                            }
                                                                                    }
                                                                                    break;
                                                                            }
                                                                            break;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    FSharpOption<FSharpList<char>> fsharpOption = CssSelectorTokenizer.TokenStr49(":checkbox").Invoke(sourceChars);
                    if (fsharpOption == null)
                    {
                        goto IL_235;
                    }
                    t = fsharpOption.Value;
                }
                tuple = CssSelectorTokenizer.readString26(x, "", t);
                t2 = tuple.Item2;
                CssSelectorTokenizer cssSelectorTokenizer16 = x;
                FSharpList<Token> fsharpList19 = FSharpList<Token>.Cons(Token.NewCheckbox(x.getOffset(t) + 1), acc);
                sourceChars = t2;
                acc = fsharpList19;
                x = cssSelectorTokenizer16;
                continue;
            IL_235:
                FSharpOption<FSharpList<char>> fsharpOption2 = CssSelectorTokenizer.TokenStr49(":selected").Invoke(sourceChars);
                if (fsharpOption2 != null)
                {
                    t = fsharpOption2.Value;
                    tuple = CssSelectorTokenizer.readString26(x, "", t);
                    t2 = tuple.Item2;
                    CssSelectorTokenizer cssSelectorTokenizer17 = x;
                    FSharpList<Token> fsharpList20 = FSharpList<Token>.Cons(Token.NewSelected(x.getOffset(t) + 1), acc);
                    sourceChars = t2;
                    acc = fsharpList20;
                    x = cssSelectorTokenizer17;
                }
                else
                {
                    FSharpOption<FSharpList<char>> fsharpOption3 = CssSelectorTokenizer.TokenStr49(":checked").Invoke(sourceChars);
                    if (fsharpOption3 != null)
                    {
                        t = fsharpOption3.Value;
                        tuple = CssSelectorTokenizer.readString26(x, "", t);
                        t2 = tuple.Item2;
                        CssSelectorTokenizer cssSelectorTokenizer18 = x;
                        FSharpList<Token> fsharpList21 = FSharpList<Token>.Cons(Token.NewChecked(x.getOffset(t) + 1), acc);
                        sourceChars = t2;
                        acc = fsharpList21;
                        x = cssSelectorTokenizer18;
                    }
                    else
                    {
                        FSharpOption<FSharpList<char>> fsharpOption4 = CssSelectorTokenizer.TokenStr49(":disabled").Invoke(sourceChars);
                        if (fsharpOption4 != null)
                        {
                            t = fsharpOption4.Value;
                            tuple = CssSelectorTokenizer.readString26(x, "", t);
                            t2 = tuple.Item2;
                            CssSelectorTokenizer cssSelectorTokenizer19 = x;
                            FSharpList<Token> fsharpList22 = FSharpList<Token>.Cons(Token.NewDisabled(x.getOffset(t) + 1), acc);
                            sourceChars = t2;
                            acc = fsharpList22;
                            x = cssSelectorTokenizer19;
                        }
                        else
                        {
                            FSharpOption<FSharpList<char>> fsharpOption5 = CssSelectorTokenizer.TokenStr49(":enabled").Invoke(sourceChars);
                            if (fsharpOption5 != null)
                            {
                                t = fsharpOption5.Value;
                                tuple = CssSelectorTokenizer.readString26(x, "", t);
                                t2 = tuple.Item2;
                                CssSelectorTokenizer cssSelectorTokenizer20 = x;
                                FSharpList<Token> fsharpList23 = FSharpList<Token>.Cons(Token.NewEnabled(x.getOffset(t) + 1), acc);
                                sourceChars = t2;
                                acc = fsharpList23;
                                x = cssSelectorTokenizer20;
                            }
                            else
                            {
                                if (sourceChars.TailOrNull == null)
                                {
                                    break;
                                }
                                t = sourceChars;
                                switch (t.HeadOrDefault)
                                {
                                    case '<':
                                        {
                                            t2 = t.TailOrNull;
                                            FSharpList<Token> seqtoken = SeqModule.ToList<Token>(SeqModule.Skip<Token>(1, acc));
                                            if (acc.Head.Tag == 5)
                                            {
                                                CssSelectorTokenizer cssSelectorTokenizer21 = x;
                                                FSharpList<Token> fsharpList24 = FSharpList<Token>.Cons(Token.NewAncestor(x.getOffset(t2)), seqtoken);
                                                sourceChars = t2;
                                                acc = fsharpList24;
                                                x = cssSelectorTokenizer21;
                                            }
                                            else
                                            {
                                                CssSelectorTokenizer cssSelectorTokenizer22 = x;
                                                FSharpList<Token> fsharpList25 = FSharpList<Token>.Cons(Token.NewAncestor(x.getOffset(t2)), acc);
                                                sourceChars = t2;
                                                acc = fsharpList25;
                                                x = cssSelectorTokenizer22;
                                            }
                                            break;
                                        }
                                    default:
                                        switch (t.HeadOrDefault)
                                        {
                                            case '>':
                                                {
                                                    t2 = t.TailOrNull;
                                                    FSharpList<Token> seqtoken = SeqModule.ToList<Token>(SeqModule.Skip<Token>(1, acc));
                                                    if (acc.Head.Tag == 5)
                                                    {
                                                        CssSelectorTokenizer cssSelectorTokenizer23 = x;
                                                        FSharpList<Token> fsharpList26 = FSharpList<Token>.Cons(Token.NewDirectChildren(x.getOffset(t2)), seqtoken);
                                                        sourceChars = t2;
                                                        acc = fsharpList26;
                                                        x = cssSelectorTokenizer23;
                                                    }
                                                    else
                                                    {
                                                        CssSelectorTokenizer cssSelectorTokenizer24 = x;
                                                        FSharpList<Token> fsharpList27 = FSharpList<Token>.Cons(Token.NewDirectChildren(x.getOffset(t2)), acc);
                                                        sourceChars = t2;
                                                        acc = fsharpList27;
                                                        x = cssSelectorTokenizer24;
                                                    }
                                                    break;
                                                }
                                            default:
                                                {
                                                    if (!char.IsLetterOrDigit(t.HeadOrDefault))
                                                    {
                                                        goto IL_440;
                                                    }
                                                    t2 = t.TailOrNull;
                                                    string s = t.HeadOrDefault.ToString();
                                                    tuple = CssSelectorTokenizer.readString26(x, s, t2);
                                                    FSharpList<char> fsharpList7 = tuple.Item2;
                                                    string s2 = tuple.Item1;
                                                    CssSelectorTokenizer cssSelectorTokenizer25 = x;
                                                    FSharpList<Token> fsharpList28 = FSharpList<Token>.Cons(Token.NewTagName(x.getOffset(t2), s2), acc);
                                                    sourceChars = fsharpList7;
                                                    acc = fsharpList28;
                                                    x = cssSelectorTokenizer25;
                                                    break;
                                                }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        IL_440:
            if (sourceChars.TailOrNull == null)
            {
                return ListModule.Reverse<Token>(acc);
            }
            t2 = sourceChars;
            c = t2.HeadOrDefault;
            if (!char.IsLetterOrDigit(c))
            {
                FSharpList<char> fsharpList7 = t2.TailOrNull;
                c = t2.HeadOrDefault;
                int offset = x.getOffset(fsharpList7);
                FSharpFunc<char, FSharpFunc<int, string>> clo = PrintfModule.PrintFormatToStringThen<FSharpFunc<char, FSharpFunc<int, string>>>(new PrintfFormat<FSharpFunc<char, FSharpFunc<int, string>>, Unit, string, string, Tuple<char, int>>("Invalid css selector syntax (char '%c' at offset %d)"));
                string s = FSharpFunc<char, int>.InvokeFast<string>(new CssSelectorTokenizer.tokenize1411(clo), c, offset);
                throw new Exception(s);
            }
            throw new Exception("Invalid css selector syntax");
        }
        internal static FSharpOption<FSharpList<a>> equal52<a>(FSharpList<a> x, FSharpList<a> s)
        {
            FSharpList<a> x2;
            while (s.TailOrNull != null)
            {
                x2 = s;
                if (x.TailOrNull != null)
                {
                    FSharpList<a> fsharpList = x;
                    a xh = fsharpList.HeadOrDefault;
                    a sh = x2.HeadOrDefault;
                    if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<a>(xh, sh))
                    {
                        FSharpList<a> tailOrNull = fsharpList.TailOrNull;
                        xh = fsharpList.HeadOrDefault;
                        FSharpList<a> tailOrNull2 = x2.TailOrNull;
                        sh = x2.HeadOrDefault;
                        FSharpList<a> fsharpList2 = tailOrNull;
                        s = tailOrNull2;
                        x = fsharpList2;
                        continue;
                    }
                }
                return null;
            }
            x2 = x;
            return FSharpOption<FSharpList<a>>.Some(x2);
        }
        [Serializable]
        internal sealed class TokenStr501 : FSharpFunc<FSharpList<char>, FSharpOption<FSharpList<char>>>
        {
            internal TokenStr501(string s)
            {
                this.s = s;
            }
            public override FSharpOption<FSharpList<char>> Invoke(FSharpList<char> x)
            {
                FSharpList<char> chars = SeqModule.ToList<char>(this.s);
                return CssSelectorTokenizer.equal52<char>(x, chars);
            }
            public string s;
        }
        internal sealed class tokenize1412 : FSharpFunc<int, string>
        {
            internal tokenize1412(FSharpFunc<int, string> clo2)
            {
                this.clo2 = clo2;
            }
            public override string Invoke(int arg20)
            {
                return this.clo2.Invoke(arg20);
            }
            public FSharpFunc<int, string> clo2;
        }
        internal sealed class tokenize1411 : FSharpFunc<char, FSharpFunc<int, string>>
        {
            internal tokenize1411(FSharpFunc<char, FSharpFunc<int, string>> clo1)
            {
                this.clo1 = clo1;
            }
            public override FSharpFunc<int, string> Invoke(char arg10)
            {
                FSharpFunc<int, string> clo = this.clo1.Invoke(arg10);
                return new CssSelectorTokenizer.tokenize1412(clo);
            }
            public FSharpFunc<char, FSharpFunc<int, string>> clo1;
        }
        public CssSelectorTokenizer()
        {
            this.charCount = 0;
            this.source = FSharpList<char>.Empty;
            this.cssSelector = "";
            this.inQuotes = false;
        }
        public Token[] Tokenize(string pCssSelector)
        {
            this.cssSelector = pCssSelector;
            this.source = ArrayModule.ToList<char>(this.cssSelector.ToCharArray());
            this.charCount = this.source.Length;
            return ListModule.ToArray<Token>(this.tokenize());
        }
        internal FSharpList<Token> tokenize()
        {
            return CssSelectorTokenizer.tokenize60(this, FSharpList<Token>.Empty, this.source);
        }
        internal int getOffset(FSharpList<char> t)
        {
            return this.charCount - 1 - t.Length;
        }
        internal int charCount;
        internal FSharpList<char> source;
        internal string cssSelector;
        internal bool inQuotes;
    }
}
