using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.RegisterEntity
{
    public record RegisterEntityResult([NotNull] RegisterEntityAction Action, [NotNull] Entity Registered) : Result
    {
        public override World Simulate(World world)
        {
            return Registered.CreateIn(world);
        }
    }
}
