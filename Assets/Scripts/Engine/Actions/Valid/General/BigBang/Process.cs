using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.BigBang
{
    public record Process(
        [NotNull] ValueList<Event<ReactedResult<JoinPlayer.Result>>> JoinPlayerEvents,
        [NotNull] ValueList<Event<ReactedResult<GenerateTile.Result>>> GenerateTileEvents,
        [NotNull] ValueList<Event<ReactedResult<GenerateEntity.Result>>> GenerateEntityEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(JoinPlayerEvents).Then(GenerateTileEvents).Then(GenerateEntityEvents);
    }
}
