using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    /// <summary>
    ///     ダメージを与えるアクションに対して、影響を与えるもの
    /// </summary>
    public interface IInstantDamageEffector
    {
        public ValueList<Interrupt<InstantDamageEffect>> EffectsOnInstantDamage(
            [NotNull] IActionContext context,
            [NotNull] InstantDamageAction action
        );
    }
}
