using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ユニットに命令する
    /// </summary>
    public record ReserveCommandAction([NotNull] Command Command) : Action<ReserveCommandResult>
    {
        public override ReserveCommandResult Validate(IActionContext context)
        {
            var preValidationResult = PreValidationResult(context);
            if (preValidationResult != null)
            {
                return preValidationResult;
            }

            var interrupts = CollectInterrupts(context).Sort();
            foreach (var interrupt in interrupts)
            {
                if (interrupt.Effect is DisallowCommandEffect)
                {
                    return new DisallowedReserveCommandResult(Command, interrupt.ComponentID);
                }

                if (interrupt.Effect is AllowCommandEffect)
                {
                    return new AllowedReserveCommandResult(Command, interrupt.ComponentID);
                }
            }

            return new DisallowedReserveCommandResult(Command, null);
        }

        [CanBeNull]
        private ReserveCommandResult PreValidationResult([NotNull] IActionContext context)
        {
            var card = Command.CardID.GetFrom(context.World);
            var unit = Command.UnitID.GetFrom(context.World);

            if (card == null)
            {
                return new FailedReserveCommandResult(Command, FailedReserveCommandReason.CardNotFound);
            }

            if (unit == null)
            {
                return new FailedReserveCommandResult(Command, FailedReserveCommandReason.UnitNotFound);
            }

            var player = card.OwnerPlayerID.GetFrom(context.World);

            if (player == null)
            {
                return new FailedReserveCommandResult(Command, FailedReserveCommandReason.OwnerNotFound);
            }

            if (unit.FollowingPlayerID != player.PlayerID)
            {
                return new FailedReserveCommandResult(Command, FailedReserveCommandReason.NotOwner);
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
