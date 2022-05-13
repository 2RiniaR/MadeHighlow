using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent
{
    public record SucceedResult(
        [NotNull] IAttachableID TargetID,
        [NotNull] Component InitialStatus,
        [NotNull] RegisterComponent.SucceedResult RegisterComponentResult,
        [NotNull] ValueList<Result> InitializeResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<AddComponentEffect>> Interrupts,
        [NotNull] Component Added
    ) : AddComponentResult
    {
        public override World Simulate(World world)
        {
            return new Timeline().Then(RegisterComponentResult).Then(InitializeResults).Simulate(world);
        }
    }
}
