using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterPlayerAction([NotNull] DeckSize DeckSize) : Action<RegisterPlayerResult>
    {
        public override RegisterPlayerResult Validate(IActionContext context)
        {
            return new RegisterPlayerResult(
                new Player(
                    new AllocateIDAction().Validate(context).AllocatedID,
                    ValueList<Card>.Empty,
                    DeckSize,
                    ValueList<Component>.Empty
                )
            );
        }
    }
}
