using System;
using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     命令を実行するアクション
    /// </summary>
    /// <param name="Command">実行する命令</param>
    public record RunCommandAction(Command Command) : Action<RunCommandResult>
    {
        public override RunCommandResult Validate(in IActionContext context)
        {
            var currentContext = context;
            var actor = Command.UnitID.GetFrom(currentContext.World) ?? throw new NullReferenceException();

            // 死者は行動できないよ。
            if (actor.Vitality != null && actor.Vitality.IsDead)
            {
                return new FailedRunCommandResult { Reason = FailedRunCommandReason.Dead };
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

            // 命令の実行前にカードを削除するよ。前払い。
            var payCardResult = PayCard(currentContext);
            currentContext = currentContext.Appended(payCardResult);

            var commandResult = ActuateCommand(currentContext);

            return new SucceedRunCommandResult
            {
                PayCard = payCardResult,
                Command = commandResult,
            };
        }

        [NotNull]
        public PayCardResult PayCard([NotNull] in IActionContext context)
        {
            var payCardAction = new PayCardAction { PaidCardID = Command.CardID };
            return payCardAction.Validate(context);
        }

        [NotNull]
        public Result ActuateCommand([NotNull] in IActionContext context)
        {
            var commandAction = Command.ActionIn(context.World);
            return commandAction.ValidateAbstract(context);
        }
    }
}