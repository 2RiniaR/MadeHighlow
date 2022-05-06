using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     タイルを新規登録するアクション
    /// </summary>
    public record RegisterTileAction(
        [NotNull] in Position2D Position2D,
        [NotNull] in Direction2D Direction2D,
        [NotNull] in Elevation Elevation
    ) : Action<RegisterTileResult>
    {
        public override RegisterTileResult Validate(in IActionContext context)
        {
            return new RegisterTileResult(
                new Tile(
                    new AllocateIDAction().Validate(context).AllocatedID,
                    Components: ValueObjectList<Component>.Empty,
                    Position2D: Position2D,
                    Direction2D: Direction2D,
                    Elevation: Elevation
                )
            );
        }
    }
}