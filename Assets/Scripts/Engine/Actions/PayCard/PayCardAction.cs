using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを対価として支払うアクション
    /// </summary>
    public record PayCardAction : IValidatable
    {
        /// <summary>
        ///     対価として支払うカードのID
        /// </summary>
        public CardEnsuredID PaidCardID { get; init; } = new();

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public PayCardResult Validate([NotNull] in IActionContext context)
        {
            // 失敗するパターンは今のところない
            return new PayCardResult { PaidCardID = PaidCardID };
        }
    }
}