using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public interface IKnockBackRejector : IPriority<IKnockBackRejector>
    {
        [CanBeNull]
        public Interrupt<KnockBackRejection> KnockBackRejection(
            [NotNull] IHistory history,
            [NotNull] KnockBackAction action,
            [NotNull] KnockBackProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<KnockBackRejection>> collected
        );
    }
}
