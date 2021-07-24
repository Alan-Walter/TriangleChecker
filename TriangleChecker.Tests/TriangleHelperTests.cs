using NUnit.Framework;

using System;
using System.Collections;

namespace TriangleChecker.Tests
{
    [TestFixture]
    public class TriangleHelperTests
    {
        private static readonly double[] InvalidValues = new double[]
        {
            double.NaN, double.PositiveInfinity, double.NegativeInfinity, 1
        };

        private static readonly double[] LowerOrEqualZero = new double[] { -10, 0, 6 };

        [Test, TestCaseSource(nameof(GetInvalidValuesTestCaseData))]
        public void DetermineType_InvalidValues_ThrowsException(double a, double b, double c)
        {
            TestDelegate action = () => TriangleHelper.DetermineType(a, b, c);

            Assert.Throws<ArgumentException>(action);
        }

        [Test, TestCaseSource(nameof(GetLowerOrEqualZeroTestCaseData))]
        public void DetermineType_LowerOrEqualZero_ThrowsException(double a, double b, double c)
        {
            TestDelegate action = () => TriangleHelper.DetermineType(a, b, c);

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Test]
        [TestCase(4, 2, 2)]
        [TestCase(1, 5, 2)]
        [TestCase(1, 1, 10)]
        public void DetermineType_InvalidSides_ReturnNoneType(double a, double b, double c)
        {
            var triangleType = TriangleHelper.DetermineType(a, b, c);

            Assert.AreEqual(TriangleType.None, triangleType);
        }

        [Test]
        [TestCase(3, 3, 3, ExpectedResult = TriangleType.Acute)]
        [TestCase(4, 5, 4, ExpectedResult = TriangleType.Acute)]
        [TestCase(3, 4, 5, ExpectedResult = TriangleType.Right)]
        [TestCase(15, 12, 9, ExpectedResult = TriangleType.Right)]
        [TestCase(6, 5, 9, ExpectedResult = TriangleType.Obtuse)]
        [TestCase(11, 9, 4, ExpectedResult = TriangleType.Obtuse)]
        public TriangleType DetermineType_CorrectSides_ReturnExpectedValue(double a, double b, double c)
        {
            var triangleType = TriangleHelper.DetermineType(a, b, c);

            return triangleType;
        }


        #region Private

        private static IEnumerable GetInvalidValuesTestCaseData()
        {
            foreach (var a in InvalidValues)
            {
                foreach (var b in InvalidValues)
                {
                    foreach (var c in InvalidValues)
                    {
                        if (double.IsFinite(a) && double.IsFinite(b) && double.IsFinite(c))
                        {
                            continue;
                        }

                        yield return new TestCaseData(a, b, c);
                    }
                }
            }
        }

        private static IEnumerable GetLowerOrEqualZeroTestCaseData()
        {
            foreach (var a in LowerOrEqualZero)
            {
                foreach (var b in LowerOrEqualZero)
                {
                    foreach (var c in LowerOrEqualZero)
                    {
                        if (a > 0 && b > 0 && c > 0)
                        {
                            continue;
                        }

                        yield return new TestCaseData(a, b, c);
                    }
                }
            }
        }

        #endregion
    }
}