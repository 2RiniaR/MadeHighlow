using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units
{
    public class UnitHealthTest
    {
        #region Constructor

        [Test]
        [TestCase(UnitHealth.MinValue)]
        [TestCase(UnitHealth.MinValue + 1)]
        [TestCase(UnitHealth.MaxValue)]
        public void Constructor_InRange_ReturnsThatValueIsSame(int value)
        {
            var health = new UnitHealth(value);

            var actual = health.Value;

            Assert.That(actual, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsThatValueIsMin()
        {
            var health = new UnitHealth(UnitHealth.MinValue - 1);

            var actual = health.Value;

            Assert.That(actual, Is.EqualTo(UnitHealth.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsThatValueIsMax()
        {
            var health = new UnitHealth(UnitHealth.MaxValue + 1);

            var actual = health.Value;

            Assert.That(actual, Is.EqualTo(UnitHealth.MaxValue));
        }

        #endregion
    }
}