using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record BigBangResult([NotNull] BigBangAction Action, [NotNull] BigBangProcess Process) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new BigBangSimulator(context, world, this).Simulate();
        }
    }
}
