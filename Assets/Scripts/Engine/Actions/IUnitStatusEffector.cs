using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IUnitStatusEffector
    {
        [NotNull]
        public Unit OnReferenced([NotNull] in World world, [NotNull] in ObjectLocator locator);
    }
}