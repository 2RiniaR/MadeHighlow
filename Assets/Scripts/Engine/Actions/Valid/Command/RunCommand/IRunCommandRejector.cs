using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public interface IRunCommandRejector : IPriority<IRunCommandRejector>
    {
        [NotNull]
        public Interrupt<RunCommandRejection> RunCommandRejection(
            [NotNull] IHistory history,
            [NotNull] RunCommandAction action,
            [NotNull] RunCommandProcess process,
            [NotNull] [ItemNotNull] ValueList<Interrupt<RunCommandRejection>> collected
        );
    }
}
