using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイルを新規登録した結果
    /// </summary>
    public record RegisterTileResult(
        [NotNull] AllocateIDResult AllocateIDResult,
        [NotNull] Tile RegisteredTile
    ) : Result
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
