using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile
{
    /// <summary>
    ///     エンティティを新規登録するアクション
    /// </summary>
    public record RegisterTileAction([NotNull] Tile InitialTile) : Action<RegisterTileResult>
    {
        public override RegisterTileResult Evaluate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Evaluate(context);
            var formattedTile = InitialTile with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };

            return new SucceedResult(allocateIDResult, formattedTile);
        }
    }
}
