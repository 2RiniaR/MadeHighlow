using Moq;
using NUnit.Framework;
using RineaR.MadeHighlow.Actions.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public class CreateComponentEvaluatorTest
    {
        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            var context = contextMock.Object;

            var stubPlayer = PlayerGenerator.Empty with { ID = ID.From(1) };
            var stubCurrentWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(stubPlayer) };
            var stubComponent = ComponentGenerator.Empty;
            var action = new Action(stubPlayer.PlayerID, stubComponent);

            var allocateIDEvent = new Event<AllocateID.Result>(
                new EventID(ID.From(2)),
                new EventID(ID.From(1)),
                new AllocateID.Result(ID.From(2))
            );
            var allocateIDWorld = stubCurrentWorld with { LatestAllocatedID = allocateIDEvent.Content.Allocated };

            var registerComponentEvent = new Event<RegisterComponent.SucceedResult>(
                new EventID(ID.From(3)),
                new EventID(ID.From(2)),
                new RegisterComponent.SucceedResult(
                    RegisterComponentActionGenerator.Empty,
                    stubComponent with { ID = ID.From(2), AttachedID = stubPlayer.PlayerID }
                )
            );
            var registerComponentWorld = allocateIDWorld with
            {
                Players = new ValueList<Player>(
                    stubPlayer with
                    {
                        Components = new ValueList<Component>(
                            stubComponent with { ID = ID.From(2), AttachedID = stubPlayer.PlayerID }
                        ),
                    }
                ),
            };

            var registerComponentHistory = new Mock<IHistory>();
            registerComponentHistory.SetupWorld(registerComponentWorld);

            var allocateIDHistory = new Mock<IHistory>();
            allocateIDHistory.SetupWorld(allocateIDWorld);
            allocateIDHistory.SetupNextEvent(registerComponentEvent, registerComponentHistory.Object);

            var initialHistory = new Mock<IHistory>();
            initialHistory.SetupWorld(stubCurrentWorld);
            initialHistory.SetupNextEvent(allocateIDEvent, allocateIDHistory.Object);

            var evaluator = new Evaluator(context, initialHistory.Object, action);

            var actual = evaluator.Evaluate();

            var expected = new SucceedResult(
                action,
                new Process(allocateIDEvent, registerComponentEvent),
                ValueList<Interrupt<RejectionContext>>.Empty
            );
            Assert.That(actual, Is.EqualTo(expected));
        }

        private record RejectorComponent(ID ID, IAttachableID AttachedID, Duration Duration) : Component(
            ID,
            AttachedID,
            Duration
        ), ICreateComponentRejector
        {
            public Priority Priority { get; } = new(0);

            public Interrupt<RejectionContext> CreateComponentRejection(
                IHistory history,
                Action action,
                Process process,
                ValueList<Interrupt<RejectionContext>> collected
            )
            {
                return new Interrupt<RejectionContext>(new Priority(0), ComponentID, new RejectionContext());
            }
        }

        [Test]
        public void Evaluate_Rejected_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            var context = contextMock.Object;

            var stubPlayer = PlayerGenerator.Empty with
            {
                ID = ID.From(1),
                Components = new ValueList<Component>(
                    new RejectorComponent(ID.From(2), new PlayerID(ID.From(1)), new UnlimitedDuration())
                ),
            };
            var stubCurrentWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(stubPlayer) };
            var stubComponent = ComponentGenerator.Empty;
            var action = new Action(stubPlayer.PlayerID, stubComponent);

            var allocateIDEvent = new Event<AllocateID.Result>(
                new EventID(ID.From(2)),
                new EventID(ID.From(1)),
                new AllocateID.Result(ID.From(2))
            );
            var allocateIDWorld = stubCurrentWorld with { LatestAllocatedID = allocateIDEvent.Content.Allocated };

            var registerComponentEvent = new Event<RegisterComponent.SucceedResult>(
                new EventID(ID.From(3)),
                new EventID(ID.From(2)),
                new RegisterComponent.SucceedResult(
                    RegisterComponentActionGenerator.Empty,
                    stubComponent with { ID = ID.From(2), AttachedID = stubPlayer.PlayerID }
                )
            );
            var registerComponentWorld = allocateIDWorld with
            {
                Players = new ValueList<Player>(
                    stubPlayer with
                    {
                        Components = new ValueList<Component>(
                            stubComponent with { ID = ID.From(2), AttachedID = stubPlayer.PlayerID }
                        ),
                    }
                ),
            };

            var registerComponentHistory = new Mock<IHistory>();
            registerComponentHistory.SetupWorld(registerComponentWorld);

            var allocateIDHistory = new Mock<IHistory>();
            allocateIDHistory.SetupWorld(allocateIDWorld);
            allocateIDHistory.SetupNextEvent(registerComponentEvent, registerComponentHistory.Object);

            var initialHistory = new Mock<IHistory>();
            initialHistory.SetupWorld(stubCurrentWorld);
            initialHistory.SetupNextEvent(allocateIDEvent, allocateIDHistory.Object);

            var evaluator = new Evaluator(context, initialHistory.Object, action);

            var actual = evaluator.Evaluate();

            var expected = new RejectedResult(
                action,
                new Process(allocateIDEvent, registerComponentEvent),
                new ValueList<Interrupt<RejectionContext>>(
                    new Interrupt<RejectionContext>(
                        new Priority(0),
                        stubPlayer.Components[0].ComponentID,
                        new RejectionContext()
                    )
                ),
                stubPlayer.Components[0].ComponentID
            );
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_RegisterFailed_ReturnsFailed()
        {
            var contextMock = new Mock<IEvaluationContext>();
            var context = contextMock.Object;

            var stubPlayer = PlayerGenerator.Empty with { ID = ID.From(1) };
            var stubCurrentWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(stubPlayer) };
            var stubComponent = ComponentGenerator.Empty;
            var action = new Action(stubPlayer.PlayerID, stubComponent);

            var allocateIDEvent = new Event<AllocateID.Result>(
                new EventID(ID.From(2)),
                new EventID(ID.From(1)),
                new AllocateID.Result(ID.From(2))
            );
            var allocateIDWorld = stubCurrentWorld with { LatestAllocatedID = allocateIDEvent.Content.Allocated };

            var registerComponentEvent = new Event<RegisterComponent.SucceedResult>(
                new EventID(ID.From(3)),
                new EventID(ID.From(2)),
                new RegisterComponent.SucceedResult(
                    RegisterComponentActionGenerator.Empty,
                    stubComponent with { ID = ID.From(2), AttachedID = stubPlayer.PlayerID }
                )
            );
            var registerComponentWorld = allocateIDWorld with
            {
                Players = new ValueList<Player>(
                    stubPlayer with
                    {
                        Components = new ValueList<Component>(
                            stubComponent with { ID = ID.From(2), AttachedID = stubPlayer.PlayerID }
                        ),
                    }
                ),
            };

            var registerComponentHistory = new Mock<IHistory>();
            registerComponentHistory.SetupWorld(registerComponentWorld);

            var allocateIDHistory = new Mock<IHistory>();
            allocateIDHistory.SetupWorld(allocateIDWorld);
            allocateIDHistory.SetupNextEvent(registerComponentEvent, registerComponentHistory.Object);

            var initialHistory = new Mock<IHistory>();
            initialHistory.SetupWorld(stubCurrentWorld);
            initialHistory.SetupNextEvent(allocateIDEvent, allocateIDHistory.Object);

            var evaluator = new Evaluator(context, initialHistory.Object, action);

            var actual = evaluator.Evaluate();

            var expected = new SucceedResult(
                action,
                new Process(allocateIDEvent, registerComponentEvent),
                ValueList<Interrupt<RejectionContext>>.Empty
            );
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
