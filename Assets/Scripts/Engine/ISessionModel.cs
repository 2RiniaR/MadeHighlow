using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public interface ISessionModel
    {
        [NotNull] public Session Session { get; }

        [NotNull]
        public World Current();

        [NotNull]
        public World SnapshotAt(ID<SessionEvent> id);

        public void Advance([NotNull] Result result);

        public void AdvanceRange([NotNull] [ItemNotNull] params Result[] results);

        public ID<T> NewID<T>();
    }
}