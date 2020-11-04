using SharpFast.Numerics;
using System;

namespace Development
{
    class Program
    {
        static void Main(string[] args)
        {
            SmallNumber number = new SmallNumber(0.1337);

            number = new SmallNumber((decimal)0.1337);

            Console.WriteLine(number);
        }
    }
}
