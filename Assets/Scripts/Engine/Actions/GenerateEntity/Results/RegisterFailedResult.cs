using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateEntity.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.GenerateEntity
{
    public record RegisterFailedResult(
        [NotNull] Entity InitialStatus,
        [NotNull] RegisterEntityResult FailedResult
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
