using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDEvaluatorTest
    {
        private static IHistory GenerateHistory()
        {
            var historyMock = new Mock<IHistory>();
            var world = WorldGenerator.Empty with { NextID = ID.From(1) };
            historyMock.SetupWorld(world);
            return historyMock.Object;
        }

        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var evaluator = new Evaluator(GenerateHistory());

            var actual = evaluator.Evaluate();

            var expected = new Result(ID.From(1));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
