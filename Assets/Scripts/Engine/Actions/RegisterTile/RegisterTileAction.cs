using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイルを新規登録するアクション
    /// </summary>
    public record RegisterTileAction(
        [NotNull] Position2D Position2D,
        [NotNull] Direction2D Direction2D,
        [NotNull] Elevation Elevation
    ) : Action<RegisterTileResult>
    {
        public override RegisterTileResult Validate(IActionContext context)
        {
            return new RegisterTileResult(
                new Tile(
                    new AllocateIDAction().Validate(context).AllocatedID,
                    Position2D,
                    Direction2D,
                    Elevation,
                    ValueObjectList<Component>.Empty
                )
            );
        }
    }
}
