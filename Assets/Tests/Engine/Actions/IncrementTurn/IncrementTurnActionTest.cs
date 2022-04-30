using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnActionTest
    {
        #region Run

        [Test]
        public void Run_Initialized_ReturnsEvent()
        {
            var @event = new IncrementTurnAction();

            var actual = @event.Run(new Session());

            Assert.That(actual, Is.EqualTo(new IncrementTurnEvent()));
        }

        [Test]
        public void Run_Uninitialized_ThrowsArgumentException()
        {
            var @event = new IncrementTurnAction();

            var actual = @event.Run(new Session());

            Assert.That(actual, Is.EqualTo(new IncrementTurnEvent()));
        }

        #endregion
    }
}