using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.DeleteEntity;

namespace RineaR.MadeHighlow.Actions.Valid.DestroyEntity
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
