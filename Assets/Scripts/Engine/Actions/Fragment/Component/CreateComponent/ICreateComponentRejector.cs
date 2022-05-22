using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateComponent
{
    public interface ICreateComponentRejector : IPriority<ICreateComponentRejector>
    {
        [CanBeNull]
        public Interrupt<CreateComponentRejection> CreateComponentRejection(
            [NotNull] IHistory history,
            [NotNull] CreateComponentAction action,
            [NotNull] CreateComponentProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<CreateComponentRejection>> collected
        );
    }
}
