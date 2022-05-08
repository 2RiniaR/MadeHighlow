using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class UnlimitedDurationTest
    {
        #region Decrement

        [Test]
        public void Decrement_Unlimited_ReturnsSame()
        {
            var duration = new UnlimitedDuration();

            var actual = duration.Decrement();

            Assert.That(actual, Is.EqualTo(duration));
        }

        #endregion
    }
}
