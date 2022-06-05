using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.Rejection;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public static class Constants
    {
        public static Player Parent { get; } = PlayerGenerator.Empty with { ID = ID.From(1) };

        public static ID ComponentID { get; } = ID.From(2);
        public static Component InitialProps { get; } = ComponentGenerator.Empty;

        public static Component Created { get; } = InitialProps with { ID = ComponentID, AttachedID = Parent.PlayerID };

        public static World BeforeWorld { get; }
            = WorldGenerator.Empty with { Players = new ValueList<Player>(Parent) };

        public static World AfterWorld { get; } = BeforeWorld with
        {
            Players = new ValueList<Player>(Parent with { Components = new ValueList<Component>(Created) }),
        };

        public static Component RejectedComponent { get; } = ComponentGenerator.Empty with { ID = ID.From(3) };
        public static Rejection Rejection { get; } = new(RejectedComponent.ComponentID);

        public static Event<AllocateID.Result> AllocateIDEvent { get; } = new(
            new EventID(ID.From(2)),
            new EventID(ID.From(1)),
            new AllocateID.Result(ComponentID)
        );

        public static Result SucceedResult([NotNull] IAction action)
        {
            return new Result(action)
            {
                AllocateID = AllocateIDEvent,
                Rejection = null,
                Created = Created,
            };
        }

        public static Result RejectedResult([NotNull] IAction action)
        {
            return new Result(action)
            {
                AllocateID = AllocateIDEvent,
                Rejection = Rejection,
                Created = null,
            };
        }

        public static Result FailedResult([NotNull] IAction action)
        {
            return new Result(action)
            {
                AllocateID = null,
                Rejection = null,
                Created = null,
            };
        }
    }
}
