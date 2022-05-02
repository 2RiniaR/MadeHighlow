using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public record DestroyAction() : Action(ActionType.Destroy)
    {
        [NotNull] public EntityLocator EntityID { get; init; } = new();

        public Result Run(in Session session)
        {
            return new DestroyResult { Actor = EntityID };
        }
    }
}