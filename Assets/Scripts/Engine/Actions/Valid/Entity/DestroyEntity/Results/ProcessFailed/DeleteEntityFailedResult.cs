using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.DeleteEntity;

namespace RineaR.MadeHighlow.Actions.DestroyEntity
{
    public record DeleteEntityFailedResult(
        [NotNull] DestroyEntityAction Action,
        [NotNull] DeleteEntityResult Failed
    ) : DestroyEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
