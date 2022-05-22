using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public class PositionEntityEvaluator
    {
        public PositionEntityEvaluator([NotNull] IHistory initial, PositionEntityAction action)
        {
            Initial = initial;
            Action = action;
        }

        [NotNull] private IHistory Initial { get; }
        [NotNull] private PositionEntityAction Action { get; }

        [CanBeNull] private Entity Target { get; set; }
        [CanBeNull] private Entity Positioned { get; set; }

        [NotNull]
        public PositionEntityResult Evaluate()
        {
            PositionEntityResult result;

            result = FindTarget();
            if (result != null) return result;

            result = Position();
            if (result != null) return result;

            return Succeed();
        }

        [CanBeNull]
        private PositionEntityResult FindTarget()
        {
            Contract.Ensures((Contract.Result<PositionEntityResult>() != null) ^ (Target != null));

            Target = Action.TargetID.GetFrom(Initial.World);
            if (Target == null)
            {
                return new FailedResult(Action, FailedReason.TargetNotExist);
            }

            return null;
        }

        [CanBeNull]
        private PositionEntityResult Position()
        {
            Contract.Requires<InvalidOperationException>(Target != null);
            Contract.Ensures((Contract.Result<PositionEntityResult>() != null) ^ (Positioned != null));

            if (!IsPositionable(Initial, Target, Action.Destination))
            {
                return new FailedResult(Action, FailedReason.ResolveFailed);
            }

            Positioned = Target;
            return null;
        }

        private static bool IsPositionable(
            [NotNull] IHistory history,
            [NotNull] Entity entity,
            [NotNull] Position3D dest
        )
        {
            /*
             * 【エンティティが設置可能な条件】
             * 
             * (T) エンティティが浮遊している場合
             * (F) エンティティが浮遊していない場合
             * 
             * (1) 同一座標にタイルが存在しない
             * (2) 同一座標のタイルが `Abyss`
             * (3) 同一座標のタイルが `Ground` でかつ、高さが Tile < Entity かつ、エンティティ設置可能
             * (4) 同一座標のタイルが `Ground` でかつ、高さが Tile < Entity かつ、エンティティ設置不可
             * (5) 同一座標のタイルが `Ground` でかつ、高さが Tile = Entity かつ、エンティティ設置可能
             * (6) 同一座標のタイルが `Ground` でかつ、高さが Tile = Entity かつ、エンティティ設置不可
             * (7) 同一座標のタイルが `Ground` でかつ、高さが Tile > Entity
             * (8) 同一座標のタイルが `Tower`
             *
             *     |-------|-------|-------|-------|-------|-------|-------|-------|-------|
             *     | O / X |  (1)  |  (2)  |  (3)  |  (4)  |  (5)  |  (6)  |  (7)  |  (8)  |
             *     |-------|-------|-------|-------|-------|-------|-------|-------|-------|
             *     |  (T)  |   O   |   O   |   O   |   O   |   O   |   X   |   X   |   X   |
             *     |-------|-------|-------|-------|-------|-------|-------|-------|-------|
             *     |  (F)  |   X   |   X   |   O   |   X   |   O   |   X   |   X   |   X   |
             *     |-------|-------|-------|-------|-------|-------|-------|-------|-------|
             */

            var landingTile = dest.To2D().GetTile(history.World);
            var destHeight = dest.Z;

            if (landingTile == null || // (1)
                landingTile.Elevation is AbyssElevation) // (2)
            {
                return entity.Levitation;
            }

            if (landingTile.Elevation is GroundElevation ground) // (3) ~ (7)
            {
                if (ground.Height < destHeight) // (3), (4)
                {
                    return ground.Placeable || entity.Levitation;
                }

                if (ground.Height == destHeight) // (5), (6)
                {
                    return ground.Placeable;
                }

                return false; //(7)
            }

            if (landingTile.Elevation is TowerElevation) // (8)
            {
                return false;
            }

            throw new InvalidOperationException();
        }

        [NotNull]
        private PositionEntityResult Succeed()
        {
            Contract.Requires<InvalidOperationException>(Positioned != null);

            return new SucceedResult(Action, Positioned);
        }
    }
}
