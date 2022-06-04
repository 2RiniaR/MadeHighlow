using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnEvaluatorTest
    {
        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var historyMock = new Mock<IHistory>();
            historyMock.SetupGet(history => history.World)
                .Returns(WorldGenerator.Empty with { CurrentTurn = new Turn(1) });
            var evaluator = new IncrementTurnEvaluator(historyMock.Object);

            var actual = evaluator.Evaluate();

            var expected = new IncrementTurnResult(new Turn(2));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
