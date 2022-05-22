using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.RegisterTile;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public record CreateTileProcess(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterTileResult> RegisterTileEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterTileEvent).Then(CreateComponentEvents);
    }
}
