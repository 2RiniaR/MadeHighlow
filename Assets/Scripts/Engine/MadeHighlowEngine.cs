using System;
using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions;

namespace RineaR.MadeHighlow
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

        [NotNull] private ISessionHolder SessionHolder { get; }
        [NotNull] private ISnapshotCache SnapshotCache { get; }
        [NotNull] private ComponentsRegistration ComponentsRegistration { get; }

        [NotNull]
        public AddComponentResult AddComponent([NotNull] ObjectLocator target, [NotNull] Component component)
        {
            throw new NotImplementedException();
        }

        [NotNull]
        public BigBangResult BigBang([NotNull] World world)
        {
            throw new NotImplementedException();
        }

        public void BreakUnit([NotNull] ObjectLocator target)
        {
        }

        public void CharmUnit([NotNull] ObjectLocator target)
        {
        }

        public void CommandUnit([NotNull] ObjectLocator target)
        {
        }

        public void Destroy([NotNull] ObjectLocator target)
        {
        }

        public void Generate([NotNull] Object newObject)
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