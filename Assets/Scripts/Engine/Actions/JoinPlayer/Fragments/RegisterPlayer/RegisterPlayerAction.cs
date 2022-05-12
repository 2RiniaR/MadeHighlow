using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterPlayerAction([NotNull] Player InitialPlayer) : Action<Results.SucceedResult>
    {
        public override Results.SucceedResult Validate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Validate(context);
            var formattedPlayer = InitialPlayer with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
            return new Results.SucceedResult(allocateIDResult, formattedPlayer);
        }
    }
}
