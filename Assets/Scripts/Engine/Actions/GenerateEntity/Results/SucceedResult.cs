using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record SucceedResult(
        [NotNull] Entity InitialEntity,
        [NotNull] Entity GeneratedEntity,
        [NotNull] SucceedProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityEffect>> Interrupts
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = Process.RegisterEntity.Simulate(currentWorld);
            currentWorld = Process.AddComponents.Aggregate(currentWorld, (curr, result) => result.Simulate(curr));
            currentWorld = Process.PositionEntity.Simulate(currentWorld);
            return currentWorld;
        }
    }
}
