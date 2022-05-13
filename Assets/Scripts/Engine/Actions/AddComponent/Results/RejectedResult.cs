using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record RejectedResult(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus,
        [NotNull] RegisterComponent.SucceedResult RegisterComponentResult,
        [NotNull] ValueList<Result> InitializeResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<AddComponentEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
