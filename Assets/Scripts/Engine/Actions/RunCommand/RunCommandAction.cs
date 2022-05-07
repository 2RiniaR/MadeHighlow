using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクション
    /// </summary>
    public record RunCommandAction([NotNull] Command Command) : Action<RunCommandResult>
    {
        public override RunCommandResult Validate(IActionContext context)
        {
            var currentContext = context;
            var actor = Command.UnitID.GetFrom(currentContext.World);

            // いないものは行動できない。
            if (actor == null)
            {
                return new FailedRunCommandResult(FailedRunCommandReason.NoActor);
            }

            // 死者は行動できないよ。
            if (actor.Vitality != null && actor.Vitality.IsDead)
            {
                return new FailedRunCommandResult(FailedRunCommandReason.Dead);
            }

            var effectors = Component.GetAllOfTypeFrom<IRunCommandEffector>(currentContext.World);
            foreach (var effector in effectors)
            {
                var effect = effector.EffectOnRunCommand(currentContext, this);

                // 「気絶状態のユニットが命令を実行できない」とかを再現できるよ、やったね！
                if (effect.Canceled)
                {
                    return new CanceledRunCommandResult(Command);
                }
            }

            var commandActionResult = ActuateCommand(currentContext);
            currentContext = currentContext.Appended(commandActionResult);

            // 命令の実行後にカードを削除するよ。後払い。
            var payCardResult = PayCard(currentContext);

            return new SucceedRunCommandResult(payCardResult, commandActionResult);
        }

        [NotNull]
        public PayCardResult PayCard([NotNull] IActionContext context)
        {
            var payCardAction = new PayCardAction(Command.CardID);
            return payCardAction.Validate(context);
        }

        [NotNull]
        public Result ActuateCommand([NotNull] IActionContext context)
        {
            var commandAction = Command.ActionIn(context.World);
            return commandAction.ValidateAbstract(context);
        }
    }
}
