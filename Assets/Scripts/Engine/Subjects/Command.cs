using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public abstract record Command([NotNull] CardID CardID, [NotNull] UnitID UnitID)
    {
        [NotNull]
        public abstract Action ActionIn([NotNull] World world);
    }
}
