using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.JoinPlayer
{
    public record Process([NotNull] Event<CreatePlayer.SucceedResult> CreatePlayerEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreatePlayerEvent);
    }
}
