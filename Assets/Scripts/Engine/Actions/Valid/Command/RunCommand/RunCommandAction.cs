using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record RunCommandAction([NotNull] Command Command) : IValidAction;
}
