using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine.Environments
{
    public interface ISnapshotCache
    {
        [CanBeNull]
        public World TryGetCache([NotNull] in ID<SessionEvent> id);

        public void SetCache([NotNull] in ID<SessionEvent> id, [NotNull] in World value);
    }
}