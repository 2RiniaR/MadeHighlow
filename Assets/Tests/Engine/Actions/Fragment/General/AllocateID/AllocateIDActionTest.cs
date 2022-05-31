using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.AllocateID
{
    public class AllocateIDActionTest
    {
        [Test]
        public void Evaluate_Always_ReturnsResult()
        {
            var stubHistory = new Mock<IHistory>();
            stubHistory.SetupGet(history => history.World)
                .Returns(WorldGenerator.Empty with { LatestAllocatedID = ID.From(1) });
            var action = new AllocateIDAction();

            var actual = action.Evaluate(stubHistory.Object);

            var expected = new AllocateIDResult(action, ID.From(2));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
