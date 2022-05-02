using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IObjectReferenceEffector
    {
        [NotNull]
        public Object OnObjectReferenced([NotNull] in ISessionModel session, [NotNull] in ObjectLocator target);
    }
}