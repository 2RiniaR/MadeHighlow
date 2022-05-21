using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.AllocateID;
using RineaR.MadeHighlow.Actions.Fragment.RegisterTile;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateTile
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
