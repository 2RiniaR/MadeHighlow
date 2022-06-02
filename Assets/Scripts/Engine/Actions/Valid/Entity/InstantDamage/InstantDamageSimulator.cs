using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public class InstantDamageSimulator
    {
        public InstantDamageSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] InstantDamageResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private InstantDamageResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                var target = Context.Finder.FindEntity(Initial, succeedResult.Action.TargetID) ??
                             throw new ArgumentException();
                var vitality = target.Vitality ?? throw new ArgumentException();

                var modifiedTarget = target with
                {
                    Vitality = vitality with { Health = succeedResult.Calculated.Caused(vitality.Health) },
                };
                return Context.Modifier.UpdateEntity(Initial, modifiedTarget);
            }

            return Initial;
        }
    }
}
