using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateTile
{
    public record Process(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterTileResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterCardEvent).Then(CreateComponentEvents);
    }
}
