using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IInstantDeathEffector : IComponent
    {
        public InstantDeathEffect EffectOnInstantDeath(
            [NotNull] in IActionContext context,
            [NotNull] in InstantDeathAction action
        );
    }
}