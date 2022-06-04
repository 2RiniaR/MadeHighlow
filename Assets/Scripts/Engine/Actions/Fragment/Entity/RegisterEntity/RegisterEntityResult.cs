using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RegisterEntity
{
    public record RegisterEntityResult([NotNull] RegisterEntityAction Action, [NotNull] Entity Registered) : IResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new RegisterEntitySimulator(context, world, this).Simulate();
        }
    }
}
