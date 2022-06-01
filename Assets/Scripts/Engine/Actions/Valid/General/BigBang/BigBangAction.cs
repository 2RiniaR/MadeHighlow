using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.General.BigBang
{
    public record BigBangAction([NotNull] World InitialWorld) : IValidAction;
}
