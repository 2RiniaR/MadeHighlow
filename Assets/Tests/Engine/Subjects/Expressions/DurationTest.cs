using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Expressions
{
    public class DurationTest
    {
        #region Unlimited

        [Test]
        public void Unlimited_Get_ReturnsThatTypeIsCounterpart()
        {
            var duration = Duration.Unlimited;

            var actual = duration.Type;

            Assert.That(actual, Is.EqualTo(DurationType.Unlimited));
        }

        #endregion
    }
}