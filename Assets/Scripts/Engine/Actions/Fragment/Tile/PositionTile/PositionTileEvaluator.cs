using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public class PositionTileEvaluator
    {
        public PositionTileEvaluator([NotNull] IHistory initial, PositionTileAction action)
        {
            Initial = initial;
            Action = action;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private PositionTileAction Action { get; }

        [CanBeNull] private Tile Target { get; set; }
        [CanBeNull] private Tile Positioned { get; set; }

        [NotNull]
        public PositionTileResult Evaluate()
        {
            PositionTileResult result;

            result = FindTarget();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private PositionTileResult FindTarget()
        {
            Contract.Ensures((Contract.Result<PositionTileResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.TileNotExist);
            }

            return null;
        }

        [CanBeNull]
        private PositionTileResult Position()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<PositionTileResult>() != null) ^ (Positioned != null));

            if (!IsPositionable(Initial, Action.Destination))
            {
                return new FailedResult(Action, FailedReason.ResolveFailed);
            }

            Positioned = Target;
            return null;
        }

        private static bool IsPositionable([NotNull] IHistory history, [NotNull] Position2D dest)
        {
            return dest.GetTile(history.World) == null;
        }

        [NotNull]
        private PositionTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Positioned != null);

            return new SucceedResult(Action, Positioned);
        }
    }
}
