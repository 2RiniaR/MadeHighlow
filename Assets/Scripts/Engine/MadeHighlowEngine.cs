using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
{
    public class MadeHighlowEngine
    {
        public MadeHighlowEngine(
            [NotNull] ISessionHolder sessionHolder,
            [NotNull] ISnapshotCache snapshotCache
        )
        {
            SessionHolder = sessionHolder;
            SnapshotCache = snapshotCache;
        }

        [NotNull] private ISessionHolder SessionHolder { get; }
        [NotNull] private ISnapshotCache SnapshotCache { get; }

        [NotNull]
        public AddComponentResult AddComponent(
            [NotNull] EntityLocator target,
            [NotNull] EntityComponent entityComponent
        )
        {
            throw new NotImplementedException();
        }

        [NotNull]
        public BigBangResult BigBang([NotNull] World world)
        {
            throw new NotImplementedException();
        }

        public void BreakUnit([NotNull] EntityLocator target)
        {
        }

        public void CharmUnit([NotNull] EntityLocator target)
        {
        }

        public void CommandUnit([NotNull] EntityLocator target)
        {
        }

        public void Destroy([NotNull] EntityLocator target)
        {
        }

        public void Generate([NotNull] object newObject)
        {
        }

        public void Interact()
        {
        }

        public void ActuateUnit()
        {
        }

        public void SupplyCard()
        {
        }

        public void IncrementTurn()
        {
        }

        public void Walk()
        {
        }

        public void Teleport()
        {
        }
    }
}