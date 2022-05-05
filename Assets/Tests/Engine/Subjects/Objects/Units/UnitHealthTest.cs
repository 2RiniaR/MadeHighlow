using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnitHealthTest
    {
        #region Constructor

        [Test]
        [TestCase(EntityHealth.MinValue)]
        [TestCase(EntityHealth.MinValue + 1)]
        [TestCase(EntityHealth.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new EntityHealth(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new EntityHealth(EntityHealth.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(EntityHealth.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new EntityHealth(EntityHealth.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(EntityHealth.MaxValue));
        }

        #endregion
    }
}