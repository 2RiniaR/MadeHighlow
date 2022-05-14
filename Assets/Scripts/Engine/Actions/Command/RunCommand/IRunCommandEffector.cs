using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public interface IRunCommandEffector
    {
        public ValueList<Interrupt<RunCommandEffect>> EffectsOnRunCommand(
            [NotNull] IHistory context,
            [NotNull] Player player,
            [NotNull] Unit unit,
            [NotNull] Card card,
            [NotNull] Command command
        );
    }
}
