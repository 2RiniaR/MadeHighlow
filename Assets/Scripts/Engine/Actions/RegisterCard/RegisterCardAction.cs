using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     カードを新規登録するアクション
    /// </summary>
    public record RegisterCardAction([NotNull] Card InitialCard) : Action<RegisterCardResult>
    {
        public override RegisterCardResult Validate(IActionContext context)
        {
            var allocateIDResult = new AllocateIDAction().Validate(context);
            var formattedCard = InitialCard with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };
            return new RegisterCardResult(allocateIDResult, formattedCard);
        }
    }
}
