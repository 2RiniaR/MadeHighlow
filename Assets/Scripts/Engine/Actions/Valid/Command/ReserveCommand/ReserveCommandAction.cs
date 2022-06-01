using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record ReserveCommandAction([NotNull] Command Command) : IValidAction;
}
