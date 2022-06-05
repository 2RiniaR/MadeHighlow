using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public class Evaluator
    {
        public Evaluator([NotNull] IEvaluationContext context, [NotNull] IHistory initial, Action action)
        {
            Initial = initial;
            Context = context;
            Action = action;
            Result = new Result(Action);
        }

        [NotNull] private IEvaluationContext Context { get; }
        [NotNull] private IHistory Initial { get; }
        [NotNull] private Action Action { get; }
        [NotNull] private Result Result { get; set; }

        [NotNull]
        public Result Evaluate()
        {
            var target = FindTarget();

            if (target == null) return Result;
            if (!IsPositionable(target)) return Result;

            Confirm(target);

            return Result;
        }

        [CanBeNull]
        private Entity FindTarget()
        {
            return Context.Finder.FindEntity(Initial.World, Action.TargetID);
        }

        private bool IsPositionable([NotNull] Entity target)
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

            var landingTile = Context.Finder.FindTile(Initial.World, Action.Destination.To2D());
            var destHeight = Action.Destination.Z;

            if (landingTile == null || // (1)
                landingTile.Elevation is AbyssElevation) // (2)
            {
                return target.Levitation;
            }

            if (landingTile.Elevation is GroundElevation ground) // (3) ~ (7)
            {
                if (ground.Height < destHeight) // (3), (4)
                {
                    return ground.Placeable || target.Levitation;
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

        private void Confirm([NotNull] Entity target)
        {
            Result = Result with { Positioned = target with { Position3D = Action.Destination } };
        }
    }
}
