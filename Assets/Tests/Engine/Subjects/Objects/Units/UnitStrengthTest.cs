using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units
{
    public class UnitStrengthTest
    {
        #region Constructor

        [Test]
        [TestCase(UnitStrength.MinValue)]
        [TestCase(UnitStrength.MinValue + 1)]
        [TestCase(UnitStrength.MaxValue)]
        public void Constructor_InRange_ReturnsThatValueIsSame(int value)
        {
            var strength = new UnitStrength(value);

            var actual = strength.Value;

            Assert.That(actual, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsThatValueIsMin()
        {
            var strength = new UnitStrength(UnitStrength.MinValue - 1);

            var actual = strength.Value;

            Assert.That(actual, Is.EqualTo(UnitStrength.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsThatValueIsMax()
        {
            var strength = new UnitStrength(UnitStrength.MaxValue + 1);

            var actual = strength.Value;

            Assert.That(actual, Is.EqualTo(UnitStrength.MaxValue));
        }

        #endregion
    }
}