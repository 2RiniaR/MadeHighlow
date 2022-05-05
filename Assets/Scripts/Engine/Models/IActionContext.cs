using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IActionContext
    {
        [NotNull] public Session Session { get; }

        [NotNull]
        public World CurrentWorld();

        [NotNull]
        public World WorldAt(ID id);

        public void Append([NotNull] ISimulatable result);

        public void AppendRange([NotNull] [ItemNotNull] params ISimulatable[] results);

        public ID NewID();
    }
}