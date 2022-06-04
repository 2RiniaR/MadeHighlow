using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public record Process(
        [NotNull] Event<AllocateID.Result> AllocateIDEvent,
        [NotNull] Event<RegisterTile.Result> RegisterTileEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterTileEvent).Then(CreateComponentEvents);
    }
}
