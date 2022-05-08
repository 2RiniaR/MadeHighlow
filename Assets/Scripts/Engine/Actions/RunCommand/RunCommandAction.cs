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
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var currentContext = context;

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is CancelRunCommandEffect)
                {
                    return new CanceledRunCommandResult(Command, interrupt.ComponentID);
                }
            }

            var commandActionResult = ActuateCommand(currentContext);
            currentContext = currentContext.Appended(commandActionResult);
            var payCardResult = PayCard(currentContext);

            return new SucceedRunCommandResult(payCardResult, commandActionResult);
        }

        [CanBeNull]
        private RunCommandResult PreValidationResult([NotNull] IActionContext context)
        {
            var actor = Command.UnitID.GetFrom(context.World);

            // いないものは行動できない。
            if (actor == null)
            {
                return new FailedRunCommandResult(Command, FailedRunCommandReason.ActorNotFound);
            }

            // 死者は行動できないよ。
            if (actor.Vitality != null && actor.Vitality.IsDead)
            {
                return new FailedRunCommandResult(Command, FailedRunCommandReason.ActorIsDead);
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
