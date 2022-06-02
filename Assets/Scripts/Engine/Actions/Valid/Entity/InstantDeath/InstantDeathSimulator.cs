using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public class InstantDeathSimulator
    {
        public InstantDeathSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] InstantDeathResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private InstantDeathResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                var target = Context.Finder.FindEntity(Initial, succeedResult.Action.TargetID) ??
                             throw new ArgumentException();
                var vitality = target.Vitality ?? throw new ArgumentException();

                var modifiedTarget = target with { Vitality = vitality.Dead };
                return Context.Modifier.UpdateEntity(Initial, modifiedTarget);
            }

            return Initial;
        }
    }
}
