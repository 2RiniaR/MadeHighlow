using JetBrains.Annotations;
using Moq;

namespace RineaR.MadeHighlow.Actions.CreatePlayer
{
    public static class Constants
    {
        public static Component UnattachedComponent1 { get; } = ComponentGenerator.Empty;
        public static Component AttachedComponent1 { get; } = ComponentGenerator.Empty with { ID = ID.From(2) };
        public static Component UnattachedComponent2 { get; } = ComponentGenerator.Empty;
        public static Component AttachedComponent2 { get; } = ComponentGenerator.Empty with { ID = ID.From(3) };
        public static Component UnattachedComponent3 { get; } = ComponentGenerator.Empty;
        public static Component AttachedComponent3 { get; } = ComponentGenerator.Empty with { ID = ID.From(4) };

        public static ID AllocatedID { get; } = ID.From(1);

        public static Player InitialProps { get; } = PlayerGenerator.Empty with
        {
            Components = new ValueList<Component>(UnattachedComponent1, UnattachedComponent2, UnattachedComponent3),
        };

        public static Player Registered { get; } = InitialProps with
        {
            ID = AllocatedID,
            Components = ValueList<Component>.Empty,
        };

        public static Player Created { get; } = InitialProps with
        {
            ID = AllocatedID,
            Components = new ValueList<Component>(AttachedComponent1, AttachedComponent2, AttachedComponent3),
        };

        public static World BeforeWorld { get; } = WorldGenerator.Empty with { Players = ValueList<Player>.Empty };
        public static World AfterWorld { get; } = BeforeWorld with { Players = new ValueList<Player>(Created) };

        public static Event<AllocateID.Result> AllocateIDEvent { get; } = new(
            new EventID(ID.From(2)),
            new EventID(ID.From(1)),
            new AllocateID.Result(AllocatedID)
        );

        public static Event<RegisterPlayer.Result> RegisterPlayerEvent { get; } = new(
            new EventID(ID.From(3)),
            AllocateIDEvent.ID,
            new RegisterPlayer.Result(Mock.Of<RegisterPlayer.IAction>()) { Registered = Registered }
        );

        public static Event<CreateComponent.Result> CreateComponentEvent1 { get; } = new(
            new EventID(ID.From(4)),
            RegisterPlayerEvent.ID,
            new CreateComponent.Result(Mock.Of<CreateComponent.IAction>()) { Created = AttachedComponent1 }
        );

        public static Event<CreateComponent.Result> CreateComponentEvent2 { get; } = new(
            new EventID(ID.From(5)),
            CreateComponentEvent1.ID,
            new CreateComponent.Result(Mock.Of<CreateComponent.IAction>()) { Created = AttachedComponent2 }
        );

        public static Event<CreateComponent.Result> CreateComponentEvent2Failed { get; } = new(
            new EventID(ID.From(5)),
            CreateComponentEvent1.ID,
            new CreateComponent.Result(Mock.Of<CreateComponent.IAction>()) { Created = null }
        );

        public static Event<CreateComponent.Result> CreateComponentEvent3 { get; } = new(
            new EventID(ID.From(6)),
            CreateComponentEvent2.ID,
            new CreateComponent.Result(Mock.Of<CreateComponent.IAction>()) { Created = AttachedComponent3 }
        );

        public static Result SucceedResult([NotNull] IAction action)
        {
            return new Result(action)
            {
                AllocateID = AllocateIDEvent,
                CreateComponents = new ValueList<Event<CreateComponent.Result>>(
                    CreateComponentEvent1,
                    CreateComponentEvent2,
                    CreateComponentEvent3
                ),
                Created = Created,
            };
        }

        public static Result FailedResult([NotNull] IAction action)
        {
            return new Result(action)
            {
                AllocateID = AllocateIDEvent,
                CreateComponents = new ValueList<Event<CreateComponent.Result>>(
                    CreateComponentEvent1,
                    CreateComponentEvent2Failed
                ),
                Created = null,
            };
        }
    }
}
