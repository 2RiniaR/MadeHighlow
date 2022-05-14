using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PositionTile
{
    public class PositionTileEvaluator
    {
        public PositionTileEvaluator(
            [NotNull] IHistory history,
            [NotNull] TileID targetID,
            [NotNull] Position2D destination
        )
        {
            History = history;
            TargetID = targetID;
            Destination = destination;
        }

        [NotNull] private IHistory History { get; }
        [NotNull] private TileID TargetID { get; }
        [NotNull] public Position2D Destination { get; }

        [CanBeNull] private Tile Target { get; set; }
        [CanBeNull] private Tile Positioned { get; set; }

        [NotNull]
        public PositionTileResult Evaluate()
        {
            PositionTileResult result;

            result = GetTarget();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private PositionTileResult GetTarget()
        {
            Contract.Ensures((Contract.Result<PositionTileResult>() != null) ^ (Target != null));

            Target = TargetID.GetFrom(History.World);
            if (Target == null)
            {
                return new FailedResult(TargetID, FailedReason.TileNotExist);
            }

            return null;
        }

        [CanBeNull]
        private PositionTileResult Position()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<PositionTileResult>() != null) ^ (Positioned != null));

            if (!IsPositionable(History, Target, Destination))
            {
                return new FailedResult(TargetID, FailedReason.ResolveFailed);
            }

            Positioned = Target;
            return null;
        }

        private static bool IsPositionable(
            [NotNull] IHistory history,
            [NotNull] Tile tile,
            [NotNull] Position2D dest
        )
        {
            return dest.GetTile(history.World) == null;
        }

        [NotNull]
        private PositionTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Positioned != null);

            return new SucceedResult(Positioned);
        }
    }
}
