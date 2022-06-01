using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public class CreatePlayerActionTest
    {
        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var stubHistory = new Mock<IHistory>();
            var action = new CreatePlayerAction(PlayerGenerator.Empty);

            var actual = action.Evaluate(stubHistory.Object);

            // var expected = new SucceedResult(
            //     action,
            //     new CreatePlayerProcess()
            // );
            // Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_CreateComponentFailed_ReturnsFailed()
        {
        }
    }
}
