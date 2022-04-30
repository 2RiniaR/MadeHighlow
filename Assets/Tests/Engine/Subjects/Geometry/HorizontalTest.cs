using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class HorizontalTest
    {
        #region AddOperator

        [Test]
        public void AddOperator_Always_ReturnsAdded()
        {
            var actual = new Horizontal(1) + 1;

            Assert.That(actual, Is.EqualTo(new Horizontal(2)));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Always_ReturnsSubtracted()
        {
            var actual = new Horizontal(2) - 1;

            Assert.That(actual, Is.EqualTo(new Horizontal(1)));
        }

        #endregion
    }
}