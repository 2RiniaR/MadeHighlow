using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.IncrementTurn
{
    public class EvaluatorTest
    {
        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var historyMock = new Mock<IHistory>();
            HistoryBuilder.New.UseMock(historyMock).SetWorld(Constants.BeforeWorld).Setup();
            var evaluator = new Evaluator(historyMock.Object);

            var actual = evaluator.Evaluate();

            Assert.That(actual, Is.EqualTo(Constants.SucceedResult));
        }
    }
}
