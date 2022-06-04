using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.EvaluationFlows.CheckRejection;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record Result([NotNull] Action Action) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new Simulator(context, world, this).Simulate();
        }

        public ValueList<Interrupt<Calculation>> Calculations { get; init; }
        public Damage Calculated { get; init; }
        public Rejection Rejection { get; init; }
    }
}
