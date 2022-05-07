using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record InstantDamageInterrupt([NotNull] ComponentID ComponentID, [NotNull] InstantDamageEffect Effect);
}
