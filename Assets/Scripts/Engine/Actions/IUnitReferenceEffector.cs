using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IUnitReferenceEffector
    {
        [NotNull]
        public Unit OnUnitReferenced([NotNull] in ISessionModel session, [NotNull] in EntityLocator target);
    }
}