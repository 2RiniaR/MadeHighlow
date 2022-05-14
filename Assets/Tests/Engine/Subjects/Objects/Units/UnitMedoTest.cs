using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnitMedoTest
    {
        #region Constructor

        [Test]
        [TestCase(Medo.MinValue)]
        [TestCase(Medo.MinValue + 1)]
        [TestCase(Medo.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new Medo(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new Medo(Medo.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(Medo.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new Medo(Medo.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(Medo.MaxValue));
        }

        #endregion
    }
}