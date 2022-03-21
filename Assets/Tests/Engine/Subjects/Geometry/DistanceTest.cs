using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class DistanceTest
    {
        #region Constructor

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void Constructor_Positive_ReturnsThatValueIsSame(int value)
        {
            var distance = new Distance(value);

            var actual = distance.Value;

            Assert.That(actual, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanZero_ReturnsThatValueIsZero()
        {
            var distance = new Distance(-1);

            var actual = distance.Value;

            Assert.That(actual, Is.EqualTo(0));
        }

        #endregion
    }
}