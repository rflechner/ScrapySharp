using Microsoft.FSharp.Core;
using System;
using System.Collections;
namespace ScrapySharp.Core.CSharp
{
    public sealed class FilterLevel : IEquatable<FilterLevel>, IStructuralEquatable, IComparable<FilterLevel>, IComparable, IStructuralComparable
    {
        internal FilterLevel level;
        public int Tag;
        public int _tag;
        public static FilterLevel _unique_Root;
        public static FilterLevel _unique_Children;
        public static FilterLevel _unique_Descendants;
        public static FilterLevel _unique_Parents;
        public static FilterLevel _unique_Ancestors;
        public static class Tags
        {
            public const int Root = 0;
            public const int Children = 1;
            public const int Descendants = 2;
            public const int Parents = 3;
            public const int Ancestors = 4;
        }
        internal FilterLevel(int _tag)
        {
            this.Tag = _tag;
        }
        public static FilterLevel Root
        {
            get
            {
                return FilterLevel._unique_Root;
            }
        }
        public bool IsRoot
        {
            get
            {
                return this.Tag == Tags.Root;
            }
        }
        public static FilterLevel Children
        {
            get
            {
                return FilterLevel._unique_Children;
            }
        }
        public bool IsChildren
        {
            get
            {
                return this.Tag == Tags.Children;
            }
        }
        public static FilterLevel Descendants
        {
            get
            {
                return FilterLevel._unique_Descendants;
            }
        }
        public bool IsDescendants
        {
            get
            {
                return this.Tag == Tags.Descendants;
            }
        }
        public static FilterLevel Parents
        {
            get
            {
                return FilterLevel._unique_Parents;
            }
        }
        public bool IsParents
        {
            get
            {
                return this.Tag == Tags.Parents;
            }
        }
        public static FilterLevel Ancestors
        {
            get
            {
                return FilterLevel._unique_Ancestors;
            }
        }
        public bool IsAncestors
        {
            get
            {
                return this.Tag == Tags.Ancestors;
            }
        }

        internal object __DebugDisplay()
        {
            return ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<FilterLevel, string>>(new PrintfFormat<FSharpFunc<FilterLevel, string>, Unit, string, string, string>("%+0.8A")).Invoke(this);
        }
        public override string ToString()
        {
            return ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<FilterLevel, string>>(new PrintfFormat<FSharpFunc<FilterLevel, string>, Unit, string, string, FilterLevel>("%+A")).Invoke(this);
        }
        public int CompareTo(FilterLevel obj)
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
                    return 0;
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
            return this.CompareTo((FilterLevel)obj);
        }
        public int CompareTo(object obj, IComparer comp)
        {
            FilterLevel filterLevel = (FilterLevel)obj;
            if (this != null)
            {
                if ((FilterLevel)obj == null)
                {
                    return 1;
                }
                int tag = this._tag;
                int tag2 = filterLevel._tag;
                if (tag == tag2)
                {
                    return 0;
                }
                return tag - tag2;
            }
            else
            {
                if ((FilterLevel)obj != null)
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
                return this._tag;
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
            FilterLevel filterLevel = obj as FilterLevel;
            if (filterLevel != null)
            {
                int tag = this._tag;
                int tag2 = filterLevel._tag;
                return tag == tag2;
            }
            return false;
        }
        public bool Equals(FilterLevel obj)
        {
            if (this == null)
            {
                return obj == null;
            }
            if (obj != null)
            {
                int tag = this._tag;
                int tag2 = obj._tag;
                return tag == tag2;
            }
            return false;
        }
        public sealed override bool Equals(object obj)
        {
            FilterLevel filterLevel = obj as FilterLevel;
            return filterLevel != null && this.Equals(filterLevel);
        }
    }
}