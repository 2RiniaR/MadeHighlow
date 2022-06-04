using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record CreatePlayerFailedResult([NotNull] Action Action, [NotNull] CreatePlayer.Result Failed) : Result;
}
