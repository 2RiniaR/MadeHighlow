using JetBrains.Annotations;

namespace RineaR.MadeHighlow.RemoveComponent
{
    public interface IFinalizableComponent
    {
        public FinalizeComponentResult Finalize([NotNull] IActionContext session);
    }
}