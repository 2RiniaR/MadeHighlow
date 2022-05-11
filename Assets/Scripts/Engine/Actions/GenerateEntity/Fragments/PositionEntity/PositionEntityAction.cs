using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record PositionEntityAction([NotNull] Entity InitialEntity) : Action<PositionEntityResult>
    {
        public override PositionEntityResult Validate(IActionContext context)
        {
            if (IsPositionable(context))
            {
                return new SucceedPositionEntityResult(InitialEntity, InitialEntity);
            }

            return new FailedPositionEntityResult(InitialEntity, FailedPositionEntityReason.Invalid);
        }

        private bool IsPositionable([NotNull] IActionContext context)
        {
            /*
             * 【エンティティが生成可能な条件】
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

            var landingTile = InitialEntity.Position3D.To2D().GetTile(context.World);
            var entityHeight = InitialEntity.Position3D.Z;

            if (landingTile == null || // (1)
                landingTile.Elevation is AbyssElevation) // (2)
            {
                return InitialEntity.Levitation;
            }

            if (landingTile.Elevation is GroundElevation ground) // (3) ~ (7)
            {
                if (ground.Height < entityHeight) // (3), (4)
                {
                    return ground.Placeable || InitialEntity.Levitation;
                }

                if (ground.Height == entityHeight) // (5), (6)
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
    }
}
