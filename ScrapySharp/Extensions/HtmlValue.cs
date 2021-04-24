using System;
using System.Globalization;

namespace ScrapySharp.Extensions
{
    public class HtmlValue : IEquatable<HtmlValue>
    {
        private readonly string value;

        public HtmlValue(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }

        public static implicit operator string(HtmlValue htmlValue)
        {
            if (htmlValue == null)
                return null;
            return htmlValue.value;
        }

        public static implicit operator HtmlValue(string value)
        {
            return new(value);
        }

        public static explicit operator bool(HtmlValue htmlValue)
        {
            if (htmlValue == null)
                return false;
            return Convert.ToBoolean(htmlValue.value);
        }

        public static explicit operator bool?(HtmlValue htmlValue)
        {
            if (bool.TryParse(htmlValue.value, out var result))
                return result;
            return null;
        }

        public static explicit operator int(HtmlValue htmlValue)
        {
            return int.Parse(htmlValue.value, NumberStyles.AllowLeadingWhite 
                | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
        }

        public static explicit operator int?(HtmlValue htmlValue)
        {
            if (int.TryParse(htmlValue.value, out var result))
                return result;
            return null;
        }

        public static explicit operator uint(HtmlValue htmlValue)
        {
            return uint.Parse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite 
                | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, 
                NumberFormatInfo.InvariantInfo);
        }

        public static explicit operator uint?(HtmlValue htmlValue)
        {
            var styles = NumberStyles.AllowLeadingWhite 
                         | NumberStyles.AllowTrailingWhite 
                         | NumberStyles.AllowLeadingSign
                         | NumberStyles.AllowDecimalPoint
                         | NumberStyles.AllowExponent;
            if (uint.TryParse(htmlValue.value, styles, NumberFormatInfo.InvariantInfo, out var result))
                return result;
            return null;
        }

        public static explicit operator long(HtmlValue htmlValue)
        {
            return long.Parse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
        }

        public static explicit operator long?(HtmlValue htmlValue)
        {
            long result;
            if (long.TryParse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
                return result;
            return null;
        }

        public static explicit operator ulong(HtmlValue htmlValue)
        {
            return ulong.Parse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
        }

        public static explicit operator ulong?(HtmlValue htmlValue)
        {
            if (ulong.TryParse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                                                | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, 
                out var result))
                return result;
            return null;
        }

        public static explicit operator float(HtmlValue htmlValue)
        {
            return float.Parse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
        }

        public static explicit operator float?(HtmlValue htmlValue)
        {
            if (float.TryParse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                                                | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, 
                out var result))
                return result;
            return null;
        }

        public static explicit operator double(HtmlValue htmlValue)
        {
            return double.Parse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
        }

        public static explicit operator double?(HtmlValue htmlValue)
        {
            if (double.TryParse(htmlValue.value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign
                                                 | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, 
                out var result))
                return result;
            return null;
        }

        public static explicit operator decimal(HtmlValue htmlValue) => Convert.ToDecimal(htmlValue.value);

        public static explicit operator decimal?(HtmlValue htmlValue)
        {
            if (decimal.TryParse(htmlValue.value, out var result))
                return result;
            return null;
        }

        public static explicit operator DateTime(HtmlValue htmlValue)
        {
            return htmlValue == null ? DateTime.MinValue : htmlValue.value.ToDate();
        }

        public static explicit operator DateTime?(HtmlValue htmlValue)
        {
            if (htmlValue == null)
                return null;
            return htmlValue.value.ToDate();
        }
        
        public static explicit operator TimeSpan(HtmlValue htmlValue)
        {
            return htmlValue == null ? TimeSpan.Zero : TimeSpan.Parse(htmlValue.value);
        }

        public static explicit operator TimeSpan?(HtmlValue htmlValue)
        {
            if (TimeSpan.TryParse(htmlValue.value, out var result))
                return result;
            return null;
        }

        public static explicit operator Guid(HtmlValue htmlValue)
        {
            return htmlValue == null ? Guid.Empty : new Guid(htmlValue.value);
        }

        public static explicit operator Guid?(HtmlValue htmlValue)
        {
            if (htmlValue == null)
                return null;
            return new Guid(htmlValue.value);
        }

        #region IEquatable implementation

        public bool Equals(HtmlValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.value, value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (HtmlValue)) return false;
            return Equals((HtmlValue) obj);
        }

        public override int GetHashCode()
        {
            return (value != null ? value.GetHashCode() : 0);
        }

        public static bool operator ==(HtmlValue left, HtmlValue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HtmlValue left, HtmlValue right)
        {
            return !Equals(left, right);
        }

        #endregion

    }
}