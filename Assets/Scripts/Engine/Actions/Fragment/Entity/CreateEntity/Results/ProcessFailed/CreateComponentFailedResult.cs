using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.CreateComponent;
using RineaR.MadeHighlow.Actions.RegisterEntity;

namespace RineaR.MadeHighlow.Actions.CreateEntity
{
    public record CreateComponentFailedResult(
        [NotNull] CreateEntityAction Action,
        [NotNull] Event<RegisterEntityResult> RegisterEntityEvent,
        [NotNull] [ItemNotNull] ValueList<Event<CreateComponent.SucceedResult>> CreateComponentEvents,
        [NotNull] CreateComponentResult Failed
    ) : CreateEntityResult;
}
