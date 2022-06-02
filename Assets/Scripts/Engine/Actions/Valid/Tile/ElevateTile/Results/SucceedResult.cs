using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public sealed record SucceedResult(
        [NotNull] ElevateTileAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<ElevateTileRejection>> RejectionInterrupts
    ) : ElevateTileResult;
}
