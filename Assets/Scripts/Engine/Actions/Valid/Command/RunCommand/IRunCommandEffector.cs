using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.RunCommand
{
    public interface IRunCommandEffector
    {
        public ValueList<Interrupt<RunCommandEffect>> EffectsOnRunCommand(
            [NotNull] IHistory history,
            [NotNull] Player player,
            [NotNull] Unit unit,
            [NotNull] Card card,
            [NotNull] Command command
        );
    }
}
