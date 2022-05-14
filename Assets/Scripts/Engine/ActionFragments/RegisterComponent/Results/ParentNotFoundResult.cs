using JetBrains.Annotations;

namespace RineaR.MadeHighlow.ActionFragments.RegisterComponent
{
    public record ParentNotFoundResult([NotNull] IAttachableID ParentID) : RegisterComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
