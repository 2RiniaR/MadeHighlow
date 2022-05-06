using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IFinalizableComponent
    {
        public FinalizeComponentResult Finalize([NotNull] IActionContext session);
    }
}