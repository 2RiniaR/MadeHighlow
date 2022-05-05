using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record IncrementTurnAction : IValidatable
    {
        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public IncrementTurnResult Validate([NotNull] in IActionContext context)
        {
            return new IncrementTurnResult();
        }
    }
}