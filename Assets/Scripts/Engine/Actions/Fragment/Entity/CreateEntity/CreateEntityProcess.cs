using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AllocateID;
using RineaR.MadeHighlow.Actions.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record CreateEntityProcess(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterEntityResult> RegisterEntityEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterEntityEvent).Then(CreateComponentEvents);
    }
}
