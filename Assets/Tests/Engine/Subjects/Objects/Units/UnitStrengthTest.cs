using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnitStrengthTest
    {
        #region Constructor

        [Test]
        [TestCase(Strength.MinValue)]
        [TestCase(Strength.MinValue + 1)]
        [TestCase(Strength.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new Strength(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new Strength(Strength.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(Strength.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new Strength(Strength.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(Strength.MaxValue));
        }

        #endregion
    }
}