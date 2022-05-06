using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイルを新規登録した結果
    /// </summary>
    public record RegisterTileResult([NotNull] in Tile RegisteredTile) : Result
    {
        public override World Simulate(in World world)
        {
            return RegisteredTile.CreateIn(world);
        }
    }
}