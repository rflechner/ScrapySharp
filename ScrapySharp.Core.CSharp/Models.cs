using Microsoft.FSharp.Core;
using System.Collections;
namespace ScrapySharp.Core.CSharp
{
    internal static class Models
    {
        internal static int CompareTocont9(Token @this, Token obj, Unit unitVar)
        {
            switch (@this.Tag)
            {
                default:
                    {
                        Token.ClassPrefix classPrefix = (Token.ClassPrefix)@this;
                        Token.ClassPrefix classPrefix2 = (Token.ClassPrefix)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = classPrefix.item;
                        int num2 = classPrefix2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 1:
                    {
                        Token.IdPrefix idPrefix = (Token.IdPrefix)@this;
                        Token.IdPrefix idPrefix2 = (Token.IdPrefix)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = idPrefix.item;
                        int num2 = idPrefix2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 2:
                    {
                        Token.TagName tagName = (Token.TagName)@this;
                        Token.TagName tagName2 = (Token.TagName)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num2 = tagName.item1;
                        int item = tagName2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        genericComparer = LanguagePrimitives.GenericComparer;
                        return string.CompareOrdinal(tagName.item2, tagName2.item2);
                    }
                case 3:
                    {
                        Token.CssClass cssClass = (Token.CssClass)@this;
                        Token.CssClass cssClass2 = (Token.CssClass)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num2 = cssClass.item1;
                        int item = cssClass2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        genericComparer = LanguagePrimitives.GenericComparer;
                        return string.CompareOrdinal(cssClass.item2, cssClass2.item2);
                    }
                case 4:
                    {
                        Token.CssId cssId = (Token.CssId)@this;
                        Token.CssId cssId2 = (Token.CssId)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num2 = cssId.item1;
                        int item = cssId2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        genericComparer = LanguagePrimitives.GenericComparer;
                        return string.CompareOrdinal(cssId.item2, cssId2.item2);
                    }
                case 5:
                    {
                        Token.AllChildren allChildren = (Token.AllChildren)@this;
                        Token.AllChildren allChildren2 = (Token.AllChildren)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = allChildren.item;
                        int num2 = allChildren2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 6:
                    {
                        Token.OpenAttribute openAttribute = (Token.OpenAttribute)@this;
                        Token.OpenAttribute openAttribute2 = (Token.OpenAttribute)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = openAttribute.item;
                        int num2 = openAttribute2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 7:
                    {
                        Token.CloseAttribute closeAttribute = (Token.CloseAttribute)@this;
                        Token.CloseAttribute closeAttribute2 = (Token.CloseAttribute)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = closeAttribute.item;
                        int num2 = closeAttribute2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 8:
                    {
                        Token.AttributeName attributeName = (Token.AttributeName)@this;
                        Token.AttributeName attributeName2 = (Token.AttributeName)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num2 = attributeName.item1;
                        int item = attributeName2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        genericComparer = LanguagePrimitives.GenericComparer;
                        return string.CompareOrdinal(attributeName.item2, attributeName2.item2);
                    }
                case 9:
                    {
                        Token.AttributeValue attributeValue = (Token.AttributeValue)@this;
                        Token.AttributeValue attributeValue2 = (Token.AttributeValue)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num2 = attributeValue.item1;
                        int item = attributeValue2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        genericComparer = LanguagePrimitives.GenericComparer;
                        return string.CompareOrdinal(attributeValue.item2, attributeValue2.item2);
                    }
                case 10:
                    {
                        Token.Assign assign = (Token.Assign)@this;
                        Token.Assign assign2 = (Token.Assign)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = assign.item;
                        int num2 = assign2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 11:
                    {
                        Token.EndWith endWith = (Token.EndWith)@this;
                        Token.EndWith endWith2 = (Token.EndWith)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = endWith.item;
                        int num2 = endWith2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 12:
                    {
                        Token.StartWith startWith = (Token.StartWith)@this;
                        Token.StartWith startWith2 = (Token.StartWith)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = startWith.item;
                        int num2 = startWith2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 13:
                    {
                        Token.DirectChildren directChildren = (Token.DirectChildren)@this;
                        Token.DirectChildren directChildren2 = (Token.DirectChildren)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = directChildren.item;
                        int num2 = directChildren2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 14:
                    {
                        Token.Ancestor ancestor = (Token.Ancestor)@this;
                        Token.Ancestor ancestor2 = (Token.Ancestor)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = ancestor.item;
                        int num2 = ancestor2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 15:
                    {
                        Token.AttributeContainsPrefix attributeContainsPrefix = (Token.AttributeContainsPrefix)@this;
                        Token.AttributeContainsPrefix attributeContainsPrefix2 = (Token.AttributeContainsPrefix)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = attributeContainsPrefix.item;
                        int num2 = attributeContainsPrefix2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 16:
                    {
                        Token.AttributeContains attributeContains = (Token.AttributeContains)@this;
                        Token.AttributeContains attributeContains2 = (Token.AttributeContains)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = attributeContains.item;
                        int num2 = attributeContains2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 17:
                    {
                        Token.AttributeContainsWord attributeContainsWord = (Token.AttributeContainsWord)@this;
                        Token.AttributeContainsWord attributeContainsWord2 = (Token.AttributeContainsWord)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = attributeContainsWord.item;
                        int num2 = attributeContainsWord2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 18:
                    {
                        Token.AttributeNotEqual attributeNotEqual = (Token.AttributeNotEqual)@this;
                        Token.AttributeNotEqual attributeNotEqual2 = (Token.AttributeNotEqual)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = attributeNotEqual.item;
                        int num2 = attributeNotEqual2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 19:
                    {
                        Token.Checkbox checkbox = (Token.Checkbox)@this;
                        Token.Checkbox checkbox2 = (Token.Checkbox)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = checkbox.item;
                        int num2 = checkbox2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 20:
                    {
                        Token.Checked @checked = (Token.Checked)@this;
                        Token.Checked checked2 = (Token.Checked)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = @checked.item;
                        int num2 = checked2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 21:
                    {
                        Token.Disabled disabled = (Token.Disabled)@this;
                        Token.Disabled disabled2 = (Token.Disabled)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = disabled.item;
                        int num2 = disabled2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 22:
                    {
                        Token.Enabled enabled = (Token.Enabled)@this;
                        Token.Enabled enabled2 = (Token.Enabled)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = enabled.item;
                        int num2 = enabled2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 23:
                    {
                        Token.Selected selected = (Token.Selected)@this;
                        Token.Selected selected2 = (Token.Selected)obj;
                        IComparer genericComparer = LanguagePrimitives.GenericComparer;
                        int num = selected.item;
                        int num2 = selected2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
            }
        }
        internal static int CompareTocont91(Token @this, Token objTemp, Unit unitVar)
        {
            switch (@this.Tag)
            {
                default:
                    {
                        Token.ClassPrefix classPrefix = (Token.ClassPrefix)@this;
                        Token.ClassPrefix classPrefix2 = (Token.ClassPrefix)objTemp;
                        int num = classPrefix.item;
                        int num2 = classPrefix2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 1:
                    {
                        Token.IdPrefix idPrefix = (Token.IdPrefix)@this;
                        Token.IdPrefix idPrefix2 = (Token.IdPrefix)objTemp;
                        int num = idPrefix.item;
                        int num2 = idPrefix2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 2:
                    {
                        Token.TagName tagName = (Token.TagName)@this;
                        Token.TagName tagName2 = (Token.TagName)objTemp;
                        int num2 = tagName.item1;
                        int item = tagName2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        return string.CompareOrdinal(tagName.item2, tagName2.item2);
                    }
                case 3:
                    {
                        Token.CssClass cssClass = (Token.CssClass)@this;
                        Token.CssClass cssClass2 = (Token.CssClass)objTemp;
                        int num2 = cssClass.item1;
                        int item = cssClass2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        return string.CompareOrdinal(cssClass.item2, cssClass2.item2);
                    }
                case 4:
                    {
                        Token.CssId cssId = (Token.CssId)@this;
                        Token.CssId cssId2 = (Token.CssId)objTemp;
                        int num2 = cssId.item1;
                        int item = cssId2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        return string.CompareOrdinal(cssId.item2, cssId2.item2);
                    }
                case 5:
                    {
                        Token.AllChildren allChildren = (Token.AllChildren)@this;
                        Token.AllChildren allChildren2 = (Token.AllChildren)objTemp;
                        int num = allChildren.item;
                        int num2 = allChildren2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 6:
                    {
                        Token.OpenAttribute openAttribute = (Token.OpenAttribute)@this;
                        Token.OpenAttribute openAttribute2 = (Token.OpenAttribute)objTemp;
                        int num = openAttribute.item;
                        int num2 = openAttribute2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 7:
                    {
                        Token.CloseAttribute closeAttribute = (Token.CloseAttribute)@this;
                        Token.CloseAttribute closeAttribute2 = (Token.CloseAttribute)objTemp;
                        int num = closeAttribute.item;
                        int num2 = closeAttribute2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 8:
                    {
                        Token.AttributeName attributeName = (Token.AttributeName)@this;
                        Token.AttributeName attributeName2 = (Token.AttributeName)objTemp;
                        int num2 = attributeName.item1;
                        int item = attributeName2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        return string.CompareOrdinal(attributeName.item2, attributeName2.item2);
                    }
                case 9:
                    {
                        Token.AttributeValue attributeValue = (Token.AttributeValue)@this;
                        Token.AttributeValue attributeValue2 = (Token.AttributeValue)objTemp;
                        int num2 = attributeValue.item1;
                        int item = attributeValue2.item1;
                        int num = (num2 >= item) ? ((num2 > item) ? 1 : 0) : -1;
                        if (num < 0)
                        {
                            return num;
                        }
                        if (num > 0)
                        {
                            return num;
                        }
                        return string.CompareOrdinal(attributeValue.item2, attributeValue2.item2);
                    }
                case 10:
                    {
                        Token.Assign assign = (Token.Assign)@this;
                        Token.Assign assign2 = (Token.Assign)objTemp;
                        int num = assign.item;
                        int num2 = assign2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 11:
                    {
                        Token.EndWith endWith = (Token.EndWith)@this;
                        Token.EndWith endWith2 = (Token.EndWith)objTemp;
                        int num = endWith.item;
                        int num2 = endWith2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 12:
                    {
                        Token.StartWith startWith = (Token.StartWith)@this;
                        Token.StartWith startWith2 = (Token.StartWith)objTemp;
                        int num = startWith.item;
                        int num2 = startWith2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 13:
                    {
                        Token.DirectChildren directChildren = (Token.DirectChildren)@this;
                        Token.DirectChildren directChildren2 = (Token.DirectChildren)objTemp;
                        int num = directChildren.item;
                        int num2 = directChildren2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 14:
                    {
                        Token.Ancestor ancestor = (Token.Ancestor)@this;
                        Token.Ancestor ancestor2 = (Token.Ancestor)objTemp;
                        int num = ancestor.item;
                        int num2 = ancestor2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 15:
                    {
                        Token.AttributeContainsPrefix attributeContainsPrefix = (Token.AttributeContainsPrefix)@this;
                        Token.AttributeContainsPrefix attributeContainsPrefix2 = (Token.AttributeContainsPrefix)objTemp;
                        int num = attributeContainsPrefix.item;
                        int num2 = attributeContainsPrefix2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 16:
                    {
                        Token.AttributeContains attributeContains = (Token.AttributeContains)@this;
                        Token.AttributeContains attributeContains2 = (Token.AttributeContains)objTemp;
                        int num = attributeContains.item;
                        int num2 = attributeContains2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 17:
                    {
                        Token.AttributeContainsWord attributeContainsWord = (Token.AttributeContainsWord)@this;
                        Token.AttributeContainsWord attributeContainsWord2 = (Token.AttributeContainsWord)objTemp;
                        int num = attributeContainsWord.item;
                        int num2 = attributeContainsWord2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 18:
                    {
                        Token.AttributeNotEqual attributeNotEqual = (Token.AttributeNotEqual)@this;
                        Token.AttributeNotEqual attributeNotEqual2 = (Token.AttributeNotEqual)objTemp;
                        int num = attributeNotEqual.item;
                        int num2 = attributeNotEqual2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 19:
                    {
                        Token.Checkbox checkbox = (Token.Checkbox)@this;
                        Token.Checkbox checkbox2 = (Token.Checkbox)objTemp;
                        int num = checkbox.item;
                        int num2 = checkbox2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 20:
                    {
                        Token.Checked @checked = (Token.Checked)@this;
                        Token.Checked checked2 = (Token.Checked)objTemp;
                        int num = @checked.item;
                        int num2 = checked2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 21:
                    {
                        Token.Disabled disabled = (Token.Disabled)@this;
                        Token.Disabled disabled2 = (Token.Disabled)objTemp;
                        int num = disabled.item;
                        int num2 = disabled2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 22:
                    {
                        Token.Enabled enabled = (Token.Enabled)@this;
                        Token.Enabled enabled2 = (Token.Enabled)objTemp;
                        int num = enabled.item;
                        int num2 = enabled2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
                case 23:
                    {
                        Token.Selected selected = (Token.Selected)@this;
                        Token.Selected selected2 = (Token.Selected)objTemp;
                        int num = selected.item;
                        int num2 = selected2.item;
                        if (num < num2)
                        {
                            return -1;
                        }
                        return (num > num2) ? 1 : 0;
                    }
            }
        }
        internal static int GetHashCodecont9(Token @this, Unit unitVar)
        {
            switch (@this.Tag)
            {
                default:
                    {
                        Token.ClassPrefix classPrefix = (Token.ClassPrefix)@this;
                        int num = 0;
                        return -1640531527 + (classPrefix.item + ((num << 6) + (num >> 2)));
                    }
                case 1:
                    {
                        Token.IdPrefix idPrefix = (Token.IdPrefix)@this;
                        int num = 1;
                        return -1640531527 + (idPrefix.item + ((num << 6) + (num >> 2)));
                    }
                case 2:
                    {
                        Token.TagName tagName = (Token.TagName)@this;
                        int num = 2;
                        int num2 = -1640531527;
                        string item = tagName.item2;
                        num = num2 + (((item == null) ? 0 : item.GetHashCode()) + ((num << 6) + (num >> 2)));
                        return -1640531527 + (tagName.item1 + ((num << 6) + (num >> 2)));
                    }
                case 3:
                    {
                        Token.CssClass cssClass = (Token.CssClass)@this;
                        int num = 3;
                        int num3 = -1640531527;
                        string item = cssClass.item2;
                        num = num3 + (((item == null) ? 0 : item.GetHashCode()) + ((num << 6) + (num >> 2)));
                        return -1640531527 + (cssClass.item1 + ((num << 6) + (num >> 2)));
                    }
                case 4:
                    {
                        Token.CssId cssId = (Token.CssId)@this;
                        int num = 4;
                        int num4 = -1640531527;
                        string item = cssId.item2;
                        num = num4 + (((item == null) ? 0 : item.GetHashCode()) + ((num << 6) + (num >> 2)));
                        return -1640531527 + (cssId.item1 + ((num << 6) + (num >> 2)));
                    }
                case 5:
                    {
                        Token.AllChildren allChildren = (Token.AllChildren)@this;
                        int num = 5;
                        return -1640531527 + (allChildren.item + ((num << 6) + (num >> 2)));
                    }
                case 6:
                    {
                        Token.OpenAttribute openAttribute = (Token.OpenAttribute)@this;
                        int num = 6;
                        return -1640531527 + (openAttribute.item + ((num << 6) + (num >> 2)));
                    }
                case 7:
                    {
                        Token.CloseAttribute closeAttribute = (Token.CloseAttribute)@this;
                        int num = 7;
                        return -1640531527 + (closeAttribute.item + ((num << 6) + (num >> 2)));
                    }
                case 8:
                    {
                        Token.AttributeName attributeName = (Token.AttributeName)@this;
                        int num = 8;
                        int num5 = -1640531527;
                        string item = attributeName.item2;
                        num = num5 + (((item == null) ? 0 : item.GetHashCode()) + ((num << 6) + (num >> 2)));
                        return -1640531527 + (attributeName.item1 + ((num << 6) + (num >> 2)));
                    }
                case 9:
                    {
                        Token.AttributeValue attributeValue = (Token.AttributeValue)@this;
                        int num = 9;
                        int num6 = -1640531527;
                        string item = attributeValue.item2;
                        num = num6 + (((item == null) ? 0 : item.GetHashCode()) + ((num << 6) + (num >> 2)));
                        return -1640531527 + (attributeValue.item1 + ((num << 6) + (num >> 2)));
                    }
                case 10:
                    {
                        Token.Assign assign = (Token.Assign)@this;
                        int num = 10;
                        return -1640531527 + (assign.item + ((num << 6) + (num >> 2)));
                    }
                case 11:
                    {
                        Token.EndWith endWith = (Token.EndWith)@this;
                        int num = 11;
                        return -1640531527 + (endWith.item + ((num << 6) + (num >> 2)));
                    }
                case 12:
                    {
                        Token.StartWith startWith = (Token.StartWith)@this;
                        int num = 12;
                        return -1640531527 + (startWith.item + ((num << 6) + (num >> 2)));
                    }
                case 13:
                    {
                        Token.DirectChildren directChildren = (Token.DirectChildren)@this;
                        int num = 13;
                        return -1640531527 + (directChildren.item + ((num << 6) + (num >> 2)));
                    }
                case 14:
                    {
                        Token.Ancestor ancestor = (Token.Ancestor)@this;
                        int num = 14;
                        return -1640531527 + (ancestor.item + ((num << 6) + (num >> 2)));
                    }
                case 15:
                    {
                        Token.AttributeContainsPrefix attributeContainsPrefix = (Token.AttributeContainsPrefix)@this;
                        int num = 15;
                        return -1640531527 + (attributeContainsPrefix.item + ((num << 6) + (num >> 2)));
                    }
                case 16:
                    {
                        Token.AttributeContains attributeContains = (Token.AttributeContains)@this;
                        int num = 16;
                        return -1640531527 + (attributeContains.item + ((num << 6) + (num >> 2)));
                    }
                case 17:
                    {
                        Token.AttributeContainsWord attributeContainsWord = (Token.AttributeContainsWord)@this;
                        int num = 17;
                        return -1640531527 + (attributeContainsWord.item + ((num << 6) + (num >> 2)));
                    }
                case 18:
                    {
                        Token.AttributeNotEqual attributeNotEqual = (Token.AttributeNotEqual)@this;
                        int num = 18;
                        return -1640531527 + (attributeNotEqual.item + ((num << 6) + (num >> 2)));
                    }
                case 19:
                    {
                        Token.Checkbox checkbox = (Token.Checkbox)@this;
                        int num = 19;
                        return -1640531527 + (checkbox.item + ((num << 6) + (num >> 2)));
                    }
                case 20:
                    {
                        Token.Checked @checked = (Token.Checked)@this;
                        int num = 20;
                        return -1640531527 + (@checked.item + ((num << 6) + (num >> 2)));
                    }
                case 21:
                    {
                        Token.Disabled disabled = (Token.Disabled)@this;
                        int num = 21;
                        return -1640531527 + (disabled.item + ((num << 6) + (num >> 2)));
                    }
                case 22:
                    {
                        Token.Enabled enabled = (Token.Enabled)@this;
                        int num = 22;
                        return -1640531527 + (enabled.item + ((num << 6) + (num >> 2)));
                    }
                case 23:
                    {
                        Token.Selected selected = (Token.Selected)@this;
                        int num = 23;
                        return -1640531527 + (selected.item + ((num << 6) + (num >> 2)));
                    }
            }
        }
        internal static bool Equalscont9(Token @this, Token that, Unit unitVar)
        {
            switch (@this.Tag)
            {
                default:
                    {
                        Token.ClassPrefix classPrefix = (Token.ClassPrefix)@this;
                        Token.ClassPrefix classPrefix2 = (Token.ClassPrefix)that;
                        return classPrefix.item == classPrefix2.item;
                    }
                case 1:
                    {
                        Token.IdPrefix idPrefix = (Token.IdPrefix)@this;
                        Token.IdPrefix idPrefix2 = (Token.IdPrefix)that;
                        return idPrefix.item == idPrefix2.item;
                    }
                case 2:
                    {
                        Token.TagName tagName = (Token.TagName)@this;
                        Token.TagName tagName2 = (Token.TagName)that;
                        return tagName.item1 == tagName2.item1 && string.Equals(tagName.item2, tagName2.item2);
                    }
                case 3:
                    {
                        Token.CssClass cssClass = (Token.CssClass)@this;
                        Token.CssClass cssClass2 = (Token.CssClass)that;
                        return cssClass.item1 == cssClass2.item1 && string.Equals(cssClass.item2, cssClass2.item2);
                    }
                case 4:
                    {
                        Token.CssId cssId = (Token.CssId)@this;
                        Token.CssId cssId2 = (Token.CssId)that;
                        return cssId.item1 == cssId2.item1 && string.Equals(cssId.item2, cssId2.item2);
                    }
                case 5:
                    {
                        Token.AllChildren allChildren = (Token.AllChildren)@this;
                        Token.AllChildren allChildren2 = (Token.AllChildren)that;
                        return allChildren.item == allChildren2.item;
                    }
                case 6:
                    {
                        Token.OpenAttribute openAttribute = (Token.OpenAttribute)@this;
                        Token.OpenAttribute openAttribute2 = (Token.OpenAttribute)that;
                        return openAttribute.item == openAttribute2.item;
                    }
                case 7:
                    {
                        Token.CloseAttribute closeAttribute = (Token.CloseAttribute)@this;
                        Token.CloseAttribute closeAttribute2 = (Token.CloseAttribute)that;
                        return closeAttribute.item == closeAttribute2.item;
                    }
                case 8:
                    {
                        Token.AttributeName attributeName = (Token.AttributeName)@this;
                        Token.AttributeName attributeName2 = (Token.AttributeName)that;
                        return attributeName.item1 == attributeName2.item1 && string.Equals(attributeName.item2, attributeName2.item2);
                    }
                case 9:
                    {
                        Token.AttributeValue attributeValue = (Token.AttributeValue)@this;
                        Token.AttributeValue attributeValue2 = (Token.AttributeValue)that;
                        return attributeValue.item1 == attributeValue2.item1 && string.Equals(attributeValue.item2, attributeValue2.item2);
                    }
                case 10:
                    {
                        Token.Assign assign = (Token.Assign)@this;
                        Token.Assign assign2 = (Token.Assign)that;
                        return assign.item == assign2.item;
                    }
                case 11:
                    {
                        Token.EndWith endWith = (Token.EndWith)@this;
                        Token.EndWith endWith2 = (Token.EndWith)that;
                        return endWith.item == endWith2.item;
                    }
                case 12:
                    {
                        Token.StartWith startWith = (Token.StartWith)@this;
                        Token.StartWith startWith2 = (Token.StartWith)that;
                        return startWith.item == startWith2.item;
                    }
                case 13:
                    {
                        Token.DirectChildren directChildren = (Token.DirectChildren)@this;
                        Token.DirectChildren directChildren2 = (Token.DirectChildren)that;
                        return directChildren.item == directChildren2.item;
                    }
                case 14:
                    {
                        Token.Ancestor ancestor = (Token.Ancestor)@this;
                        Token.Ancestor ancestor2 = (Token.Ancestor)that;
                        return ancestor.item == ancestor2.item;
                    }
                case 15:
                    {
                        Token.AttributeContainsPrefix attributeContainsPrefix = (Token.AttributeContainsPrefix)@this;
                        Token.AttributeContainsPrefix attributeContainsPrefix2 = (Token.AttributeContainsPrefix)that;
                        return attributeContainsPrefix.item == attributeContainsPrefix2.item;
                    }
                case 16:
                    {
                        Token.AttributeContains attributeContains = (Token.AttributeContains)@this;
                        Token.AttributeContains attributeContains2 = (Token.AttributeContains)that;
                        return attributeContains.item == attributeContains2.item;
                    }
                case 17:
                    {
                        Token.AttributeContainsWord attributeContainsWord = (Token.AttributeContainsWord)@this;
                        Token.AttributeContainsWord attributeContainsWord2 = (Token.AttributeContainsWord)that;
                        return attributeContainsWord.item == attributeContainsWord2.item;
                    }
                case 18:
                    {
                        Token.AttributeNotEqual attributeNotEqual = (Token.AttributeNotEqual)@this;
                        Token.AttributeNotEqual attributeNotEqual2 = (Token.AttributeNotEqual)that;
                        return attributeNotEqual.item == attributeNotEqual2.item;
                    }
                case 19:
                    {
                        Token.Checkbox checkbox = (Token.Checkbox)@this;
                        Token.Checkbox checkbox2 = (Token.Checkbox)that;
                        return checkbox.item == checkbox2.item;
                    }
                case 20:
                    {
                        Token.Checked @checked = (Token.Checked)@this;
                        Token.Checked checked2 = (Token.Checked)that;
                        return @checked.item == checked2.item;
                    }
                case 21:
                    {
                        Token.Disabled disabled = (Token.Disabled)@this;
                        Token.Disabled disabled2 = (Token.Disabled)that;
                        return disabled.item == disabled2.item;
                    }
                case 22:
                    {
                        Token.Enabled enabled = (Token.Enabled)@this;
                        Token.Enabled enabled2 = (Token.Enabled)that;
                        return enabled.item == enabled2.item;
                    }
                case 23:
                    {
                        Token.Selected selected = (Token.Selected)@this;
                        Token.Selected selected2 = (Token.Selected)that;
                        return selected.item == selected2.item;
                    }
            }
        }
        internal static bool Equalscont91(Token @this, Token obj, Unit unitVar)
        {
            switch (@this.Tag)
            {
                default:
                    {
                        Token.ClassPrefix classPrefix = (Token.ClassPrefix)@this;
                        Token.ClassPrefix classPrefix2 = (Token.ClassPrefix)obj;
                        return classPrefix.item == classPrefix2.item;
                    }
                case 1:
                    {
                        Token.IdPrefix idPrefix = (Token.IdPrefix)@this;
                        Token.IdPrefix idPrefix2 = (Token.IdPrefix)obj;
                        return idPrefix.item == idPrefix2.item;
                    }
                case 2:
                    {
                        Token.TagName tagName = (Token.TagName)@this;
                        Token.TagName tagName2 = (Token.TagName)obj;
                        return tagName.item1 == tagName2.item1 && string.Equals(tagName.item2, tagName2.item2);
                    }
                case 3:
                    {
                        Token.CssClass cssClass = (Token.CssClass)@this;
                        Token.CssClass cssClass2 = (Token.CssClass)obj;
                        return cssClass.item1 == cssClass2.item1 && string.Equals(cssClass.item2, cssClass2.item2);
                    }
                case 4:
                    {
                        Token.CssId cssId = (Token.CssId)@this;
                        Token.CssId cssId2 = (Token.CssId)obj;
                        return cssId.item1 == cssId2.item1 && string.Equals(cssId.item2, cssId2.item2);
                    }
                case 5:
                    {
                        Token.AllChildren allChildren = (Token.AllChildren)@this;
                        Token.AllChildren allChildren2 = (Token.AllChildren)obj;
                        return allChildren.item == allChildren2.item;
                    }
                case 6:
                    {
                        Token.OpenAttribute openAttribute = (Token.OpenAttribute)@this;
                        Token.OpenAttribute openAttribute2 = (Token.OpenAttribute)obj;
                        return openAttribute.item == openAttribute2.item;
                    }
                case 7:
                    {
                        Token.CloseAttribute closeAttribute = (Token.CloseAttribute)@this;
                        Token.CloseAttribute closeAttribute2 = (Token.CloseAttribute)obj;
                        return closeAttribute.item == closeAttribute2.item;
                    }
                case 8:
                    {
                        Token.AttributeName attributeName = (Token.AttributeName)@this;
                        Token.AttributeName attributeName2 = (Token.AttributeName)obj;
                        return attributeName.item1 == attributeName2.item1 && string.Equals(attributeName.item2, attributeName2.item2);
                    }
                case 9:
                    {
                        Token.AttributeValue attributeValue = (Token.AttributeValue)@this;
                        Token.AttributeValue attributeValue2 = (Token.AttributeValue)obj;
                        return attributeValue.item1 == attributeValue2.item1 && string.Equals(attributeValue.item2, attributeValue2.item2);
                    }
                case 10:
                    {
                        Token.Assign assign = (Token.Assign)@this;
                        Token.Assign assign2 = (Token.Assign)obj;
                        return assign.item == assign2.item;
                    }
                case 11:
                    {
                        Token.EndWith endWith = (Token.EndWith)@this;
                        Token.EndWith endWith2 = (Token.EndWith)obj;
                        return endWith.item == endWith2.item;
                    }
                case 12:
                    {
                        Token.StartWith startWith = (Token.StartWith)@this;
                        Token.StartWith startWith2 = (Token.StartWith)obj;
                        return startWith.item == startWith2.item;
                    }
                case 13:
                    {
                        Token.DirectChildren directChildren = (Token.DirectChildren)@this;
                        Token.DirectChildren directChildren2 = (Token.DirectChildren)obj;
                        return directChildren.item == directChildren2.item;
                    }
                case 14:
                    {
                        Token.Ancestor ancestor = (Token.Ancestor)@this;
                        Token.Ancestor ancestor2 = (Token.Ancestor)obj;
                        return ancestor.item == ancestor2.item;
                    }
                case 15:
                    {
                        Token.AttributeContainsPrefix attributeContainsPrefix = (Token.AttributeContainsPrefix)@this;
                        Token.AttributeContainsPrefix attributeContainsPrefix2 = (Token.AttributeContainsPrefix)obj;
                        return attributeContainsPrefix.item == attributeContainsPrefix2.item;
                    }
                case 16:
                    {
                        Token.AttributeContains attributeContains = (Token.AttributeContains)@this;
                        Token.AttributeContains attributeContains2 = (Token.AttributeContains)obj;
                        return attributeContains.item == attributeContains2.item;
                    }
                case 17:
                    {
                        Token.AttributeContainsWord attributeContainsWord = (Token.AttributeContainsWord)@this;
                        Token.AttributeContainsWord attributeContainsWord2 = (Token.AttributeContainsWord)obj;
                        return attributeContainsWord.item == attributeContainsWord2.item;
                    }
                case 18:
                    {
                        Token.AttributeNotEqual attributeNotEqual = (Token.AttributeNotEqual)@this;
                        Token.AttributeNotEqual attributeNotEqual2 = (Token.AttributeNotEqual)obj;
                        return attributeNotEqual.item == attributeNotEqual2.item;
                    }
                case 19:
                    {
                        Token.Checkbox checkbox = (Token.Checkbox)@this;
                        Token.Checkbox checkbox2 = (Token.Checkbox)obj;
                        return checkbox.item == checkbox2.item;
                    }
                case 20:
                    {
                        Token.Checked @checked = (Token.Checked)@this;
                        Token.Checked checked2 = (Token.Checked)obj;
                        return @checked.item == checked2.item;
                    }
                case 21:
                    {
                        Token.Disabled disabled = (Token.Disabled)@this;
                        Token.Disabled disabled2 = (Token.Disabled)obj;
                        return disabled.item == disabled2.item;
                    }
                case 22:
                    {
                        Token.Enabled enabled = (Token.Enabled)@this;
                        Token.Enabled enabled2 = (Token.Enabled)obj;
                        return enabled.item == enabled2.item;
                    }
                case 23:
                    {
                        Token.Selected selected = (Token.Selected)@this;
                        Token.Selected selected2 = (Token.Selected)obj;
                        return selected.item == selected2.item;
                    }
            }
        }
    }
}