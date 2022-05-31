using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterPlayer
{
    public class RegisterPlayerActionTest
    {
        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var stubHistory = new Mock<IHistory>();
            var action = new RegisterPlayerAction(ID.From(1), PlayerGenerator.Empty);

            var actual = action.Evaluate(stubHistory.Object);

            var expected = new RegisterPlayerResult(
                action,
                PlayerGenerator.Empty with { ID = ID.From(1), Components = ValueList<Component>.Empty }
            );
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
