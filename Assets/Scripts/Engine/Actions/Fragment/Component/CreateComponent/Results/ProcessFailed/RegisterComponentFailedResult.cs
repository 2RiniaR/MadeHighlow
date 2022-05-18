using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterComponent;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateComponent
{
    public record RegisterComponentFailedResult(
        [NotNull] CreateComponentAction Action,
        [NotNull] RegisterComponentResult Failed
    ) : CreateComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
