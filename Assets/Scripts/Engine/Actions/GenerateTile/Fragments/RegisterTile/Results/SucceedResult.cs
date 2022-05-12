using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile
{
    /// <summary>
    ///     エンティティを新規登録した結果
    /// </summary>
    public record SucceedResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Tile RegisteredTile
    ) : RegisterTileResult
    {
        public override World Simulate(World world)
        {
            var currentWorld = world;
            currentWorld = AllocateIDResult.Simulate(currentWorld);
            currentWorld = RegisteredTile.CreateIn(currentWorld);
            return currentWorld;
        }
    }
}
