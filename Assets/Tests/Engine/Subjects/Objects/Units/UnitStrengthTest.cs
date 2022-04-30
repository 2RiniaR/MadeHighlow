using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnitStrengthTest
    {
        #region Constructor

        [Test]
        [TestCase(UnitStrength.MinValue)]
        [TestCase(UnitStrength.MinValue + 1)]
        [TestCase(UnitStrength.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new UnitStrength(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new UnitStrength(UnitStrength.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(UnitStrength.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new UnitStrength(UnitStrength.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(UnitStrength.MaxValue));
        }

        #endregion
    }
}