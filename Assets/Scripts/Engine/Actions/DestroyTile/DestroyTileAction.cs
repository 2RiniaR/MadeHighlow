using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record DestroyTileAction : IValidatable
    {
        [NotNull] public TileEnsuredID EntityID { get; init; } = new();

        ISimulatable IValidatable.Validate(in IActionContext context)
        {
            return Validate(context);
        }

        [NotNull]
        public DestroyTileResult Validate([NotNull] in IActionContext context)
        {
            return new DestroyTileResult { Actor = EntityID };
        }
    }
}