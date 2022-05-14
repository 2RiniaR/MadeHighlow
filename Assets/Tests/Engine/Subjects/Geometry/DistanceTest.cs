using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class DistanceTest
    {
        #region Constructor

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void Constructor_NonNegative_ReturnsSame(int value)
        {
            var actual = new Distance(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_Negative_ReturnsZero()
        {
            var actual = new Distance(-1);

            Assert.That(actual.Value, Is.EqualTo(0));
        }

        #endregion
    }
}