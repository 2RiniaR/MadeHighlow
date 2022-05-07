using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IDestroyEntityValidator
    {
        [NotNull]
        public DestroyEntityValidation ValidateDestroyEntity([NotNull] IActionContext session);
    }
}
