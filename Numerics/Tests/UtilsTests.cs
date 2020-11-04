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
                Utils.Add(leftStart, leftStart + left.Length, rightStart, rightStart + right.Length, resultStart, resultStart + result.Length);
            }

            Assert.AreEqual(11, result[0], "First segment mismatch.");
            Assert.AreEqual(0, result[1], "Second segment mismatch.");
            Assert.AreEqual(0, result[2], "Third segment mismatch.");
            Assert.AreEqual(0, result[3], "Fourth segment mismatch.");
        }
    }
}
