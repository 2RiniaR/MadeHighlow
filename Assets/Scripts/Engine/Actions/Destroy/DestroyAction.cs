using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record DestroyAction() : Action(ActionType.Destroy)
    {
        [NotNull] public ObjectLocator ObjectID { get; init; } = new();

        public Result Run(in Session session)
        {
            return new DestroyResult { Actor = ObjectID };
        }
    }
}