using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects;

namespace RineaR.MadeHighlow.Engine
{
    public interface ISnapshotProducer
    {
        [NotNull]
        public World SnapshotAt(ID<SessionEvent> id);
    }
}