using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record RejectedResult(
        [NotNull] Entity InitialEntity,
        [NotNull] ComponentID RejectedComponentID,
        [NotNull] SucceedProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityEffect>> Interrupts
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
