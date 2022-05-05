using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IKnockBackProcessor
    {
        public KnockBackProcessing ProcessKnockBack([NotNull] IActionContext session);
    }
}