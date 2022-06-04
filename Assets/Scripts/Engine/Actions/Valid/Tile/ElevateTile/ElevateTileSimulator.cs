using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public class ElevateTileSimulator
    {
        public ElevateTileSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] ElevateTileResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private ElevateTileResult Result { get; }

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
