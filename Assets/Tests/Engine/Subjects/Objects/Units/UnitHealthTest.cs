using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnitHealthTest
    {
        #region Constructor

        [Test]
        [TestCase(Health.MinValue)]
        [TestCase(Health.MinValue + 1)]
        [TestCase(Health.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new Health(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new Health(Health.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(Health.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new Health(Health.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(Health.MaxValue));
        }

        #endregion
    }
}