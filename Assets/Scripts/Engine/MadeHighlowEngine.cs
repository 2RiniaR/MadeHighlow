using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Environments;
using RineaR.MadeHighlow.Engine.Events;
using Action = RineaR.MadeHighlow.Engine.Actions.Action;

namespace RineaR.MadeHighlow.Engine
{
    public class MadeHighlowEngine
    {
        public MadeHighlowEngine(
            [NotNull] ISessionHolder sessionHolder,
            [NotNull] ISnapshotCache snapshotCache,
            [NotNull] ComponentsRegistration componentsRegistration
        )
        {
            SessionHolder = sessionHolder;
            SnapshotCache = snapshotCache;
            ComponentsRegistration = componentsRegistration;
        }

        [NotNull] public ISessionHolder SessionHolder { get; }
        [NotNull] public ISnapshotCache SnapshotCache { get; }
        [NotNull] public ComponentsRegistration ComponentsRegistration { get; }

        [NotNull]
        public EventTimeline RunAction([NotNull] Action action)
        {
            throw new NotImplementedException();
        }
    }
}