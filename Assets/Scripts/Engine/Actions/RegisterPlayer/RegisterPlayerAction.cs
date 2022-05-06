namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     プレイヤーを新規登録するアクション
    /// </summary>
    public record RegisterPlayerAction : Action<RegisterPlayerResult>
    {
        public PlayerDeckSize DeckSize { get; init; } = new();

        public override RegisterPlayerResult Validate(in IActionContext context)
        {
            return new RegisterPlayerResult
            {
                Registered = new Player
                {
                    ID = new AllocateIDAction().Validate(context).Allocated,
                    Cards = ValueObjectList<Card>.Empty,
                    Components = ValueObjectList<Component>.Empty,
                    DeckSize = DeckSize,
                },
            };
        }
    }
}