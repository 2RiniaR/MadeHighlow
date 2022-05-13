using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.AddComponent.RegisterComponent
{
    public record ParentNotFoundResult([NotNull] IAttachableID ParentID) : RegisterComponentResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
