using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.KnockBack
{
    public record KnockBackAction(
        ID SourceID,
        [NotNull] EntityID TargetID,
        [NotNull] KnockBack KnockBack
    ) : IValidAction;
}
