using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IDeathProcessor
    {
        public DeathProcessing ProcessDeath([NotNull] IActionContext session);
    }
}