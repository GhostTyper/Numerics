using System;
using System.Collections.Generic;
using System.Text;

namespace SharpFast.Numerics
{
    public struct SmallNumber
    {
        private int data;
        public bool IsPositiveOverflow => data == int.MaxValue;

        public bool IsNegativeOverflow => data == int.MinValue;

        public static SmallNumber MaxValue => new SmallNumber(int.MaxValue - 0.001);

        public static SmallNumber MinValue => new SmallNumber(int.MinValue - 0.001);

        public SmallNumber(byte number)
        {
            data = number * 1000;
        }

        public SmallNumber(char number)
        {
            data = number * 1000;
        }

        public SmallNumber(short number)
        {
            data = number * 1000;
        }

        public SmallNumber(int number)
        {
            if (number >= 2147483)
                data = int.MaxValue;
            else if (number <= -2147483)
                data = int.MinValue;
            else
                data = number * 1000;
        }

        public SmallNumber(uint number)
        {
            if (number >= 2147483)
                data = int.MaxValue;
            else
                data = (int)number * 1000;
        }

        public SmallNumber(long number)
        {
            if (number >= 2147483)
                data = int.MaxValue;
            else if (number <= -2147483)
                data = int.MinValue;
            else
                data = (int)number * 1000;
        }

        public SmallNumber(ulong number)
        {
            if (number >= 2147483)
                data = int.MaxValue;
            else
                data = (int)number * 1000;
        }

        public SmallNumber(float number)
        {
            if (number >= 2147483.6465)
                data = int.MaxValue;
            else if (number <= -2147483.6475)
                data = int.MinValue;
            else
                data = (int)(number * 1000.0 + 0.5);
        }

        public SmallNumber(double number)
        {
            if (number >= 2147483.6465)
                data = int.MaxValue;
            else if (number <= -2147483.6475)
                data = int.MinValue;
            else
                data = (int)(number * 1000.0 + 0.5);
        }

        public SmallNumber(decimal number)
        {
            if (number >= (decimal)2147483.6465)
                data = int.MaxValue;
            else if (number <= (decimal)-2147483.6475)
                data = int.MinValue;
            else
                data = (int)(number * (decimal)1000.0 + (decimal)0.5);
        }



        public static implicit operator SmallNumber(byte number)
        {
            return new SmallNumber(number);
        }

        public static explicit operator byte(SmallNumber number)
        {
            if (number > byte.MaxValue)
                return byte.MaxValue;

            if (number < byte.MinValue)
                return byte.MinValue;

            return (byte)number;
        }

        public static implicit operator SmallNumber(char number)
        {
            return new SmallNumber(number);
        }

        public static explicit operator char(SmallNumber number)
        {
            if (number > char.MaxValue)
                return char.MaxValue;

            if (number < char.MinValue)
                return char.MinValue;

            return (char)number;
        }

        public static implicit operator SmallNumber(short number)
        {
            return new SmallNumber(number);
        }

        public static explicit operator short(SmallNumber number)
        {
            if (number > short.MaxValue)
                return short.MaxValue;

            if (number < short.MinValue)
                return short.MinValue;

            return (short)number;
        }

        public static implicit operator SmallNumber(uint number)
        {
           return new SmallNumber(number);
        }

        public static explicit operator SmallNumber(long number)
        {
            return new SmallNumber(number);
        }

        public static implicit operator long(SmallNumber number)
        {
            return number;
        }

        public static explicit operator SmallNumber(ulong number)
        {
            return new SmallNumber(number);
        }

        public static implicit operator ulong(SmallNumber number)
        {
            return number;
        }

        public static explicit operator SmallNumber(float number)
        {
            return new SmallNumber(number);
        }

        public static implicit operator float(SmallNumber number)
        {
            return number;
        }

        public static explicit operator SmallNumber(double number)
        {
            return new SmallNumber(number);
        }

        public static implicit operator double(SmallNumber number)
        {
            return number;
        }

        public static explicit operator SmallNumber(decimal number)
        {
            return new SmallNumber(number);
        }

        public static implicit operator decimal(SmallNumber number)
        {
            return number;
        }

        public static SmallNumber operator+ (SmallNumber l, SmallNumber r)
        {
            SmallNumber tmp;

            long number = (long)l.data + r.data;

            if (number > int.MaxValue)
                number = int.MaxValue;
            else if (number < int.MinValue)
                number = int.MinValue;

            tmp = new SmallNumber();

            tmp.data = (int)number;

            return tmp;
        }

        public static SmallNumber operator- (SmallNumber l, SmallNumber r)
        {
            long number = (long)l.data - r.data;

            if (number > int.MaxValue)
                number = int.MaxValue;
            else if (number < int.MinValue)
                number = int.MinValue;

            SmallNumber tmp = new SmallNumber();

            tmp.data = (int)number;

            return tmp;
        }

        public static SmallNumber operator* (SmallNumber l, SmallNumber r)
        {
            long number = ((long)l.data * r.data) / 1000;


            if (number > int.MaxValue)
                number = int.MaxValue;
            else if (number < int.MinValue)
                number = int.MinValue;

            SmallNumber tmp = new SmallNumber();

            tmp.data = (int)number;

            return tmp;
        }

        public static SmallNumber operator /(SmallNumber l, SmallNumber r)
        {
            if (r.data < 1000)
            {
                r.data *= 1000;
                l.data *= 1000;
            }

            long right = r.data / 1000;

            long number = (l.data / right);

            if (number > int.MaxValue)
                number = int.MaxValue;
            else if (number < int.MinValue)
                number = int.MinValue;

            SmallNumber tmp = new SmallNumber();

            tmp.data = (int)number;

            return tmp;
        }

        public static bool operator ==(SmallNumber l, SmallNumber r)
        {
            return l.data == r.data;
        }

        public static bool operator !=(SmallNumber l, SmallNumber r)
        {
            return l.data != r.data;
        }

        public static bool operator >(SmallNumber l, SmallNumber r)
        {
            return l.data > r.data;
        }

        public static bool operator <(SmallNumber l, SmallNumber r)
        {
            return l.data < r.data;
        }

        public static bool operator <=(SmallNumber l, SmallNumber r)
        {
            return l.data <= r.data;
        }

        public static bool operator >=(SmallNumber l, SmallNumber r)
        {
            return l.data >= r.data;
        }

        public static SmallNumber operator %(SmallNumber l, SmallNumber r)
        {
            SmallNumber tmp = new SmallNumber();
                
            tmp.data = l.data % r.data;

            return tmp;
        }

        public override string ToString()
        {
            if (data == int.MaxValue)
                return "Positive Overflow";

            if (data == int.MinValue)
                return "Negative Overflow";

            string text = data.ToString();

            if (data < 0)
            {
                if (data > -1000)
                    return $"-0.{-data:000}";

                return $"{text.Substring(0, text.Length - 3)}.{text.Substring(text.Length - 3, 3)}";
            }
            else
            {
                if (text.Length < 4)
                    return $"0.{data:000}";

                return $"{text.Substring(0, text.Length - 3)}.{text.Substring(text.Length - 3, 3)}";
            }
        }
    }
}
