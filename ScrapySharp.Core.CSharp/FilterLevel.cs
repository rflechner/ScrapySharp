using Microsoft.FSharp.Core;
using System;
using System.Collections;
namespace ScrapySharp.Core.CSharp
{
    public sealed class FilterLevel : IEquatable<FilterLevel>, IStructuralEquatable, IComparable<FilterLevel>, IComparable, IStructuralComparable
    {
        public int Tag;
        internal readonly int _tag;
        internal static readonly FilterLevel _unique_Root;
        internal static readonly FilterLevel _unique_Children;
        internal static readonly FilterLevel _unique_Descendants;
        internal static readonly FilterLevel _unique_Parents;
        internal static readonly FilterLevel _unique_Ancestors;
        internal FilterLevel(int _tag)
        {
            Tag = _tag;
        }
        public static FilterLevel Root
        {
            get
            {
                return _unique_Root;
            }
            set
            {
                Root = new FilterLevel(0);
            }
        }
        public bool IsRoot
        {
            get
            {
                return Tag == 0;
            }
        }
        public static FilterLevel Children
        {
            get
            {
                return _unique_Children;
            }
            set
            {
                Children = new FilterLevel(1);
            }
        }
        public bool IsChildren
        {
            get
            {
                return Tag == 1;
            }
        }
        public static FilterLevel Descendants
        {
            get
            {
                return _unique_Descendants;
            }
            set
            {
                Descendants = new FilterLevel(2);
            }
        }
        public bool IsDescendants
        {
            get
            {
                return Tag == 2;
            }
        }
        public static FilterLevel Parents
        {
            get
            {
                return _unique_Parents;
            }
            set
            {
                Parents = new FilterLevel(3);
            }
        }
        public bool IsParents
        {
            get
            {
                return Tag == 3;
            }
        }
        public static FilterLevel Ancestors
        {
            get
            {
                return _unique_Ancestors;
            }
            set
            {
                Ancestors = new FilterLevel(4);
            }
        }
        public bool IsAncestors
        {
            get
            {
                return Tag == 4;
            }
        }

        internal object __DebugDisplay()
        {
            return ExtraTopLevelOperators.PrintFormatToString(new PrintfFormat<FSharpFunc<FilterLevel, string>, Unit, string, string, string>("%+0.8A")).Invoke(this);
        }
        public override string ToString()
        {
            return ExtraTopLevelOperators.PrintFormatToString(new PrintfFormat<FSharpFunc<FilterLevel, string>, Unit, string, string, FilterLevel>("%+A")).Invoke(this);
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