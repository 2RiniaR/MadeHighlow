using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IDamageProcessor
    {
        public DamageProcessing ProcessDamage([NotNull] in IActionContext session);
    }
}