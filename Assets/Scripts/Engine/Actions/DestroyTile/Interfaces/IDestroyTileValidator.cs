using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IDestroyTileValidator
    {
        [NotNull]
        public DestroyTileValidation ValidateDestroyTile([NotNull] IActionContext session);
    }
}