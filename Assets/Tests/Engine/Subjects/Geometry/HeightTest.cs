using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class HeightTest
    {
        #region AddOperator

        [Test]
        public void AddOperator_Always_ReturnsAdded()
        {
            var actual = new Height(1) + 1;

            Assert.That(actual, Is.EqualTo(new Height(2)));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Always_ReturnsSubtracted()
        {
            var actual = new Height(2) - 1;

            Assert.That(actual, Is.EqualTo(new Height(1)));
        }

        #endregion

        #region Constructor

        [Test]
        [TestCase(Height.MinValue)]
        [TestCase(Height.MinValue + 1)]
        [TestCase(Height.MaxValue)]
        public void Constructor_Valid_ReturnsSame(int value)
        {
            var actual = new Height(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsMin()
        {
            var actual = new Height(Height.MinValue - 1);

            Assert.That(actual.Value, Is.EqualTo(Height.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsMax()
        {
            var actual = new Height(Height.MaxValue + 1);

            Assert.That(actual.Value, Is.EqualTo(Height.MaxValue));
        }

        #endregion
    }
}