using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityStep
{
    public interface ICostEffector : IPriority<ICostEffector>
    {
        [CanBeNull]
        [ItemNotNull]
        public ValueList<Interrupt<CostEffect>> CostEffects([NotNull] CostEffectContext context);
    }
}
