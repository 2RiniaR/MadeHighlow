using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.DeleteComponent
{
    public interface IDeleteComponentRejector : IPriority<IDeleteComponentRejector>
    {
        [CanBeNull]
        public Interrupt<DeleteComponentRejection> DeleteComponentRejection(
            [NotNull] IHistory history,
            [NotNull] DeleteComponentAction action,
            [NotNull] [ItemNotNull] ValueList<Interrupt<DeleteComponentRejection>> collected
        );
    }
}
