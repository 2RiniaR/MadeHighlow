using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class DurationTest
    {
        #region Unlimited

        [Test]
        public void Unlimited_Always_ReturnsCounterpartType()
        {
            var actual = Duration.Unlimited;

            Assert.That(actual.Type, Is.EqualTo(DurationType.Unlimited));
        }

        #endregion

        #region Decrement

        [Test]
        public void Decrement_Unlimited_ReturnsSame()
        {
            var duration = Duration.Unlimited;

            var actual = duration.Decrement();

            Assert.That(actual, Is.EqualTo(duration));
        }

        #endregion
    }
}