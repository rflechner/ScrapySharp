using System;
using System.Globalization;

namespace ScrapySharp.Extensions
{
    public class HtmlValue : IEquatable<HtmlValue>
    {
        private const NumberStyles ParsingNumberStyles = 
            NumberStyles.AllowLeadingWhite 
            | NumberStyles.AllowTrailingWhite 
            | NumberStyles.AllowLeadingSign
            | NumberStyles.AllowDecimalPoint
            | NumberStyles.AllowExponent;

        private readonly string value;

        public HtmlValue(string value)
        {
            this.value = value;
        }

        public override string ToString() => value;

        public static implicit operator string(HtmlValue htmlValue) => htmlValue == null ? null : htmlValue.value;

        public static implicit operator HtmlValue(string value) => new(value);

        public static explicit operator bool(HtmlValue htmlValue) => htmlValue != null && Convert.ToBoolean(htmlValue.value);

        public static explicit operator bool?(HtmlValue htmlValue)
        {
            if (bool.TryParse(htmlValue.value, out var result))
                return result;
            return null;
        }

        public static explicit operator int(HtmlValue htmlValue) => int.Parse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo);

        public static explicit operator int?(HtmlValue htmlValue)
        {
            if (int.TryParse(htmlValue.value, out var result))
                return result;
            return null;
        }

        public static explicit operator uint(HtmlValue htmlValue) => uint.Parse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo);

        public static explicit operator uint?(HtmlValue htmlValue)
        {
            if (uint.TryParse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo, out var result))
                return result;
            return null;
        }

        public static explicit operator long(HtmlValue htmlValue) => long.Parse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo);

        public static explicit operator long?(HtmlValue htmlValue)
        {
            if (long.TryParse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo, out var result))
                return result;
            return null;
        }

        public static explicit operator ulong(HtmlValue htmlValue) => ulong.Parse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo);

        public static explicit operator ulong?(HtmlValue htmlValue)
        {
            if (ulong.TryParse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo, out var result))
                return result;
            return null;
        }

        public static explicit operator float(HtmlValue htmlValue) => float.Parse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo);

        public static explicit operator float?(HtmlValue htmlValue)
        {
            if (float.TryParse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo, out var result))
                return result;
            return null;
        }

        public static explicit operator double(HtmlValue htmlValue) => double.Parse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo);

        public static explicit operator double?(HtmlValue htmlValue)
        {
            if (double.TryParse(htmlValue.value, ParsingNumberStyles, NumberFormatInfo.InvariantInfo, out var result))
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

        public static explicit operator DateTime(HtmlValue htmlValue) => htmlValue == null ? DateTime.MinValue : htmlValue.value.ToDate();

        public static explicit operator DateTime?(HtmlValue htmlValue)
        {
            if (htmlValue == null)
                return null;
            return htmlValue.value.ToDate();
        }
        
        public static explicit operator TimeSpan(HtmlValue htmlValue) => htmlValue == null ? TimeSpan.Zero : TimeSpan.Parse(htmlValue.value);

        public static explicit operator TimeSpan?(HtmlValue htmlValue)
        {
            if (TimeSpan.TryParse(htmlValue.value, out var result))
                return result;
            return null;
        }

        public static explicit operator Guid(HtmlValue htmlValue) => htmlValue == null ? Guid.Empty : new Guid(htmlValue.value);

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

        public override int GetHashCode() => value != null ? value.GetHashCode() : 0;

        public static bool operator ==(HtmlValue left, HtmlValue right) => Equals(left, right);

        public static bool operator !=(HtmlValue left, HtmlValue right) => !Equals(left, right);

        #endregion

    }
}