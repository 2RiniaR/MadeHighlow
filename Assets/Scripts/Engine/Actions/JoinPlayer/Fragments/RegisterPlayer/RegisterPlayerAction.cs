using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterPlayerAction([NotNull] Player InitialPlayer) : Action<RegisterPlayerResult>
    {
        public override RegisterPlayerResult Validate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Validate(context);
            var formattedPlayer = InitialPlayer with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
            return new RegisterPlayerResult(allocateIDResult, formattedPlayer);
        }
    }
}
