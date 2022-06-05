using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public interface IReplacer : IPriority<IReplacer>
    {
        [ItemNotNull]
        [CanBeNull]
        public ValueList<Interrupt<Replacement>> CardReplacements([NotNull] ReplacementContext context);
    }
}
