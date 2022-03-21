using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Events;

namespace RineaR.MadeHighlow.Engine.Environments
{
    public interface ISessionHolder
    {
        [NotNull]
        public Session Current();

        public void Append([NotNull] in EventTimeline timeline);
    }
}