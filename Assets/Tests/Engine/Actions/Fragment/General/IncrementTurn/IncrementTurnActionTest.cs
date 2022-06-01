using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnActionTest
    {
        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var stubHistory = new Mock<IHistory>();
            stubHistory.SetupGet(history => history.World)
                .Returns(WorldGenerator.Empty with { CurrentTurn = new Turn(1) });
            var action = new IncrementTurnEvaluator();

            var actual = action.Evaluate(stubHistory.Object);

            var expected = new IncrementTurnResult(action, new Turn(2));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
