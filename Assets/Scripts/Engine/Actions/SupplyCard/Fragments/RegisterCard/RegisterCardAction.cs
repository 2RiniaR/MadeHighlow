using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard.RegisterCard
{
    /// <summary>
    ///     カードを新規登録するアクション
    /// </summary>
    public record RegisterCardAction([NotNull] Card InitialCard)
    {
        public RegisterCardResult Validate(IActionContext context)
        {
            var player = InitialCard.OwnerPlayerID.GetFrom(context.World);
            if (player == null)
            {
                return new FailedResult(InitialCard, FailedReason.OwnerNotExist);
            }

            var allocateIDResult = new AllocateIDAction().Validate(context);
            var formattedCard = InitialCard with
            {
                ID = allocateIDResult.AllocatedID,
                Components = ValueList<Component>.Empty,
            };

            return new SucceedResult(allocateIDResult, formattedCard);
        }
    }
}
