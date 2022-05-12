using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record ReserveCommandAction([NotNull] Command Command) : Action<ReserveCommandResult>
    {
        public override ReserveCommandResult Evaluate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is DisallowEffect)
                {
                    return new DisallowedResult(Command, interrupt.ComponentID);
                }

                if (interrupt.Effect is AllowEffect)
                {
                    return new AllowedResult(Command, interrupt.ComponentID);
                }
            }

            return new DisallowedResult(Command, null);
        }

        [CanBeNull]
        private ReserveCommandResult PreValidationResult([NotNull] IActionContext context)
        {
            var card = Command.CardID.GetFrom(context.World);
            var unit = Command.UnitID.GetFrom(context.World);

            if (card == null)
            {
                return new FailedResult(Command, FailedReason.CardNotFound);
            }

            if (unit == null)
            {
                return new FailedResult(Command, FailedReason.UnitNotFound);
            }

            var player = card.OwnerPlayerID.GetFrom(context.World);

            if (player == null)
            {
                return new FailedResult(Command, FailedReason.OwnerNotFound);
            }

            if (unit.FollowingPlayerID != player.PlayerID)
            {
                return new FailedResult(Command, FailedReason.NotOwner);
            }

            return null;
        }

        [ItemNotNull]
        [NotNull]
        private ValueList<Interrupt<ReserveCommandEffect>> CollectInterrupts([NotNull] IActionContext context)
        {
            var effectors = Component.GetAllOfTypeFrom<IReserveCommandEffector>(context.World);
            return effectors.SelectMany(effector => effector.EffectsOnReserveCommand(context, this));
        }
    }
}
