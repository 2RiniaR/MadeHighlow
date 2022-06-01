using Moq;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public class RegisterComponentActionTest
    {
        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var stubPlayerID = ID.From(1);
            var stubPlayer = PlayerGenerator.Empty with { ID = stubPlayerID };
            var stubCurrentWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(stubPlayer) };
            var stubHistory = new Mock<IHistory>();
            stubHistory.SetupGet(history => history.World).Returns(stubCurrentWorld);

            var stubComponentID = ID.From(2);
            var stubComponent = ComponentGenerator.Empty;
            var action = new RegisterComponentAction(stubPlayer.PlayerID, stubComponentID, stubComponent);

            var actual = action.Evaluate(stubHistory.Object);

            var expected = new SucceedResult(
                action,
                stubComponent with { ID = stubComponentID, AttachedID = stubPlayer.PlayerID }
            );
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_NoParent_ReturnsFailed()
        {
            var stubPlayerID = ID.From(1);
            var stubPlayer = PlayerGenerator.Empty with { ID = stubPlayerID };
            var stubCurrentWorld = WorldGenerator.Empty;
            var stubHistory = new Mock<IHistory>();
            stubHistory.SetupGet(history => history.World).Returns(stubCurrentWorld);

            var stubComponentID = ID.From(2);
            var stubComponent = ComponentGenerator.Empty;
            var action = new RegisterComponentAction(stubPlayer.PlayerID, stubComponentID, stubComponent);

            var actual = action.Evaluate(stubHistory.Object);

            var expected = new ParentNotFoundResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
