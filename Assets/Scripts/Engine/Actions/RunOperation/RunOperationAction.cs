using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクション
    /// </summary>
    public record RunOperationAction : Action<RunOperationResult>
    {
        /// <summary>
        ///     実行する命令
        /// </summary>
        [NotNull]
        public UnitOperation Operation { get; init; } = UnitOperation.Empty;

        public override RunOperationResult Validate(in IActionContext context)
        {
            var currentContext = context;

            var effectors = Component.GetAllOfTypeFrom<IRunOperationEffector>(currentContext.World);
            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnRunOperation(currentContext, this);

                // 「気絶状態のユニットが命令を実行できない」とかを再現できるよ、やったね！
                if (effect.Canceled)
                {
                    return new CanceledRunOperationResult();
                }
            }

            var payCardResult = PayCard(currentContext);
            currentContext = currentContext.Appended(payCardResult);

            var commandResult = ActuateCommand(currentContext);

            return new SucceedRunOperationResult
            {
                PayCard = payCardResult,
                Command = commandResult,
            };
        }

        [NotNull]
        public PayCardResult PayCard([NotNull] in IActionContext context)
        {
            var payCardAction = new PayCardAction { PaidCardID = Operation.CardID };
            return payCardAction.Validate(context);
        }

        [NotNull]
        public Result ActuateCommand([NotNull] in IActionContext context)
        {
            var commandAction = Operation.ActionIn(context);
            return commandAction.ValidateAbstract(context);
        }
    }
}