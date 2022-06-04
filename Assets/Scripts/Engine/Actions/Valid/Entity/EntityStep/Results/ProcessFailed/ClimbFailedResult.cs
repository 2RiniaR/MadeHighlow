using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public record ClimbFailedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Event<MoveEntity.SucceedResult>> SucceedResults,
        [NotNull] MoveEntity.Result Failed
    ) : Result;
}
