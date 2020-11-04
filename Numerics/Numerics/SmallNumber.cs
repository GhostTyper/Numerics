using System;
using System.Collections.Generic;
using System.Text;

namespace SharpFast.Numerics
{
    public struct SmallNumber
    {
        private int data;

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

        public implicit operator SmallNumber(uint number)
        {
            new SmallNumber(number);
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
