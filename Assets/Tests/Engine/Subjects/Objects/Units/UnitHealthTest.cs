using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnitHealthTest
    {
        #region Constructor

        [Test]
        [TestCase(UnitHealth.MinValue)]
        [TestCase(UnitHealth.MinValue + 1)]
        [TestCase(UnitHealth.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new UnitHealth(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new UnitHealth(UnitHealth.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(UnitHealth.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new UnitHealth(UnitHealth.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(UnitHealth.MaxValue));
        }

        #endregion
    }
}