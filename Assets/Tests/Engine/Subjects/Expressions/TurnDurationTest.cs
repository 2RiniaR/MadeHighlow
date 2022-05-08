using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class TurnDurationTest
    {
        #region Construtor

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void Constructor_NonNegative_ReturnsSame(int value)
        {
            var actual = new TurnDuration(value);

            Assert.That(actual.Value, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_Negative_ReturnsZero()
        {
            var actual = new TurnDuration(-1);

            Assert.That(actual.Value, Is.EqualTo(0));
        }

        #endregion

        #region Decrement

        [Test]
        public void Decrement_Positive_ReturnsSubtracted()
        {
            var duration = new TurnDuration(1);

            var actual = duration.Decrement();

            Assert.That(actual, Is.EqualTo(new TurnDuration()));
        }

        [Test]
        public void Decrement_Zero_ReturnsNull()
        {
            var duration = new TurnDuration();

            var actual = duration.Decrement();

            Assert.That(actual, Is.Null);
        }

        #endregion
    }
}
