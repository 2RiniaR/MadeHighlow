using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.Fragment.CreateEntity
{
    public record Process(
        [NotNull] Event<AllocateIDResult> AllocateIDEvent,
        [NotNull] Event<RegisterEntityResult> RegisterCardEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents
    )
    {
        public Timeline Timeline { get; }
            = new Timeline().Then(AllocateIDEvent).Then(RegisterCardEvent).Then(CreateComponentEvents);
    }
}
