using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Expressions
{
    public class TurnDurationTest
    {
        #region Construtor

        [Test]
        public void Constructor_Get_ReturnsThatTypeIsCounterpart()
        {
            var duration = new TurnDuration();

            var actual = duration.Type;

            Assert.That(actual, Is.EqualTo(DurationType.FromTurn));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void Constructor_Positive_ReturnsThatValueIsSame(int value)
        {
            var duration = new TurnDuration(value);

            var actual = duration.Value;

            Assert.That(actual, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanZero_ReturnsThatValueIsZero()
        {
            var duration = new TurnDuration(-1);

            var actual = duration.Value;

            Assert.That(actual, Is.EqualTo(0));
        }

        #endregion
    }
}