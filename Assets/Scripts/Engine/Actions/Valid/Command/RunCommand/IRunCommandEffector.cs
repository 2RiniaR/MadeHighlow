using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public interface IRunCommandEffector : IPriority<IRunCommandEffector>
    {
        [NotNull]
        [ItemNotNull]
        public ValueList<Interrupt<RunCommandEffect>> EffectsOnRunCommand(
            [NotNull] IHistory history,
            [NotNull] RunCommandAction action,
            [NotNull] RunCommandProcess process
        );
    }
}
