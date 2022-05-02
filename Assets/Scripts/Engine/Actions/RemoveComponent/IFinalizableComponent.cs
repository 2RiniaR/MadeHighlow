using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RemoveComponent
{
    public interface IFinalizableComponent
    {
        public FinalizeComponentResult Finalize([NotNull] ISessionModel session);
    }
}