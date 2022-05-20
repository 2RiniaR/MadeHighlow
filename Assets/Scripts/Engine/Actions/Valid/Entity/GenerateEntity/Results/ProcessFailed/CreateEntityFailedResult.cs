using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.CreateEntity;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public record CreateEntityFailedResult(
        [NotNull] GenerateEntityAction Action,
        [NotNull] CreateEntityResult Failed
    ) : GenerateEntityResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
