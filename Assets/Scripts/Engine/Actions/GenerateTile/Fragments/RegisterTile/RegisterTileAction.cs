using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile
{
    /// <summary>
    ///     エンティティを新規登録するアクション
    /// </summary>
    public record RegisterTileAction([NotNull] Tile InitialTile) : Action<RegisterTileResult>
    {
        public override RegisterTileResult Validate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Validate(context);
            var formattedTile = InitialTile with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };

            return new SucceedResult(allocateIDResult, formattedTile);
        }
    }
}
