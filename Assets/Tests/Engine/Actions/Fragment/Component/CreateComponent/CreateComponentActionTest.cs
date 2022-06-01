using Moq;
using NUnit.Framework;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public class CreateComponentActionTest
    {
        [Test]
        public void Evaluate_Valid_ReturnsSucceed()
        {
            var stubPlayer = PlayerGenerator.Empty with { ID = ID.From(1) };
            var stubCurrentWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(stubPlayer) };
            var stubComponent = ComponentGenerator.Empty;
            var action = new CreateComponentAction(stubPlayer.PlayerID, stubComponent);

            var allocateIDEvent = new Event<AllocateIDResult>(
                new EventID(ID.From(2)),
                new EventID(ID.From(1)),
                new AllocateIDResult(AllocateIDActionGenerator.Empty, ID.From(2))
            );
            var allocateIDWorld = stubCurrentWorld with { LatestAllocatedID = allocateIDEvent.Result.AllocatedID };

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

            var actual = action.Evaluate(initialHistory.Object);

            var expected = new SucceedResult(
                action,
                new CreateComponentProcess(allocateIDEvent, registerComponentEvent),
                ValueList<Interrupt<CreateComponentRejection>>.Empty
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

            public Interrupt<CreateComponentRejection> CreateComponentRejection(
                IHistory history,
                CreateComponentAction action,
                CreateComponentProcess process,
                ValueList<Interrupt<CreateComponentRejection>> collected
            )
            {
                return new Interrupt<CreateComponentRejection>(
                    new Priority(0),
                    ComponentID,
                    new CreateComponentRejection()
                );
            }
        }

        [Test]
        public void Evaluate_Rejected_ReturnsFailed()
        {
            var stubPlayer = PlayerGenerator.Empty with
            {
                ID = ID.From(1),
                Components = new ValueList<Component>(
                    new RejectorComponent(ID.From(2), new PlayerID(ID.From(1)), new UnlimitedDuration())
                ),
            };
            var stubCurrentWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(stubPlayer) };
            var stubComponent = ComponentGenerator.Empty;
            var action = new CreateComponentAction(stubPlayer.PlayerID, stubComponent);

            var allocateIDEvent = new Event<AllocateIDResult>(
                new EventID(ID.From(2)),
                new EventID(ID.From(1)),
                new AllocateIDResult(AllocateIDActionGenerator.Empty, ID.From(2))
            );
            var allocateIDWorld = stubCurrentWorld with { LatestAllocatedID = allocateIDEvent.Result.AllocatedID };

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

            var actual = action.Evaluate(initialHistory.Object);

            var expected = new RejectedResult(
                action,
                new CreateComponentProcess(allocateIDEvent, registerComponentEvent),
                new ValueList<Interrupt<CreateComponentRejection>>(
                    new Interrupt<CreateComponentRejection>(
                        new Priority(0),
                        stubPlayer.Components[0].ComponentID,
                        new CreateComponentRejection()
                    )
                ),
                stubPlayer.Components[0].ComponentID
            );
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Evaluate_RegisterFailed_ReturnsFailed()
        {
            var stubPlayer = PlayerGenerator.Empty with { ID = ID.From(1) };
            var stubCurrentWorld = WorldGenerator.Empty with { Players = new ValueList<Player>(stubPlayer) };
            var stubComponent = ComponentGenerator.Empty;
            var action = new CreateComponentAction(stubPlayer.PlayerID, stubComponent);

            var allocateIDEvent = new Event<AllocateIDResult>(
                new EventID(ID.From(2)),
                new EventID(ID.From(1)),
                new AllocateIDResult(AllocateIDActionGenerator.Empty, ID.From(2))
            );
            var allocateIDWorld = stubCurrentWorld with { LatestAllocatedID = allocateIDEvent.Result.AllocatedID };

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

            var actual = action.Evaluate(initialHistory.Object);

            var expected = new SucceedResult(
                action,
                new CreateComponentProcess(allocateIDEvent, registerComponentEvent),
                ValueList<Interrupt<CreateComponentRejection>>.Empty
            );
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
