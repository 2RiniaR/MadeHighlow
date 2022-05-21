using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.GenerateEntity
{
    public interface IGenerateEntityRejector : IPriority<IGenerateEntityRejector>
    {
        [CanBeNull]
        public Interrupt<GenerateEntityRejection> GenerateEntityRejection(
            [NotNull] IHistory history,
            [NotNull] GenerateEntityAction action,
            [NotNull] GenerateEntityProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<GenerateEntityRejection>> collected
        );
    }
}
