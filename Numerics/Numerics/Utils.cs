namespace SharpFast.Numerics
{
    /// <summary>
    /// This is the numerical utility class. It manages primitive calculations (eg. adding, subtracting,
    /// negativating, multiplying, dividing, remainder calculations) for big integer operations.
    ///
    /// The static functions in this static class require you to align the numbers little-endian.Make
    /// sure you understand this concept before using this class because it contains unsafe code which
    /// may damage the memory of the program you use those functions in.
    ///
    /// When writing arabic decimal numbers onto a piece of paper the lowest significant number stands
    /// on the right and the number with the highest significance on the left.
    ///
    /// However in the memory when using little-endian alignment the least significant byte can be found
    /// on the left side while the most significant byte is found on the right side.
    ///
    /// This knowledge is crucial since some methods require you to specify your data as uint* and some
    /// as ulong*. Because this library uses little-endian your alignment must make sure that an uint at
    /// a lower address has a lower significance valuewise.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// The quickest way of dividing bigger numbers I could come up with. I challenge you to find
        /// a better way. :)
        ///
        /// The given pointers are limited in their amount by the int-parameters you specify. However
        /// this routine will scan the data you present to it to avoid unused cycles. And no, you didn't
        /// find a better way of dividing, when you just split up this method in various methods for
        /// various amounts of int* or some inner loops.
        ///
        /// *result will contain the result of the division. This result isn't rounded or whatever.
        /// *divisor should contain the divisor and shouldn't contain 0 in every slot. This method will
        /// punish doing so with return false. *numerator should contain the numerator and will contain
        /// the remainder at the end (=after calling this method).
        ///
        /// *num will be limited to nums uint pieces, *div is limited to divs uint pieces, *res is limited
        /// to ress uint pieces.
        /// 
        /// Works completely on little-endian formattings as described in the class documentation comment.
        /// </summary>
        /// <param name="numeratorStart">The pointer of an little endian formatted uint sequence. This will
        /// contain the remainder after the successful method call.</param>
        /// <param name="numeratorEnd">The last element of the remainder.</param>
        /// <param name="divisorStart">The pointer of an little endian formatted uint sequence specifying
        /// the divisor.</param>
        /// <param name="divisorEnd">The last element of the divisor.</param>
        /// <param name="resultStart">The divided result. This need to be initialized with zeros.</param>
        /// <param name="resultEnd">The last element of the result.</param>
        /// <returns>true, if the method succeeded, false otherwise. false will only happen if you tried
        /// to divide by zero.</returns>
        public static unsafe bool Divide(uint* numeratorStart, uint* numeratorEnd, uint* divisorStart,
            uint* divisorEnd, uint* resultStart, uint* resultEnd)
        {
            uint* numerator;
            uint* numeratorDivisorBorder;
            uint* divisor;
            uint* result;

            // First we make sure we loop through the smallest amount of uint pieces.

            while (*divisorEnd == 0 && divisorEnd > divisorStart)
                divisorEnd--;

            if (divisorStart >= divisorEnd)  // Divisor is 0 and we don't support dividing by zero.
                return false;

            while (*numeratorEnd == 0 && numeratorEnd > numeratorStart)
                numeratorEnd--;

            // Numerator is < Divisors, we don't need to do anything here, because the result must be zero.
            if (numeratorEnd - numeratorStart < divisorEnd - divisorStart)
                return true;

            // We need to shift the divisor a maximum to the left so our guessing algorithm can do a good job.

            int shift = 0;
            int backShift;

            uint divHi = *divisorEnd;
            uint divLo = divisorEnd > divisorStart ? *(divisorEnd - 1) : 0;

            while (divHi < 4194304)
            {
                shift += 10;
                divHi <<= 10;
            }

            while (divHi < 536870912)
            {
                shift += 3;
                divHi <<= 3;
            }

            while (divHi < 2147483648U)
            {
                shift++;
                divHi <<= 1;
            }

            if (shift == 0)
                backShift = 32;
            else
            {
                backShift = 32 - shift;

                divHi |= divLo >> backShift;

                if (divisorEnd - divisorStart > 2)
                    divLo = (divLo << shift) | ((*divisorEnd - 2) >> backShift);
                else
                    divLo <<= shift;
            }

            // Whis code in majority is just outrolled the same converted to pointer only like the BigInteger
            // implementation .NET Core 3.

            for (numerator = numeratorEnd, numeratorDivisorBorder = numeratorStart + (divisorEnd - divisorStart);
                numerator >= numeratorDivisorBorder; numerator--)
            {

            }

            return true;
        }
    }
}
