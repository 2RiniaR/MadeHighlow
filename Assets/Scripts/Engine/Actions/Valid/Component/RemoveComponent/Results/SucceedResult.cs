using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RemoveComponent
{
    public record SucceedResult(
        [NotNull] Component Target,
        [NotNull] ValueList<ReactedResult> FinalizeComponentResults,
        [NotNull] [ItemNotNull] ValueList<Interrupt<RemoveComponentEffect>> Interrupts
    ) : RemoveComponentResult
    {
        public override World Simulate(World world)
        {
            return Target.DeleteFrom(world);
        }
    }
}
