using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpFast.Numerics;
using System;

namespace Tests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public unsafe void IsThisOrThatOk()
        {
            uint[] left = new uint[4] { 5, 0, 0, 0 };
            uint[] right = new uint[4] { 6, 0, 0, 0 };
            uint[] result = new uint[4] { 0, 0, 0, 0 };

            fixed (uint* leftStart = left)
            fixed (uint* rightStart = right)
            fixed (uint* resultStart = result)
            {
                //Utils.Add(leftStart, leftStart + left.Length, rightStart, rightStart + right.Length, resultStart, resultStart + result.Length);
            }

            Assert.AreEqual(11, result[0], "First segment mismatch.");
            Assert.AreEqual(0, result[1], "Second segment mismatch.");
            Assert.AreEqual(0, result[2], "Third segment mismatch.");
            Assert.AreEqual(0, result[3], "Fourth segment mismatch.");
        }

        [TestMethod]
        public unsafe void SmallNumberTests()
        {
            {
                SmallNumber a = new SmallNumber(6);
                SmallNumber b = new SmallNumber(5);

                SmallNumber resultAdd = a + b;

                Assert.IsTrue(resultAdd == new SmallNumber(11));

                SmallNumber resultSub = a - b;

                Assert.IsTrue(resultSub == new SmallNumber(1));

                SmallNumber resultDiv = a / b;

                Assert.IsTrue(resultDiv == new SmallNumber(1.2));

                SmallNumber resultMult = a * b;

                Assert.IsTrue(resultMult == new SmallNumber(30));

                Assert.IsFalse(a == b);
                Assert.IsTrue(a != b);

                Assert.IsFalse(a < b);
                Assert.IsTrue(a > b);
            }

            {
                SmallNumber a = new SmallNumber(21474836465);
                SmallNumber b = new SmallNumber(5000);

                SmallNumber resultAdd = a + b;

                Assert.IsTrue(resultAdd == new SmallNumber(int.MaxValue));

                SmallNumber resultSub = a - b;

                Assert.IsTrue(resultSub == new SmallNumber(2147483.647 - 5000));

                SmallNumber resultDiv = a / b;

               // Assert.IsTrue(resultDiv == new SmallNumber(2147483.647 / 5000));

                SmallNumber resultMult = a * b;

                Assert.IsTrue(resultMult == new SmallNumber(int.MaxValue));

                Assert.IsFalse(a == b);
                Assert.IsTrue(a != b);

                Assert.IsFalse(a < b);
                Assert.IsTrue(a > b);
            }

        }
    }
}
