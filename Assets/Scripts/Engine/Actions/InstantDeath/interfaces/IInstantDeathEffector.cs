using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IInstantDeathEffector : IComponent
    {
        public InstantDeathEffect EffectOnInstantDeath(
            [NotNull] IActionContext context,
            [NotNull] InstantDeathAction action
        );
    }
}