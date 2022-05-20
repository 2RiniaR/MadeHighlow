using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateComponent
{
    public record RejectedResult(
        [NotNull] CreateComponentAction Action,
        [NotNull] CreateComponentProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CreateComponentEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : CreateComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
