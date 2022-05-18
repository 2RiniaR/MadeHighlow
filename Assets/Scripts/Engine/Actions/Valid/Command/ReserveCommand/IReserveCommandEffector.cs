using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ReserveCommand
{
    public interface IReserveCommandEffector : IPriority<IReserveCommandEffector>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<ReserveCommandEffect>> EffectsOnReserveCommand(
            [NotNull] IHistory session,
            [NotNull] ReserveCommandAction action
        );
    }
}
