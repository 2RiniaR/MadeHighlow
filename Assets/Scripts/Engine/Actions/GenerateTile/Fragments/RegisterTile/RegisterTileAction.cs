using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile.RegisterTile
{
    public record RegisterTileAction([NotNull] Tile InitialProps) : Action<RegisterTileResult>
    {
        public override RegisterTileResult Evaluate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Evaluate(context);
            var formattedTile = InitialProps with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };

            return new SucceedResult(allocateIDResult, formattedTile);
        }
    }
}
