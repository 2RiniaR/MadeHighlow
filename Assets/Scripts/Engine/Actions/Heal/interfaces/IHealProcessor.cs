using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IHealProcessor
    {
        public HealProcessing ProcessHeal([NotNull] IActionContext session);
    }
}