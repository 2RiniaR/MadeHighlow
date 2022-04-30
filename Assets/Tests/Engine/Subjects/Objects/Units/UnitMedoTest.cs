using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnitMedoTest
    {
        #region Constructor

        [Test]
        [TestCase(UnitMedo.MinValue)]
        [TestCase(UnitMedo.MinValue + 1)]
        [TestCase(UnitMedo.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new UnitMedo(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new UnitMedo(UnitMedo.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(UnitMedo.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new UnitMedo(UnitMedo.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(UnitMedo.MaxValue));
        }

        #endregion
    }
}