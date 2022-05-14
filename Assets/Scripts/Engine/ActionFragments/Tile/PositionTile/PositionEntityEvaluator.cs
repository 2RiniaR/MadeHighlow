using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow.ActionFragments.PositionTile
{
    public class PositionTileEvaluator
    {
        public PositionTileEvaluator(
            [NotNull] IHistory context,
            [NotNull] TileID targetID,
            [NotNull] Position2D destination
        )
        {
            Context = context;
            TargetID = targetID;
            Destination = destination;
        }

        [NotNull] private IHistory Context { get; }
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

            Target = TargetID.GetFrom(Context.World);
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

            if (!IsPositionable(Context, Target, Destination))
            {
                return new FailedResult(TargetID, FailedReason.ResolveFailed);
            }

            Positioned = Target;
            return null;
        }

        private static bool IsPositionable(
            [NotNull] IHistory context,
            [NotNull] Tile tile,
            [NotNull] Position2D dest
        )
        {
            return dest.GetTile(context.World) == null;
        }

        [NotNull]
        private PositionTileResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Positioned != null);

            return new SucceedResult(Positioned);
        }
    }
}
