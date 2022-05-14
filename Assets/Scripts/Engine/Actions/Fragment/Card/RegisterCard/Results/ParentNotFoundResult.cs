using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterCard
{
    public record ParentNotFoundResult([NotNull] PlayerID ParentID) : RegisterCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
