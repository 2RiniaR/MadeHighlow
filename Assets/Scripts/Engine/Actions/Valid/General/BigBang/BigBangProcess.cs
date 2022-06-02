using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.GenerateEntity;
using RineaR.MadeHighlow.Actions.GenerateTile;
using RineaR.MadeHighlow.Actions.JoinPlayer;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record BigBangProcess(
        [NotNull] ValueList<Event<ReactedResult<JoinPlayerResult>>> JoinPlayerEvents,
        [NotNull] ValueList<Event<ReactedResult<GenerateTileResult>>> GenerateTileEvents,
        [NotNull] ValueList<Event<ReactedResult<GenerateEntityResult>>> GenerateEntityEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(JoinPlayerEvents).Then(GenerateTileEvents).Then(GenerateEntityEvents);
    }
}
