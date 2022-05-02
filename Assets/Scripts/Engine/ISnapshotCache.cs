using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface ISnapshotCache
    {
        [CanBeNull]
        public World TryGetCache(in ID<SessionEvent> id);

        public void SetCache(in ID<SessionEvent> id, [NotNull] in World value);
    }
}