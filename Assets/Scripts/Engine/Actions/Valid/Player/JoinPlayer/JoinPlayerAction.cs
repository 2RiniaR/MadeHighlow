using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record JoinPlayerAction([NotNull] Player InitialPlayer) : IValidAction;
}
