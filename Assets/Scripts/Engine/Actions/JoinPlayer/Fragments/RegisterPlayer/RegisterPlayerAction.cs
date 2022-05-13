using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer.RegisterPlayer
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterPlayerAction([NotNull] Player InitialPlayer) : Action<RegisterPlayerResult>
    {
        public override RegisterPlayerResult Evaluate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Evaluate(context);
            var formattedPlayer = InitialPlayer with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
            return new SucceedResult(allocateIDResult, formattedPlayer);
        }
    }
}
