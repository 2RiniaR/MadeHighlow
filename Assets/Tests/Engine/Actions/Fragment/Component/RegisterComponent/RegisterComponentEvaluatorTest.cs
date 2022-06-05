using Moq;
using NUnit.Framework;
using RineaR.MadeHighlow.Actions.CreateComponent.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.RegisterComponent
{
    public class RegisterComponentEvaluatorTest
    {
        private static ID PlayerID { get; } = ID.From(1);
        private static ID ComponentID { get; } = ID.From(2);

        private static Player Player => PlayerGenerator.Empty with { ID = PlayerID };

        private static World PlayerExistingWorld { get; }
            = WorldGenerator.Empty with { Players = new ValueList<Player>(Player) };

        private static World PlayerNotExistingWorld { get; }
            = WorldGenerator.Empty with { Players = ValueList<Player>.Empty };

        private static Component ComponentStatus => ComponentGenerator.Empty;

        private static IHistory HistoryWith(World world)
        {
            var mock = new Mock<IHistory>();
            mock.SetupGet(history => history.World).Returns(world);
            return mock.Object;
        }

        private static IEvaluationContext ContextWith(Player foundPlayer)
        {
            var mock = new Mock<IEvaluationContext>();
            mock.Setup(context => context.Finder.FindAttachable(It.IsAny<World>(), Player.PlayerID))
                .Returns(foundPlayer);
            return mock.Object;
        }

        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var context = ContextWith(foundPlayer: Player);
            var history = HistoryWith(PlayerExistingWorld);
            var action = new Action(Player.PlayerID, ComponentID, ComponentStatus);
            var evaluator = new Evaluator(context, history, action);

            var actual = evaluator.Evaluate();

            var expected = new SucceedResult(
                action,
                ComponentStatus with { ID = ComponentID, AttachedID = Player.PlayerID }
            );
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_NoParent_ReturnsFailed()
        {
            var context = ContextWith(foundPlayer: null);
            var history = HistoryWith(PlayerNotExistingWorld);
            var action = new Action(Player.PlayerID, ComponentID, ComponentStatus);
            var evaluator = new Evaluator(context, history, action);

            var actual = evaluator.Evaluate();

            var expected = new ParentNotFoundResult(action);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
