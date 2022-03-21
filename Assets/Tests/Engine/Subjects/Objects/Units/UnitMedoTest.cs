using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Units
{
    public class UnitMedoTest
    {
        #region Constructor

        [Test]
        [TestCase(UnitMedo.MinValue)]
        [TestCase(UnitMedo.MinValue + 1)]
        [TestCase(UnitMedo.MaxValue)]
        public void Constructor_InRange_ReturnsThatValueIsSame(int value)
        {
            var medo = new UnitMedo(value);

            var actual = medo.Value;

            Assert.That(actual, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsThatValueIsMin()
        {
            var medo = new UnitMedo(UnitMedo.MinValue - 1);

            var actual = medo.Value;

            Assert.That(actual, Is.EqualTo(UnitMedo.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsThatValueIsMax()
        {
            var medo = new UnitMedo(UnitMedo.MaxValue + 1);

            var actual = medo.Value;

            Assert.That(actual, Is.EqualTo(UnitMedo.MaxValue));
        }

        #endregion
    }
}