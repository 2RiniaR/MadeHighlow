using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record IncrementTurnAction : Action<IncrementTurnResult>
    {
        [NotNull]
        public override IncrementTurnResult Validate([NotNull] in IActionContext context)
        {
            return new IncrementTurnResult();
        }
    }
}