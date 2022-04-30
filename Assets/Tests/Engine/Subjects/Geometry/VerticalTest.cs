using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class VerticalTest
    {
        #region AddOperator

        [Test]
        public void AddOperator_Always_ReturnsAdded()
        {
            var actual = new Vertical(1) + 1;

            Assert.That(actual, Is.EqualTo(new Vertical(2)));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Always_ReturnsSubtracted()
        {
            var actual = new Vertical(2) - 1;

            Assert.That(actual, Is.EqualTo(new Vertical(1)));
        }

        #endregion
    }
}