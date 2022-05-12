using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.PayCard;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    /// <summary>
    ///     命令を実行するアクション
    /// </summary>
    public record RunCommandAction([NotNull] Command Command) : Action<RunCommandResult>
    {
        public override RunCommandResult Evaluate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var currentContext = context;

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is CancelEffect)
                {
                    return new CanceledResult(Command, interrupt.ComponentID);
                }
            }

            var commandActionResult = ActuateCommand(currentContext);
            currentContext = currentContext.Appended(commandActionResult);
            var payCardResult = PayCard(currentContext);

            return new SucceedResult(payCardResult, commandActionResult);
        }

        [CanBeNull]
        private RunCommandResult PreValidationResult([NotNull] IActionContext context)
        {
            var actor = Command.UnitID.GetFrom(context.World);

            // いないものは行動できない。
            if (actor == null)
            {
                return new FailedResult(Command, FailedReason.ActorNotFound);
            }

            // 死者は行動できないよ。
            if (actor.Vitality != null && actor.Vitality.IsDead)
            {
                return new FailedResult(Command, FailedReason.ActorIsDead);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<RunCommandEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IRunCommandEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnRunCommand(context, this));
        }

        [NotNull]
        public PayCardResult PayCard([NotNull] IActionContext context)
        {
            var payCardAction = new PayCardAction(Command.CardID);
            return payCardAction.Evaluate(context);
        }

        [NotNull]
        public Result ActuateCommand([NotNull] IActionContext context)
        {
            var commandAction = Command.ActionIn(context.World);
            return commandAction.EvaluateAbstract(context);
        }
    }
}
