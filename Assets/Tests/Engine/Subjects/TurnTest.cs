using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class TurnTest
    {
        #region Increment

        [Test]
        public void Increment_Valid_ReturnsIncremented()
        {
            var turn = new Turn(1);

            var actual = turn.Increment();

            Assert.That(actual, Is.EqualTo(new Turn(2)));
        }

        [Test]
        public void Increment_IntMax_ReturnsSame()
        {
            var turn = new Turn(int.MaxValue);

            var actual = turn.Increment();

            Assert.That(actual, Is.EqualTo(turn));
        }

        #endregion
    }
}