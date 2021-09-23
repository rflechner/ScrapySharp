using Microsoft.FSharp.Core;
using System;
using System.Collections;
using System.Diagnostics;
namespace ScrapySharp.Core.CSharp
{
    public abstract class Token : IEquatable<Token>, IStructuralEquatable, IComparable<Token>, IComparable, IStructuralComparable
    {
        internal Token(int _tag)
        {
            this.Tag = _tag;
        }
        public static Token NewClassPrefix(int item)
        {
            return new Token.ClassPrefix(item);
        }
        public bool IsClassPrefix
        {
            get
            {
                return this.Tag == 0;
            }
        }
        public static Token NewIdPrefix(int item)
        {
            return new Token.IdPrefix(item);
        }
        public bool IsIdPrefix
        {
            get
            {
                return this.Tag == 1;
            }
        }
        public static Token NewTagName(int item1, string item2)
        {
            return new Token.TagName(item1, item2);
        }
        public bool IsTagName
        {
            get
            {
                return this.Tag == 2;
            }
        }
        public static Token NewCssClass(int item1, string item2)
        {
            return new Token.CssClass(item1, item2);
        }
        public bool IsCssClass
        {
            get
            {
                return this.Tag == 3;
            }
        }
        public static Token NewCssId(int item1, string item2)
        {
            return new Token.CssId(item1, item2);
        }
        public bool IsCssId
        {
            get
            {
                return this.Tag == 4;
            }
        }
        public static Token NewAllChildren(int item)
        {
            return new Token.AllChildren(item);
        }
        public bool IsAllChildren
        {
            get
            {
                return this.Tag == 5;
            }
        }
        public static Token NewOpenAttribute(int item)
        {
            return new Token.OpenAttribute(item);
        }
        public bool IsOpenAttribute
        {
            get
            {
                return this.Tag == 6;
            }
        }
        public static Token NewCloseAttribute(int item)
        {
            return new Token.CloseAttribute(item);
        }
        public bool IsCloseAttribute
        {
            get
            {
                return this.Tag == 7;
            }
        }
        public static Token NewAttributeName(int item1, string item2)
        {
            return new Token.AttributeName(item1, item2);
        }
        public bool IsAttributeName
        {
            get
            {
                return this.Tag == 8;
            }
        }
        public static Token NewAttributeValue(int item1, string item2)
        {
            return new Token.AttributeValue(item1, item2);
        }
        public bool IsAttributeValue
        {
            get
            {
                return this.Tag == 9;
            }
        }
        public static Token NewAssign(int item)
        {
            return new Token.Assign(item);
        }
        public bool IsAssign
        {
            get
            {
                return this.Tag == 10;
            }
        }
        public static Token NewEndWith(int item)
        {
            return new Token.EndWith(item);
        }
        public bool IsEndWith
        {
            get
            {
                return this.Tag == 11;
            }
        }
        public static Token NewStartWith(int item)
        {
            return new Token.StartWith(item);
        }
        public bool IsStartWith
        {
            get
            {
                return this.Tag == 12;
            }
        }
        public static Token NewDirectChildren(int item)
        {
            return new Token.DirectChildren(item);
        }
        public bool IsDirectChildren
        {
            get
            {
                return this.Tag == 13;
            }
        }
        public static Token NewAncestor(int item)
        {
            return new Token.Ancestor(item);
        }
        public bool IsAncestor
        {
            get
            {
                return this.Tag == 14;
            }
        }
        public static Token NewAttributeContainsPrefix(int item)
        {
            return new Token.AttributeContainsPrefix(item);
        }
        public bool IsAttributeContainsPrefix
        {
            get
            {
                return this.Tag == 15;
            }
        }
        public static Token NewAttributeContains(int item)
        {
            return new Token.AttributeContains(item);
        }
        public bool IsAttributeContains
        {
            get
            {
                return this.Tag == 16;
            }
        }
        public static Token NewAttributeContainsWord(int item)
        {
            return new Token.AttributeContainsWord(item);
        }
        public bool IsAttributeContainsWord
        {
            get
            {
                return this.Tag == 17;
            }
        }
        public static Token NewAttributeNotEqual(int item)
        {
            return new Token.AttributeNotEqual(item);
        }
        public bool IsAttributeNotEqual
        {
            get
            {
                return this.Tag == 18;
            }
        }
        public static Token NewCheckbox(int item)
        {
            return new Token.Checkbox(item);
        }
        public bool IsCheckbox
        {
            get
            {
                return this.Tag == 19;
            }
        }
        public static Token NewChecked(int item)
        {
            return new Token.Checked(item);
        }
        public bool IsChecked
        {
            get
            {
                return this.Tag == 20;
            }
        }
        public static Token NewDisabled(int item)
        {
            return new Token.Disabled(item);
        }
        public bool IsDisabled
        {
            get
            {
                return this.Tag == 21;
            }
        }
        public static Token NewEnabled(int item)
        {
            return new Token.Enabled(item);
        }
        public bool IsEnabled
        {
            get
            {
                return this.Tag == 22;
            }
        }
        public static Token NewSelected(int item)
        {
            return new Token.Selected(item);
        }
        public bool IsSelected
        {
            get
            {
                return this.Tag == 23;
            }
        }
        public int Tag { get; }
        internal object __DebugDisplay()
        {
            return ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Token, string>>(new PrintfFormat<FSharpFunc<Token, string>, Unit, string, string, string>("%+0.8A")).Invoke(this);
        }
        public override string ToString()
        {
            return ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Token, string>>(new PrintfFormat<FSharpFunc<Token, string>, Unit, string, string, Token>("%+A")).Invoke(this);
        }
        public int CompareTo(Token obj)
        {
            if (this != null)
            {
                if (obj == null)
                {
                    return 1;
                }
                int tag = this._tag;
                int tag2 = obj._tag;
                if (tag == tag2)
                {
                    return Models.CompareTocont9(this, obj, null);
                }
                return tag - tag2;
            }
            else
            {
                if (obj != null)
                {
                    return -1;
                }
                return 0;
            }
        }
        public int CompareTo(object obj)
        {
            return this.CompareTo((Token)obj);
        }
        public int CompareTo(object obj, IComparer comp)
        {
            Token token = (Token)obj;
            if (this != null)
            {
                if ((Token)obj == null)
                {
                    return 1;
                }
                int tag = this._tag;
                int tag2 = token._tag;
                if (tag == tag2)
                {
                    return Models.CompareTocont91(this, token, null);
                }
                return tag - tag2;
            }
            else
            {
                if ((Token)obj != null)
                {
                    return -1;
                }
                return 0;
            }
        }
        public int GetHashCode(IEqualityComparer comp)
        {
            if (this != null)
            {
                return Models.GetHashCodecont9(this, null);
            }
            return 0;
        }
        public sealed override int GetHashCode()
        {
            return this.GetHashCode(LanguagePrimitives.GenericEqualityComparer);
        }
        public bool Equals(object obj, IEqualityComparer comp)
        {
            if (this == null)
            {
                return obj == null;
            }
            Token token = obj as Token;
            if (token != null)
            {
                int tag = this._tag;
                int tag2 = token._tag;
                return tag == tag2 && Models.Equalscont9(this, token, null);
            }
            return false;
        }
        public bool Equals(Token obj)
        {
            if (this == null)
            {
                return obj == null;
            }
            if (obj != null)
            {
                int tag = this._tag;
                int tag2 = obj._tag;
                return tag == tag2 && Models.Equalscont91(this, obj, null);
            }
            return false;
        }
        public sealed override bool Equals(object obj)
        {
            Token token = obj as Token;
            return token != null && this.Equals(token);
        }
        internal readonly int _tag;
        public static class Tags
        {
            public const int ClassPrefix = 0;
            public const int IdPrefix = 1;
            public const int TagName = 2;
            public const int CssClass = 3;
            public const int CssId = 4;
            public const int AllChildren = 5;
            public const int OpenAttribute = 6;
            public const int CloseAttribute = 7;
            public const int AttributeName = 8;
            public const int AttributeValue = 9;
            public const int Assign = 10;
            public const int EndWith = 11;
            public const int StartWith = 12;
            public const int DirectChildren = 13;
            public const int Ancestor = 14;
            public const int AttributeContainsPrefix = 15;
            public const int AttributeContains = 16;
            public const int AttributeContainsWord = 17;
            public const int AttributeNotEqual = 18;
            public const int Checkbox = 19;
            public const int Checked = 20;
            public const int Disabled = 21;
            public const int Enabled = 22;
            public const int Selected = 23;
        }
        public class ClassPrefix : Token
        {
            internal ClassPrefix(int item) : base(0)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class IdPrefix : Token
        {
            internal IdPrefix(int item) : base(1)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class TagName : Token
        {
            internal TagName(int item1, string item2) : base(2)
            {
                this.Item1 = item1;
                this.Item2 = item2;
            }
            public int Item1 { get; }
            public string Item2 { get; }
            internal readonly int item1;
            internal readonly string item2;
        }
        public class CssClass : Token
        {
            internal CssClass(int item1, string item2) : base(3)
            {
                this.Item1 = item1;
                this.Item2 = item2;
            }
            public int Item1 { get; }
            public string Item2 { get; }
            internal readonly int item1;
            internal readonly string item2;
        }
        public class CssId : Token
        {
            internal CssId(int item1, string item2) : base(4)
            {
                this.Item1 = item1;
                this.Item2 = item2;
            }
            public int Item1 { get; }
            public string Item2 { get; }
            internal readonly int item1;
            internal readonly string item2;
        }
        public class AllChildren : Token
        {
            internal AllChildren(int item) : base(5)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class OpenAttribute : Token
        {
            internal OpenAttribute(int item) : base(6)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class CloseAttribute : Token
        {
            internal CloseAttribute(int item) : base(7)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class AttributeName : Token
        {
            internal AttributeName(int item1, string item2) : base(8)
            {
                this.Item1 = item1;
                this.Item2 = item2;
            }
            public int Item1 { get; }
            public string Item2 { get; }
            internal readonly int item1;
            internal readonly string item2;
        }
        public class AttributeValue : Token
        {
            internal AttributeValue(int item1, string item2) : base(9)
            {
                this.Item1 = item1;
                this.Item2 = item2;
            }
            public int Item1 { get; }
            public string Item2 { get; }
            internal readonly int item1;
            internal readonly string item2;
        }
        public class Assign : Token
        {
            internal Assign(int item) : base(10)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class EndWith : Token
        {
            internal EndWith(int item) : base(11)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class StartWith : Token
        {
            internal StartWith(int item) : base(12)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class DirectChildren : Token
        {
            internal DirectChildren(int item) : base(13)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class Ancestor : Token
        {
            internal Ancestor(int item) : base(14)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class AttributeContainsPrefix : Token
        {
            internal AttributeContainsPrefix(int item) : base(15)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class AttributeContains : Token
        {
            internal AttributeContains(int item) : base(16)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class AttributeContainsWord : Token
        {
            internal AttributeContainsWord(int item) : base(17)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class AttributeNotEqual : Token
        {
            internal AttributeNotEqual(int item) : base(18)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class Checkbox : Token
        {
            internal Checkbox(int item) : base(19)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class Checked : Token
        {
            internal Checked(int item) : base(20)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class Disabled : Token
        {
            internal Disabled(int item) : base(21)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class Enabled : Token
        {
            internal Enabled(int item) : base(22)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        public class Selected : Token
        {
            internal Selected(int item) : base(23)
            {
                this.Item = item;
            }
            public int Item { get; }
            internal readonly int item;
        }
        internal class ClassPrefixDebugTypeProxy
        {
            public ClassPrefixDebugTypeProxy(Token.ClassPrefix obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.ClassPrefix _obj;
        }
        internal class IdPrefixDebugTypeProxy
        {
            public IdPrefixDebugTypeProxy(Token.IdPrefix obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.IdPrefix _obj;
        }
        internal class TagNameDebugTypeProxy
        {
            public TagNameDebugTypeProxy(Token.TagName obj)
            {
                this._obj = obj;
            }
            public int Item1
            {
                get
                {
                    return this._obj.item1;
                }
            }
            public string Item2
            {
                get
                {
                    return this._obj.item2;
                }
            }
            internal Token.TagName _obj;
        }
        internal class CssClassDebugTypeProxy
        {
            public CssClassDebugTypeProxy(Token.CssClass obj)
            {
                this._obj = obj;
            }
            public int Item1
            {
                get
                {
                    return this._obj.item1;
                }
            }
            public string Item2
            {
                get
                {
                    return this._obj.item2;
                }
            }
            internal Token.CssClass _obj;
        }
        internal class CssIdDebugTypeProxy
        {
            public CssIdDebugTypeProxy(Token.CssId obj)
            {
                this._obj = obj;
            }
            public int Item1
            {
                get
                {
                    return this._obj.item1;
                }
            }
            public string Item2
            {
                get
                {
                    return this._obj.item2;
                }
            }
            internal Token.CssId _obj;
        }
        internal class AllChildrenDebugTypeProxy
        {
            public AllChildrenDebugTypeProxy(Token.AllChildren obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.AllChildren _obj;
        }
        internal class OpenAttributeDebugTypeProxy
        {
            public OpenAttributeDebugTypeProxy(Token.OpenAttribute obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.OpenAttribute _obj;
        }
        internal class CloseAttributeDebugTypeProxy
        {
            public CloseAttributeDebugTypeProxy(Token.CloseAttribute obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.CloseAttribute _obj;
        }
        internal class AttributeNameDebugTypeProxy
        {
            public AttributeNameDebugTypeProxy(Token.AttributeName obj)
            {
                this._obj = obj;
            }
            public int Item1
            {
                get
                {
                    return this._obj.item1;
                }
            }
            public string Item2
            {
                get
                {
                    return this._obj.item2;
                }
            }
            internal Token.AttributeName _obj;
        }
        internal class AttributeValueDebugTypeProxy
        {
            public AttributeValueDebugTypeProxy(Token.AttributeValue obj)
            {
                this._obj = obj;
            }
            public int Item1
            {
                get
                {
                    return this._obj.item1;
                }
            }
            public string Item2
            {
                get
                {
                    return this._obj.item2;
                }
            }
            internal Token.AttributeValue _obj;
        }
        internal class AssignDebugTypeProxy
        {
            public AssignDebugTypeProxy(Token.Assign obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.Assign _obj;
        }
        internal class EndWithDebugTypeProxy
        {
            public EndWithDebugTypeProxy(Token.EndWith obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.EndWith _obj;
        }
        internal class StartWithDebugTypeProxy
        {
            public StartWithDebugTypeProxy(Token.StartWith obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.StartWith _obj;
        }
        internal class DirectChildrenDebugTypeProxy
        {
            public DirectChildrenDebugTypeProxy(Token.DirectChildren obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.DirectChildren _obj;
        }
        internal class AncestorDebugTypeProxy
        {
            public AncestorDebugTypeProxy(Token.Ancestor obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.Ancestor _obj;
        }
        internal class AttributeContainsPrefixDebugTypeProxy
        {
            public AttributeContainsPrefixDebugTypeProxy(Token.AttributeContainsPrefix obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.AttributeContainsPrefix _obj;
        }
        internal class AttributeContainsDebugTypeProxy
        {
            public AttributeContainsDebugTypeProxy(Token.AttributeContains obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.AttributeContains _obj;
        }
        internal class AttributeContainsWordDebugTypeProxy
        {
            public AttributeContainsWordDebugTypeProxy(Token.AttributeContainsWord obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.AttributeContainsWord _obj;
        }
        internal class AttributeNotEqualDebugTypeProxy
        {
            public AttributeNotEqualDebugTypeProxy(Token.AttributeNotEqual obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.AttributeNotEqual _obj;
        }
        internal class CheckboxDebugTypeProxy
        {
            public CheckboxDebugTypeProxy(Token.Checkbox obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.Checkbox _obj;
        }
        internal class CheckedDebugTypeProxy
        {
            public CheckedDebugTypeProxy(Token.Checked obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.Checked _obj;
        }
        internal class DisabledDebugTypeProxy
        {
            public DisabledDebugTypeProxy(Token.Disabled obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.Disabled _obj;
        }
        internal class EnabledDebugTypeProxy
        {
            public EnabledDebugTypeProxy(Token.Enabled obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.Enabled _obj;
        }
        internal class SelectedDebugTypeProxy
        {
            public SelectedDebugTypeProxy(Token.Selected obj)
            {
                this._obj = obj;
            }
            public int Item
            {
                get
                {
                    return this._obj.item;
                }
            }
            internal Token.Selected _obj;
        }
    }
}