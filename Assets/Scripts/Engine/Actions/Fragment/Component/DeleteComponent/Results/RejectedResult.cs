using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.DeleteComponent
{
    public record RejectedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt> Rejections,
        [NotNull] ComponentID RejectedID
    ) : Result;
}
