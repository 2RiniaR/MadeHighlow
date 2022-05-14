using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public interface IReserveCommandEffector
    {
        public ValueList<Interrupt<ReserveCommandEffect>> EffectsOnReserveCommand(
            [NotNull] IHistory session,
            [NotNull] Player player,
            [NotNull] Unit unit,
            [NotNull] Card card,
            [NotNull] Command command
        );
    }
}
