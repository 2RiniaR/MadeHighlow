using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public interface IReplacer : IPriority<IReplacer>
    {
        [ItemNotNull]
        [CanBeNull]
        public ValueList<Interrupt<Replacement>> CardReplacements([NotNull] ReplacementContext context);
    }
}
