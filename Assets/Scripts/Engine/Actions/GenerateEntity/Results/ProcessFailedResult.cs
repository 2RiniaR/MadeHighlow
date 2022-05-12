using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record ProcessFailedResult([NotNull] Entity Entity, [NotNull] FailedProcess Process) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
