using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class IncrementTurnEvaluatorTest
    {
        private static IHistory GenerateHistory()
        {
            var historyMock = new Mock<IHistory>();
            var world = WorldGenerator.Empty with { CurrentTurn = new Turn(0) };
            historyMock.SetupWorld(world);
            return historyMock.Object;
        }

        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var evaluator = new Evaluator(GenerateHistory());

            var actual = evaluator.Evaluate();

            var expected = new Result(new Turn(1));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
