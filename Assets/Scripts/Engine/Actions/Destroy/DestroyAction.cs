using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Destroy
{
    public record DestroyAction() : Action(ActionType.Destroy)
    {
        [NotNull] public ObjectLocator ObjectID { get; init; } = new();

        public Event Run(in Session session)
        {
            return new DestroyEvent { Actor = ObjectID };
        }
    }
}