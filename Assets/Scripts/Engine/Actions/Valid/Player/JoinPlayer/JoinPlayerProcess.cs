using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.JoinPlayer
{
    public record JoinPlayerProcess([NotNull] Event<Fragment.CreatePlayer.SucceedResult> CreatePlayerEvent)
    {
        public Timeline Timeline { get; } = new Timeline().Then(CreatePlayerEvent);
    }
}
