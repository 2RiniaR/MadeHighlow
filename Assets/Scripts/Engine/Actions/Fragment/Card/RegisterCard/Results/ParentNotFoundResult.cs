using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterCard
{
    public record ParentNotFoundResult([NotNull] RegisterCardAction Action) : RegisterCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
