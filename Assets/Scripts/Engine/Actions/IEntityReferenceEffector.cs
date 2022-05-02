using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IEntityReferenceEffector
    {
        [NotNull]
        public Entity OnEntityReferenced([NotNull] in ISessionModel session, [NotNull] in EntityLocator target);
    }
}