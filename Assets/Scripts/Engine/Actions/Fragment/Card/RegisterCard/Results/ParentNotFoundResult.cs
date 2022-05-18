using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterCard
{
    public record ParentNotFoundResult([NotNull] RegisterCardAction Action) : RegisterCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
