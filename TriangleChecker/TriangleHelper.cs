using System;

namespace TriangleChecker
{
    public static class TriangleHelper
    {
        public static TriangleType DetermineType(double a, double b, double c)
        {
            ValidateDoubleValue(a, nameof(a));
            ValidateDoubleValue(b, nameof(b));
            ValidateDoubleValue(c, nameof(c));

            SortSides(ref a, ref b, ref c);
            if (a + b <= c)
            {
                return TriangleType.None;
            }

            var diff = a * a + b * b - c * c;

            if (diff < 0)
            {
                return TriangleType.Obtuse;
            }
            else if (diff == 0)
            {
                return TriangleType.Right;
            }
            else
            {
                return TriangleType.Acute;
            }
        }

        #region Private

        private static void SortSides<T>(ref T a, ref T b, ref T c) where T : IComparable
        {
            if (a.CompareTo(b) > 0)
            {
                Swap(ref a, ref b);
            }

            if (b.CompareTo(c) > 0)
            {
                Swap(ref b, ref c);
            }

            if (a.CompareTo(b) > 0)
            {
                Swap(ref a, ref b);
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        private static void ValidateDoubleValue(double val, string paramName)
        {
            if (!double.IsFinite(val))
            {
                throw new ArgumentException("Invalid value", paramName);
            }

            if (val <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than 0");
            }
        }

        #endregion
    }
}
