using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class RegisterPlayerEvaluatorTest
    {
        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var context = new Mock<IEvaluationContext>().Object;
            var history = new Mock<IHistory>().Object;
            var action = new Action(ID.From(1), PlayerGenerator.Empty);
            var evaluator = new Evaluator(context, history, action);

            var actual = evaluator.Evaluate();

            var expected = new Result(
                action,
                PlayerGenerator.Empty with { ID = ID.From(1), Components = ValueList<Component>.Empty }
            );
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
