using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface ISnapshotProducer
    {
        [NotNull]
        public World SnapshotAt(ID<SessionEvent> id);
    }
}