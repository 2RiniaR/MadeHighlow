using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    /// <summary>
    ///     即死効果を与えるアクションに対して、影響を与えるもの
    /// </summary>
    public interface IInstantDeathEffector
    {
        public ValueList<Interrupt<InstantDeathEffect>> EffectsOnInstantDeath(
            [NotNull] IActionContext context,
            [NotNull] InstantDeathAction action
        );
    }
}
