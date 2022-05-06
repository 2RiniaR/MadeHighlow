using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterTileAction : Action<RegisterTileResult>
    {
        /// <summary>
        ///     位置
        /// </summary>
        [NotNull]
        public Position2D Position2D { get; init; } = Position2D.Zero;

        /// <summary>
        ///     方向
        /// </summary>
        [NotNull]
        public Direction2D Direction2D { get; init; } = Direction2D.XPositive;

        /// <summary>
        ///     高さ
        /// </summary>
        [NotNull]
        public TileHeight Height { get; init; } = new GroundTileHeight();

        public override RegisterTileResult Validate(in IActionContext context)
        {
            return new RegisterTileResult
            {
                Registered = new Tile
                {
                    ID = new AllocateIDAction().Validate(context).Allocated,
                    Components = ValueObjectList<Component>.Empty,
                    Position2D = Position2D,
                    Direction2D = Direction2D,
                    Height = Height,
                },
            };
        }
    }
}