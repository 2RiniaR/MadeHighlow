using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityAction : IValidatable
    {
        [NotNull] public EntityEnsuredID EntityID { get; init; } = new();

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public DestroyEntityResult Validate([NotNull] in IActionContext context)
        {
            return new DestroyEntityResult { Actor = EntityID };
        }
    }
}