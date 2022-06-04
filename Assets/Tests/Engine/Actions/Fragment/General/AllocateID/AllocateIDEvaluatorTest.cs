using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDEvaluatorTest
    {
        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var historyMock = new Mock<IHistory>();
            historyMock.SetupGet(history => history.World)
                .Returns(WorldGenerator.Empty with { LatestAllocatedID = ID.From(1) });
            var evaluator = new Evaluator(historyMock.Object);

            var actual = evaluator.Evaluate();

            var expected = new Result(ID.From(2));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
