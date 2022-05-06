using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyEntityAction : Action<DestroyEntityResult>
    {
        [NotNull] public EntityID EntityID { get; init; } = new();


        [NotNull]
        public override DestroyEntityResult Validate([NotNull] in IActionContext context)
        {
            return new DestroyEntityResult { Actor = EntityID };
        }
    }
}