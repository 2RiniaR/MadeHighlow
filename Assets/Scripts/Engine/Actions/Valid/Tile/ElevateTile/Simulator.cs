using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public class Simulator
    {
        public Simulator([NotNull] ISimulationContext context, [NotNull] World initial, [NotNull] Result result)
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private Result Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                var target = Context.Finder.FindTile(Initial, succeedResult.Action.TargetID) ??
                             throw new AggregateException();
                var modifiedTarget = target with { Elevation = succeedResult.Action.Elevate.Caused(target.Elevation) };
                return Context.Modifier.UpdateTile(Initial, modifiedTarget);
            }

            return Initial;
        }
    }
}
